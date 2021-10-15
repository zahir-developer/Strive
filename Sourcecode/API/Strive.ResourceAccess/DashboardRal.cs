using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Dashboard;
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
        public List<DashboardGridViewModel> GetDashboardStatistics(DashboardDto dashboard)
        {
            _prm.Add("@LocationId", dashboard.locationId);
            _prm.Add("@FromDate", dashboard.FromDate);
            _prm.Add("@ToDate", dashboard.ToDate);
            _prm.Add("@CurrentDate", dashboard.CurrentDate);
            var result = db.Fetch<DashboardGridViewModel>(EnumSP.DashboardStatistics.USPGETDASHBOARDSTATISTICS.ToString(), _prm);
            return result;
        }
        public List<TimeInViewModel> GetAvailablilityScheduleTime(TimeInDto timein)
        {
            _prm.Add("@LocationId", timein.LocationId);
            _prm.Add("@Date", timein.Date);
            var result = db.Fetch<TimeInViewModel>(EnumSP.DashboardStatistics.USPGETAVAILABLETIMESLOT.ToString(), _prm);
            return result;
        }
    }
}
