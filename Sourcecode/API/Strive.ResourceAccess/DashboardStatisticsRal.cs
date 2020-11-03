using Strive.BusinessEntities;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class DashboardStatisticsRal : RalBase
    {
        public DashboardStatisticsRal(ITenantHelper tenant) : base(tenant) { }
        public DashboardGridViewModel GetDashboardStatisticsForLocationId(int id)
        {
            _prm.Add("@LocationId", id);
            var result = db.FetchSingle<DashboardGridViewModel>(EnumSP.DashboardStatistics.USPGETDASHBOARD.ToString(), _prm);
            return result;
        }
    }
}
