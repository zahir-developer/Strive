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

        [HttpPost]
        [Route("GetTimeClockEmployeeDetails")]
        public Result TimeClockEmployeeDetails([FromBody] TimeClockEmployeeDetailDto timeClockEmployeeDetailDto) => _bplManager.TimeClockEmployeeDetails(timeClockEmployeeDetailDto);

        [HttpPost]
        [Route("GetTimeClockWeekDetails")]
        public Result TimeClockWeekDetails([FromBody] TimeClockWeekDetailDto timeClockWeekDetailDto) => _bplManager.TimeClockWeekDetails(timeClockWeekDetailDto);

        [HttpPost]
        [Route("GetTimeClockEmployeeHourDetails")]
        public Result GetTimeClockEmployeeHourDetails([FromBody] TimeClockLocationDto timeClockLocationDto) => _bplManager.TimeClockEmployeeHourDetail(timeClockLocationDto);


        [HttpDelete]
        [Route("DeleteTimeClockEmployee")]
        public Result DeleteTimeClockEmployee(TimeClockDeleteDto timeClockDeleteDto) => _bplManager.DeleteTimeClockEmployee(timeClockDeleteDto);


       

      [HttpGet]
        [Route("GetClockedInDetailer/{date}")]
        public Result GetClockedInDetailer(DateTime dateTime) => _bplManager.GetClockedInDetailer(dateTime);






    }
}