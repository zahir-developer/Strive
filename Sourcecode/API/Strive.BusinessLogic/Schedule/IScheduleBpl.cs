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
        Result SaveSchedule(Strive.BusinessEntities.Model.ScheduleModel schedule);
        Result UpdateSchedule(Strive.BusinessEntities.Model.ScheduleModel schedule);
    }
}
