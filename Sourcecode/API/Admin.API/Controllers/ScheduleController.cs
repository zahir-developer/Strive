using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Strive.BusinessEntities.DTO.Schedule;
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
        public Result ScheduleSave([FromBody] ScheduleDto schedule)
        {
            return _bplManager.SaveSchedule(schedule);
        }
        //[HttpPost]
        //[Route("UpdateSchedule")]
        //public Result UpdateSchedule([FromBody]ScheduleModel schedule)
        //{
        //     return _bplManager.UpdateSchedule(schedule);
        //}
        [HttpDelete]
        [Route("DeleteSchedule")]
        public Result DeleteSchedule(int id) => _bplManager.DeleteSchedule(id);
        #region
        [HttpPost]
        [Route("GetSchedule")]
        public Result GetSchedule([FromBody] ScheduleDetailDto scheduleDetail)
        {
            return _bplManager.GetSchedule(scheduleDetail);
        }
        #endregion
        [HttpGet]
        [Route("GetScheduleById")]
        public Result GetScheduleById(int scheduleId) => _bplManager.GetScheduleById(scheduleId);

        
    }
}