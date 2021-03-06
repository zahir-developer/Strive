using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.Schedule;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Schedule
{
    public class ScheduleBpl : Strivebase, IScheduleBpl
    {
        public ScheduleBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper)
        {
        }
        public Result SaveSchedule(ScheduleDto schedule)
        {
            return ResultWrap(new ScheduleRal(_tenant).SaveSchedule, schedule, "Result");
        }
        public Result DeleteSchedule(int scheduleId)
        {
            return ResultWrap(new ScheduleRal(_tenant).DeleteSchedule, scheduleId, "Status");
        }
        public Result GetSchedule(ScheduleDetailDto scheduleDetail)
        {
            return ResultWrap(new ScheduleRal(_tenant).GetSchedule,scheduleDetail, "ScheduleDetail");
        }
        public Result GetScheduleById(int scheduleId)
        {
            return ResultWrap(new ScheduleRal(_tenant).GetScheduleById, scheduleId, "Status");
        }

        public Result GetScheduleAndForcasted(ScheduleDetailDto scheduleDetail)
        {
            return ResultWrap(new ScheduleRal(_tenant).GetScheduleAndForcasted, scheduleDetail, "ScheduleForcastedDetail");
        }


    }
}
