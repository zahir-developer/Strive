using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Weather;
using Strive.BusinessEntities.Weather;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Weather;
using Strive.Common;
using System;
using System.Net;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    [Route("/Admin/[controller]")]
    public class WeatherController : ControllerBase
    {
        //weatherInfoView _result;
        //readonly IWeatherHelper _weatherHelper;
        readonly IWeatherBpl _weatherBpl;
        readonly IConfiguration _configuration;
        

        public WeatherController(IWeatherBpl weatherBpl,  IConfiguration configuration)
        {
            _weatherBpl = weatherBpl;
            _configuration = configuration;
            _weatherBpl = weatherBpl;
        }


        [HttpGet]
        [Route("/Admin/[controller]/GetWeatherPrediction/{locationId}/{dateTime}")]
        public Result GetWeatherPrediction(int locationId, DateTime dateTime)
        {
            return _weatherBpl.GetWeatherPrediction(locationId, dateTime);
        }


        [HttpPost]
        [Route("/Admin/[controller]/SaveWeatherPrediction")]
        public Result SaveWeatherPrediction([FromBody] WeatherDTO weatherPrediction)
        {
            return _weatherBpl.AddWeatherPrediction(weatherPrediction);
        }

        [HttpGet]
        [Route("GetWeatherData/{locationId}")]
        [AllowAnonymous]
        public async System.Threading.Tasks.Task<WeatherView> GetWeatherAsync(int locationId)
        {
            var result = new WeatherView();
            try
            {
                string baseurl = Pick("Weather", "BaseUrl");
                string api = Pick("Weather", "Apikey");
                string ApiMethod = Pick("Weather", "ApiMethod");
                result = await _weatherBpl.GetWeather(baseurl, api, ApiMethod, locationId);
            }
            catch (Exception ex)
            {
                //_result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;

        }
        private string Pick(string section, string name)
        {
            return _configuration.GetSection("StriveAdminSettings:" + section)[name] ?? string.Empty;
        }

        [HttpPost]
        [Route("GetForcastedRainTemperature")]
        public Result GetForcastedCarEmployeehours([FromBody]ForecastedRainPercentageDto forecastedRainPercentage)
        {

            return _weatherBpl.GetForcastedCarEmployeehours(forecastedRainPercentage);
        }
    }
}