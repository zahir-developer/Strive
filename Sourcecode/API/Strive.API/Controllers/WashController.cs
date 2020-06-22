using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.Common;
using System;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Strive.API.Controllers
{
    [Authorize]
    public class WashController : ControllerBase
    {
        //readonly IWash _wash;
        readonly IConfiguration _configuration;

        //public WashController(IWash wash, IConfiguration configuration)
        //{
        //    _wash = wash;
        //    _configuration = configuration;
        //}

        [HttpGet]
        [Route("wash")]
        [AllowAnonymous]
        public Result GetAllWash()
        {
            throw new NotImplementedException();
            //var result = _wash.GetAllWashDetails(authentication, _configuration.GetSection("StriveSettings:Jwt")["SecretKey"]);
            //return result;
        }

    }
}