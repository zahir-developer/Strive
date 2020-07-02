using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Strive.BusinessLogic.Location;

namespace Strive.BusinessLogic.Common
{
    public class CommonBpl : Strivebase, ICommonBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;

        public CommonBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }

        public Result GetAllCodes()
        {
            try
            {
                var lstCode = new CommonRal(_tenant).GetAllCodes();
                _resultContent.Add(lstCode.WithName("Codes"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }

        public Result GetCodesByCategory(GlobalCodes codeCategory)
        {
            try
            {
                var lstCode = new CommonRal(_tenant).GetCodeByCategory(codeCategory);
                _resultContent.Add(lstCode.WithName("Codes"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }

        public Result GetCodesByCategory(int codeCategoryId)
        {
            try
            {
                var lstCode = new CommonRal(_tenant).GetCodeByCategoryId(codeCategoryId);
                _resultContent.Add(lstCode.WithName("Codes"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }

        public async Task<Result> CreateLocationForWeatherPortal()
        {
            const string baseUrl = "https://api.climacell.co/";
            const string apiMethod = "v3/locations";
            const string apiKey = "sbXIC0D1snD0d4SrQEXPdG8iNiD1mOLV";

            var weatherlocation = new WeatherLocation()
            {
                name = "Strive-Location1",
                point = new point() {lat = 34.07, lon = -84.29}
            };
            var wlocation = JsonConvert.SerializeObject(weatherlocation);
            var stringContent = new StringContent(wlocation, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var response = await client.PostAsync(apiMethod + "?apikey=" + apiKey, stringContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                }
            }

            return null;
        }


        //https://api.climacell.co/v3/weather/forecast/daily?location_id=5efe1a24ed57b2001925dd6e&start_time=2020-07-02&end_time=2020-07-02&fields%5B%5D=precipitation&fields%5B%5D=precipitation_probability&fields%5B
        public async Task<Result> GetWeather()
        {
            const string baseUrl = "https://api.climacell.co/";
            const string apiMethod = "v3/weather/forecast/daily";
            const string apiKey = "sbXIC0D1snD0d4SrQEXPdG8iNiD1mOLV";

            string location_id = "5efe1a24ed57b2001925dd6e";
            string start_time = "2020-07-02";
            string end_time = "2020-07-02";
            string[] fields = new string[] {"precipitation","precipitation_probability","temp"};

            var queries =
                $"location_id={location_id}&start_time={start_time}&end_time={end_time}&fields=precipitation&fields=precipitation_probability&fields=temp";
                

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));;

                // New code:
                var response = await client.GetAsync(apiMethod + "?apikey=" + apiKey + "&" + queries);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                }
            }

            return null;

        }



        public async Task<Result> GetAllWeatherLocations()
        {
            const string baseUrl = "https://api.climacell.co/";
            const string apiMethod = "v3/locations";
            const string apiKey = "sbXIC0D1snD0d4SrQEXPdG8iNiD1mOLV";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var response = await client.GetAsync(apiMethod + "?apikey=" + apiKey);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();
                }
            }

            return null;

        }
    }
}
