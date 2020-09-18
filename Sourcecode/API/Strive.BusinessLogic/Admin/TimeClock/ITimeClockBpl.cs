using Strive.BusinessEntities.DTO.TimeClock;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.TimeClock
{
    public interface ITimeClockBpl
    {
        Result GetTimeClock(TimeClockDto timeClock);

        Result SaveTimeClock(Strive.BusinessEntities.Model.TimeClockModel timeClock);

        Result TimeClockEmployeeDetails(TimeClockEmployeeDetailDto timeClockEmployeeDetailDto);

        Result TimeClockWeekDetails(TimeClockWeekDetailDto timeClockWeekDetailDto);

        Result DeleteTimeClockEmployee(TimeClockDeleteDto timeClockDeleteDto);

    }
}
