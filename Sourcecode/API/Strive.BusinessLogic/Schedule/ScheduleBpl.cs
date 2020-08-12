﻿using Microsoft.Extensions.Caching.Distributed;
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
        public Result SaveSchedule(Strive.BusinessEntities.Model.ScheduleModel schedule)
        {
            return ResultWrap(new ScheduleRal(_tenant).SaveSchedule, schedule, "Result");
        }
        public Result UpdateSchedule(Strive.BusinessEntities.Model.ScheduleModel schedule)
        {
            return ResultWrap(new ScheduleRal(_tenant).UpdateSchedule, schedule, "Status");
        }
        public Result DeleteSchedule(int scheduleId)
        {
            return ResultWrap(new ScheduleRal(_tenant).DeleteSchedule, scheduleId, "Status");
        }
        public Result GetSchedule()
        {
            return ResultWrap(new ScheduleRal(_tenant).GetSchedule, "Status");
        }
        public Result GetScheduleById(int scheduleId)
        {
            return ResultWrap(new ScheduleRal(_tenant).GetScheduleById, scheduleId, "Status");
        }
        
    }
}
