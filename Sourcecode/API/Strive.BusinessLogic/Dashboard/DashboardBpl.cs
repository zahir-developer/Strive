using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.Dashboard;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.DashboardStatistics
{
    public class DashboardBpl : Strivebase, IDashboardBpl
    {
        public DashboardBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetDashboardStatistics(DashboardDto dashboard)
        {
            return ResultWrap(new DashboardRal(_tenant).GetDashboardStatistics, dashboard, "GetDashboardStatisticsForLocationId");
        }
    }
}
