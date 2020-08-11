using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Schedule;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ScheduleController : StriveControllerBase<IScheduleBpl>
    {
        public ScheduleController(IScheduleBpl scheduleBpl) : base(scheduleBpl) { }

       [HttpPost]
       [Route("ScheduleSave")]
       public Result ScheduleSave([FromBody] ScheduleModel schedule)
       {
            return _bplManager.SaveSchedule(schedule);
       }
       [HttpPost]
       [Route("UpdateSchedule")]
       public Result UpdateSchedule([FromBody]ScheduleModel schedule)
       {
            return _bplManager.UpdateSchedule(schedule);
       }
    }
}