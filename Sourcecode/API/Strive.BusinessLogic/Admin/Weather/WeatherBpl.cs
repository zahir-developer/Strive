using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Weather;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.Weather;
using Strive.BusinessLogic.Weather;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Strive.BusinessLogic
{
    public class WeatherBpl : Strivebase, IWeatherBpl
    {
        public WeatherBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        void SaveWeathertoDB_AutoProcess()
        {
            //var weatherSchema = GetWeatherSchemaList();
            //var weatherDetails

        }

        void IWeatherBpl.SaveWeathertoDB_AutoProcess()
        {
            throw new System.NotImplementedException();
        }

        public Result GetWeatherPrediction(int locationId, DateTime dateTime)
        {
            try
            {

                DateTime lastMonth = dateTime.AddMonths(-1);
                DateTime lastweek = dateTime.AddDays(-7);
                DateTime lastThirdMonth = dateTime.AddMonths(-3);
                WeatherPredictionResultViewModel weatherPredictionDetails = new WeatherPredictionResultViewModel();
                weatherPredictionDetails.WeatherPredictionLastWeek = new WeatherPredictions();
                weatherPredictionDetails.WeatherPredictionLastMonth = new WeatherPredictions();
                weatherPredictionDetails.WeatherPredictionLastThirdMonth = new WeatherPredictions();

                var weather = new WeatherRal(_tenant).GetWeatherDetails(locationId, dateTime);
                if (weather != null)
                {
                    var todayResult = weather.FirstOrDefault(s => s.CreatedDate == dateTime);

                    if (todayResult != null)
                    {
                        weatherPredictionDetails.WeatherPredictionToday = todayResult;
                    }

                    var lastWeekResult = weather.FirstOrDefault(s => s.CreatedDate == lastweek);
                    if (lastWeekResult != null)
                    {
                        weatherPredictionDetails.WeatherPredictionLastWeek = lastWeekResult;

                    }
                    var lastMonthResult = weather.FirstOrDefault(s => s.CreatedDate == lastMonth);
                    if (lastMonthResult != null)
                    {
                        weatherPredictionDetails.WeatherPredictionLastMonth = lastMonthResult;

                    }
                    var lastThridMonthResult = weather.FirstOrDefault(s => s.CreatedDate == lastThirdMonth);
                    if (lastThridMonthResult != null)
                    {
                        weatherPredictionDetails.WeatherPredictionLastThirdMonth = lastThridMonthResult;

                    }
                }
                _resultContent.Add(weatherPredictionDetails.WithName("WeatherPrediction"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public async Task<bool> GetDailyWeatherPredictionAsync(string baseUrl, string apiKey, string apiMethod)
        {

            var allLocationList = new LocationRal(_tenant).GetAllLocation();
            foreach (var oLoc in allLocationList)
            {
                //Get Current weather:
                var now = DateTime.Now;
                string startTime = now.AddHours(-6).ToString("yyyy-MM-ddTHH:mm:ssZ").ToString();
                //new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).ToString("yyyy-MM-ddTHH:mm:ssZ").ToString(); 
                string endTime = now.AddHours(30).ToString("yyyy-MM-ddTHH:mm:ssZ").ToString();
                string[] fields = new string[] { "precipitation", "precipitation_probability", "temp" };

                var query = "?location=" + oLoc.Latitude + "," + oLoc.Longitude + "&startTime=" + startTime + "&endTime=" + endTime + "&fields=temperature,precipitationProbability,precipitationType&timesteps=1h&units=metric";

                WeatherInfo weatherInfo = new WeatherInfo();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("apiKey", apiKey);


                        var responselink = baseUrl + apiMethod + query;

                        var response = await client.GetAsync(responselink);
                        response.EnsureSuccessStatusCode();
                        var result = "";
                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsStringAsync();
                        }
                        var res = JsonConvert.DeserializeObject<WeatherData>(result);


                        //Current Weather
                        weatherInfo.Temporature = ConvertToFahrenheit(
                            res.Data.timelines[0].intervals.Where(x => Convert.ToDateTime(x.startTime).ToShortDateString() == Convert.ToDateTime(startTime).ToShortDateString()).OrderByDescending(x => x.values.temperature).Take(1).ToList()[0].values.temperature).ToString();
                        var oIntervals = res.Data.timelines[0].intervals.Where(x => x.values.precipitationType == 1 && Convert.ToDateTime(x.startTime).ToShortDateString() == Convert.ToDateTime(startTime).ToShortDateString()).OrderByDescending(x => x.values.precipitationProbability).Take(1).ToList();
                        if (oIntervals.Count > 0)
                        {
                            weatherInfo.RainPercentage = res.Data.timelines[0].intervals.Where(x => x.values.precipitationType == 1 && Convert.ToDateTime(x.startTime).ToShortDateString() == Convert.ToDateTime(startTime).ToShortDateString()).OrderByDescending(x => x.values.precipitationProbability).Take(1).ToList()[0].values.precipitationProbability.ToString();
                        }
                        else
                        {
                            weatherInfo.RainPercentage = "0";
                        }

                        WeatherPredictionDTO weatherPrediction = new WeatherPredictionDTO();
                        weatherPrediction.LocationWeather = new LocationWeather();
                        weatherPrediction.LocationWeather.LocationId = oLoc.LocationId;
                        weatherPrediction.LocationWeather.HighTemperature = weatherInfo.Temporature;

                        weatherPrediction.LocationWeather.RainProbability = weatherInfo.RainPercentage;
                        weatherPrediction.LocationWeather.WeatherDate = DateTime.Now;
                        weatherPrediction.LocationWeather.CreatedDate = DateTime.Now;
                        ResultWrap(new WeatherRal(_tenant).AddWeatherPrediction, weatherPrediction, "Status");
                    }
                }
                catch (Exception ex)
                {
                    string[] lines = { "Error: " + ex.Message.ToString(), "Location :" + oLoc.LocationId.ToString() + "," + oLoc.Latitude.ToString() + "," + oLoc.Longitude.ToString() };

                    // Append new lines of text to the file
                    File.AppendAllLines(Path.Combine(_tenant.ErrorLog, "Errorlog.txt"), lines);
                }
            }



            return true;
        }
        public Result AddWeatherPrediction(WeatherDTO weatherPrediction)
        {
            try
            {
                bool result = false;
                if (weatherPrediction.WeatherPrediction.WeatherId == 0)
                {

                    return ResultWrap(new WeatherRal(_tenant).AddWeather, weatherPrediction, "Status");
                }
                else
                {
                    return ResultWrap(new WeatherRal(_tenant).UpdateWeather, weatherPrediction, "Status");
                }
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }
        public async Task<WeatherView> GetWeather(string baseUrl, string apiKey, string apiMethod, int locationId)
        {
            WeatherView weatherInfoView = new WeatherView();

            var weather = new WeatherRal(_tenant).GetWeatherPredictionDetails(locationId, DateTime.UtcNow);
            if (weather.Count() > 0)
            {
                weatherInfoView.CurrentWeather = new WeatherInfo();
                weatherInfoView.CurrentWeather.RainPercentage = weather[0].RainProbability;
                weatherInfoView.CurrentWeather.Temporature = weather[0].HighTemperature;
            }
            /*
                var addresssDetail = new LocationRal(_tenant).GetLocationAddressDetails(locationId);

            //Get Current weather:
            string startTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ").ToString(); ;
            string endTime = DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ssZ").ToString();
            string[] fields = new string[] { "precipitation", "precipitation_probability", "temp" };

            var query = "?location=" + addresssDetail.Latitude + "," + addresssDetail.Longitude + "&startTime=" + startTime + "&endTime=" + endTime + "&fields=temperature,precipitationProbability,precipitationType&timesteps=1h&units=metric";

            weatherInfoView = await GetWeatherInfoAsync(baseUrl, apiKey, apiMethod, query);
            
            _result = Helper.BindSuccessResult(_resultContent);
            */
            return weatherInfoView;

        }

        public async Task<WeatherView> GetWeatherInfoAsync(string baseUrl, string apiKey, string apiMethod, string query)
        {
            var result = "";
            string rainPercentage = "-";
            string Temperature = "-";

            WeatherView weatherInfoView = new WeatherView();
            weatherInfoView.CurrentWeather = new WeatherInfo();
            weatherInfoView.LastWeekWeather = new WeatherInfo();
            weatherInfoView.LastMonthWeather = new WeatherInfo();

            WeatherInfo weatherInfo = new WeatherInfo();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("apiKey", apiKey);


                    var responselink = baseUrl + apiMethod + query;
                    var response = await client.GetAsync(responselink);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    var res = JsonConvert.DeserializeObject<WeatherData>(result);

                    //Current Weather
                    weatherInfo.Temporature = ConvertToFahrenheit(res.Data.timelines[0].intervals[0].values.temperature).ToString();
                    weatherInfo.RainPercentage = res.Data.timelines[0].intervals[0].values.precipitationType == 1 ? res.Data.timelines[0].intervals[0].values.precipitationProbability.ToString("0.00") : "0";
                    weatherInfoView.CurrentWeather = weatherInfo;

                    //LastWeekWeather
                    weatherInfoView.LastWeekWeather.Temporature = "";
                    weatherInfoView.LastWeekWeather.RainPercentage = "";

                    //LastMonthWeather
                    weatherInfoView.LastMonthWeather.Temporature = Temperature;
                    weatherInfoView.LastMonthWeather.RainPercentage = rainPercentage;

                }
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return weatherInfoView;

        }

        public decimal ConvertToFahrenheit(decimal celsius)
        {
            return decimal.Round((celsius * 9 / 5) + 32);
        }

        public Result GetForcastedCarEmployeehours(ForecastedRainPercentageDto forecastedRainPercentage)
        {

            return ResultWrap(new WeatherRal(_tenant).GetForcastedCarEmployeehours, forecastedRainPercentage, "ForecastedRainpercentage");
        }
    }

    public class WeatherData
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        //[JsonProperty("Temp")]
        //public List<Temp> Temp { get; set; }
        //[JsonProperty("Precipitation")]
        //public List<Precipitation> Precipitation { get; set; }
        //[JsonProperty("precipitation_probability")]
        //public PrecipitationProbability PrecipitationProbability { get; set; }
    }
}






