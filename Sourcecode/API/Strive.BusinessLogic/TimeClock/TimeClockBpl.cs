using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.TimeClock;
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

        public Result GetTimeClock(int userId, DateTime date)
        {
            var result = new TimeClockRal(_tenant).GetTimeClock(userId, date);
            _resultContent.Add(result.WithName("TimeClock"));
            _result = Helper.BindSuccessResult(_resultContent);

            return _result;
        }

        public Result SaveTimeClock(Strive.BusinessEntities.TimeClock.TimeClock clockTime)
        {
            var result = new TimeClockRal(_tenant).SaveTimeClock(clockTime);
            _resultContent.Add(result.WithName("Success"));
            _result = Helper.BindSuccessResult(_resultContent);

            return new Result();
        }
    }
}
