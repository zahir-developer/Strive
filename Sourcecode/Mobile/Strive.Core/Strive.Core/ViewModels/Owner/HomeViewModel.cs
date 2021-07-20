using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Owner;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Owner
{
    public class HomeViewModel : BaseViewModel
    {
        public GetDashboardStatisticsForLocationId statisticsData { get; set; }
        public ScheduleModel dbSchedule { get; set; }

        public async Task getStatistics()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

            var result = await AdminService.getDashboardServices(new StatisticRequest
            {
                locationId = 1,
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

        public async Task getDashboardSchedule()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

            var jobDate = (System.DateTime.Now).ToString("yyy-MM-dd");
            var result = await AdminService.getDashboardSchedule(jobDate,1);

            if(result == null)
            {
                _userDialog.Toast("No relatable data");

            }
            else
            {
                dbSchedule = new ScheduleModel();
                dbSchedule = result;
            }
            _userDialog.HideLoading();
        }

    }
}
 