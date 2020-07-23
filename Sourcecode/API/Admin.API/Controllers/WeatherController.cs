using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Weather;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Weather;
using Strive.Common;
using System;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    public class WeatherController : ControllerBase
    {
        readonly IWeatherBpl _weatherBpl;

        public WeatherController(IWeatherBpl weatherBpl)
        {
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
        public Result SaveWeatherPrediction([FromBody] WeatherPrediction weatherPrediction)
        {
            return _weatherBpl.AddWeatherPrediction(weatherPrediction);
        }
    }
}