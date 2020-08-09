using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.TimeClock;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.TimeClock;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class TimeClockController : StriveControllerBase<ITimeClockBpl>
    {

        public TimeClockController(ITimeClockBpl timeClockBpl) : base(timeClockBpl) { }

        [HttpPost]
        [Route("Save")]
        public Result SaveTimeClock([FromBody] TimeClockModel clockTime)
        {
            return _bplManager.SaveTimeClock(clockTime);
        }

        [HttpPost]
        [Route("TimeClockDetails")]
        public Result TimeClock([FromBody] TimeClockDto timeClock) => _bplManager.GetTimeClock(timeClock);

    }
}