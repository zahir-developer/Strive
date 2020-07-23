using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Weather;
using Strive.BusinessLogic;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Weather;
using Strive.Common;
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


        [HttpPost]
        [Route("/Admin/[controller]/GetWeatherPrediction/{locationId}")]
        public Result UpdateWeatherPrediction(int locationId)
        {
            return _weatherBpl.GetWeatherPrediction(locationId);
        }


        [HttpPost]
        [Route("/Admin/[controller]/AddWeatherPrediction")]
        public Result WeatherPrediction([FromBody] WeatherPrediction weatherPrediction)
        {
            return _weatherBpl.AddWeatherPrediction(weatherPrediction);
        }

        [HttpPost]
        [Route("/Admin/[controller]/UpdateWeatherPrediction")]
        public Result UpdateWeatherPrediction([FromBody] WeatherPrediction weatherPrediction)
        {
            return _weatherBpl.UpdateWeatherPrediction(weatherPrediction);
        }


    }
}