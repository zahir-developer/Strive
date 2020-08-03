using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.TimeClock;
using Strive.BusinessLogic.Auth;
using Strive.BusinessLogic.TimeClock;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class TimeClockController : ControllerBase
    {

        ITimeClockBpl _clockTimeBpl = null;

        public TimeClockController(ITimeClockBpl clockTimeBpl)
        {
            _clockTimeBpl = clockTimeBpl;
        }


        [HttpPost]
        [Route("Save")]
        public Result SaveTimeClock([FromBody] TimeClock clockTime)
        {
            return _clockTimeBpl.SaveTimeClock(clockTime);

        }

        [HttpGet]
        [Route("TimeClock/userId/datetime")]
        public Result TimeClock(int userId, DateTime dateTime)
        {
            return _clockTimeBpl.GetTimeClock(userId, dateTime);
        }
    }
}