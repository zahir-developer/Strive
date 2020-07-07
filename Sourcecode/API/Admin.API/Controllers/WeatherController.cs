using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities;
using Strive.BusinessLogic.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    public class WeatherController : ControllerBase
    {
        readonly ICommonBpl _commonBpl;

        public WeatherController(ICommonBpl commonBpl)
        {
            _commonBpl = commonBpl;
        }

        [HttpGet]
        [Route("/Admin/[controller]")]
        [AllowAnonymous]
        public void GetWeather()
        {
            var result = _commonBpl.GetWeather();
        }

        [HttpPost]
        [Route("/Admin/[controller]/AddLocation")]
        [AllowAnonymous]
        public void AddWeatherLocation()
        {
            var result = _commonBpl.CreateLocationForWeatherPortal();
        }

        
    }
}