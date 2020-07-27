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
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public WeatherBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
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
        public async Task<WeatherInfo> GetWeather(string baseUrl, string apiKey, string apiMethod, int locationId)
        {
            WeatherInfo weatherInfoView = new WeatherInfo();

            WeatherInfo weatherInfo = new WeatherInfo();
            var addresssDetail = new LocationRal(_tenant).GetLocationAddressDetails(locationId);

            //Get Current weather:
            string startTime = "now";
            string endTime = "2020-07-27T14%3A09%3A50Z";
            string[] fields = new string[] { "precipitation", "precipitation_probability", "temp" };

            var query = "?location_id=" + addresssDetail.WeatherLocationId + "&lat=" + addresssDetail.Latitude + "&lon=" + addresssDetail.Longitude + "&start_time=" + startTime + "&end_time=" + endTime + "&unit_system=si&fields=temp&fields=precipitation_probability&fields=precipitation";

            weatherInfoView = await GetWeatherInfoAsync(baseUrl, apiKey, apiMethod, query);

            _result = Helper.BindSuccessResult(_resultContent);

            return weatherInfoView;

        }

        public async Task<WeatherInfo> GetWeatherInfoAsync(string baseUrl, string apiKey, string apiMethod, string query)
        {
            var result = "";
            int laseWeekRainPer = 45;
            int laseWeekTemp = 32;
            int laseMonthRainPer = 45;
            int laseMonthTemp = 32;

            WeatherInfo weatherInfoView = new WeatherInfo();
            LastWeekWeather lastWeekWeather = new LastWeekWeather();
            LastMonthWeather lastMonthWeather = new LastMonthWeather();
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
                    weatherInfoView.Temporature = res.FirstOrDefault().Temp[0].Min.Value;
                    weatherInfoView.Rain = res.FirstOrDefault().Precipitation[0].Max.Value;
                    weatherInfoView.RainPercentage = res.FirstOrDefault().PrecipitationProbability.Value;

                    lastWeekWeather.Temporature = laseWeekTemp;
                    lastWeekWeather.RainPercentage = laseWeekRainPer;
                    lastMonthWeather.Temporature = laseMonthTemp;
                    lastMonthWeather.RainPercentage = laseMonthRainPer;
                    weatherInfoView.lastWeekWeather = lastWeekWeather;
                    weatherInfoView.LastMonthWeather = lastMonthWeather;
                }
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return weatherInfoView;

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
