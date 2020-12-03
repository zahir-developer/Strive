using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Weather;
using Strive.BusinessLogic.Weather;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
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
                var weather = new WeatherRal(_tenant).GetWeatherDetails(locationId, dateTime);
                if (weather != null)
                    _resultContent.Add(weather.WithName("WeatherPrediction"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }


        public Result AddWeatherPrediction(WeatherPrediction weatherPrediction)
        {
            try
            {
                bool result = false;
                if (weatherPrediction.WeatherId == 0)
                {
                    result = new WeatherRal(_tenant).AddWeather(weatherPrediction);
                }
                else
                {
                    result = new WeatherRal(_tenant).UpdateWeather(weatherPrediction);
                }
                _resultContent.Add(result.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
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

            var addresssDetail = new LocationRal(_tenant).GetLocationAddressDetails(locationId);

            //Get Current weather:
            string startTime = "now";
            string endTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm").ToString();
            string[] fields = new string[] { "precipitation", "precipitation_probability", "temp" };

            var query = "?location_id=" + addresssDetail.WeatherLocationId + "&lat=" + addresssDetail.Latitude + "&lon=" + addresssDetail.Longitude + "&start_time=" + startTime + "&end_time=" + endTime + "&unit_system=si&fields=temp&fields=precipitation_probability&fields=precipitation";

            weatherInfoView = await GetWeatherInfoAsync(baseUrl, apiKey, apiMethod, query);

            _result = Helper.BindSuccessResult(_resultContent);

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
                    var res = JsonConvert.DeserializeObject<List<WeatherData>>(result);

                    //Current Weather
                    weatherInfo.Temporature = ConvertToFahrenheit(res.FirstOrDefault().Temp[0].Min.Value.GetValueOrDefault(0)).ToString();
                    weatherInfo.RainPercentage = res.FirstOrDefault().PrecipitationProbability.Value.ToString();
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
    }

    public class WeatherData
    {
        [JsonProperty("Temp")]
        public List<Temp> Temp { get; set; }
        [JsonProperty("Precipitation")]
        public List<Precipitation> Precipitation { get; set; }
        [JsonProperty("precipitation_probability")]
        public PrecipitationProbability PrecipitationProbability { get; set; }
    }
}
