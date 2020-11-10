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
    public class DashboardRal : RalBase
    {
        public DashboardRal(ITenantHelper tenant) : base(tenant) { }
        public List<DashboardGridViewModel> GetDashboardStatistics(int locationId)
        {
            _prm.Add("@LocationId", locationId);
            var result = db.Fetch<DashboardGridViewModel>(EnumSP.DashboardStatistics.USPGETDASHBOARDSTATISTICS.ToString(), _prm);
            return result;
        }
    }
}
