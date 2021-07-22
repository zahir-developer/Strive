using Acr.UserDialogs;
using Strive.Core.Models.Owner;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Owner
{
    public class RevenueHomeViewModel:BaseViewModel
    {
        public GetDashboardStatisticsForLocationId statisticsData { get; set; }
        public async Task getStatistics(int locationId)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

            var result = await AdminService.getDashboardServices(new StatisticRequest
            {
                locationId = locationId,
                fromDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
                toDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
            });

            if (result == null)
            {
                _userDialog.Toast("No relatable data");
            }
            else
            {
                statisticsData = new GetDashboardStatisticsForLocationId();
                statisticsData = result.GetDashboardStatisticsForLocationId[0];
            }
            _userDialog.HideLoading();
        }
    }
}
