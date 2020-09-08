using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.TimeClock;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.TimeClock
{
    public class TimeClockBpl : Strivebase, ITimeClockBpl
    {
        public TimeClockBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper)
        {
        }

        public Result GetTimeClock(TimeClockDto timeClock)
        {

            return ResultWrap(new TimeClockRal(_tenant).GetTimeClock, timeClock, "TimeClock");
        }

        public Result SaveTimeClock(Strive.BusinessEntities.Model.TimeClockModel timeClock)
        {
            return ResultWrap(new TimeClockRal(_tenant).SaveTimeClock, timeClock, "Result");
        }
    }
}
