using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.BusinessEntities.TimeClock;

namespace Strive.BusinessLogic.TimeClock
{
    public interface ITimeClockBpl
    {
        Result GetTimeClock(int userId, DateTime date);

        Result SaveTimeClock(Strive.BusinessEntities.TimeClock.TimeClock timeClock);
    }
}
