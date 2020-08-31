using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.Common;
using Strive.BusinessLogic.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class CommonController : ControllerBase
    {
        readonly ICommonBpl _commonBpl = null;

        public CommonController(ICommonBpl commonBpl)
        {
            _commonBpl = commonBpl;
        }

        [HttpGet]
        [Route("CountryList")]
        public Result GetCountyList()
        {
            return _commonBpl.GetCodesByCategory(GlobalCodes.COUNTRY);
        }

        [HttpGet]
        [Route("StateList")]
        public Result GetStateList()
        {
            return _commonBpl.GetCodesByCategory(GlobalCodes.STATE);
        }

        [HttpGet]
        [Route("GetCodesByCategory/{globalCode}")]
        public Result GetCodeByCategory(GlobalCodes globalCode)
        {
            return _commonBpl.GetCodesByCategory(globalCode);
        }


        [HttpGet]
        [Route("/Admin/[controller]")]
        [AllowAnonymous]
        public void GetWeather()
        {
            var result = _commonBpl.GetWeather();
        }

        [HttpGet]
        [Route("GetEmailIdExist/{email}")]
        public Result GetEmailIdExist(string email) => _commonBpl.GetEmailIdExist(email);
    }
}
