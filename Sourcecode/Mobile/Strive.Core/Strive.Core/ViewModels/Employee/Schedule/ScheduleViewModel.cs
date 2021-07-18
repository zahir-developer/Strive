using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Owner;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Employee.Schedule
{
    public class ScheduleViewModel : BaseViewModel
    {
        #region Properties

        public ScheduleDetail scheduleList { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetScheduleList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);            
            var result = await AdminService.GetScheduleList(new ScheduleRequest
            {
                startDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
                endDate = getEndDate().ToString("yyy-MM-dd"),
                locationId = 0,
            });
            if(result == null)
            {
                _userDialog.Toast("No relatable data");
            }
            else
            {
                scheduleList = new ScheduleDetail();
                scheduleList.ScheduleDetailViewModel = new List<ScheduleDetailViewModel>();
                scheduleList.ScheduleEmployeeViewModel = new ScheduleEmployeeViewModel();
                scheduleList.ScheduleHoursViewModel = new ScheduleHoursViewModel();
                scheduleList = result.ScheduleDetail;
            }
            _userDialog.HideLoading();
        }

        public DateTime getEndDate()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return endDate;
        }
        #endregion Commands
    }
}
