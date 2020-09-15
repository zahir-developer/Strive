using Strive.BusinessEntities.DTO.Schedule;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Schedule
{
    public interface IScheduleBpl
    {
        Result SaveSchedule(ScheduleDto schedule);
        Result DeleteSchedule(int scheduleId);
        Result GetScheduleById(int scheduleId);
        Result GetSchedule(ScheduleDetailDto scheduleDetail);

    }
}
