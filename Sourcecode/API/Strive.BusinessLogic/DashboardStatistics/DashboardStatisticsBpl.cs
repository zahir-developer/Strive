using Microsoft.Extensions.Caching.Distributed;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.DashboardStatistics
{
    public class DashboardStatisticsBpl : Strivebase, IDashboardStatisticsBpl
    {
        public DashboardStatisticsBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetDashboardStatisticsForLocationId(int id)
        {
            return ResultWrap(new DashboardStatisticsRal(_tenant).GetDashboardStatisticsForLocationId, id, "GetDashboardStatisticsForLocationId");
        }
    }
}
