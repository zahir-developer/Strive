﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Employee.Detailer;
using Strive.Core.Models.Owner;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;

namespace Strive.Core.ViewModels.Employee.Schedule
{
    public class ScheduleViewModel : BaseViewModel
    {
        private string date = (System.DateTime.Now).ToString("yyy-MM-dd");
        public static string StartDate { get; set; }
        public status DetailerList { get; set; }
        #region Properties

        public ScheduleDetail scheduleList { get; set; }
        //(System.DateTime.Now).ToString("yyy-MM-dd")
        #endregion Properties

        #region Commands

        public async Task GetScheduleList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            if (StartDate!=null)
            {
                date = StartDate;
            }
            
            var result = await AdminService.GetScheduleList(new ScheduleRequest
            {
                startDate = date,
                endDate = getEndDate().ToString("yyy-MM-dd"),
                locationId = 0,
                employeeId = EmployeeTempData.EmployeeID,
            });
            if(result == null)
            {
                _userDialog.Toast("No relatable data");
            }
            else
            {
                if(result.ScheduleDetail.ScheduleDetailViewModel == null)
                {
                    _userDialog.Toast("No relatable data");
                }
                scheduleList = new ScheduleDetail();
                scheduleList.ScheduleDetailViewModel = new List<ScheduleDetailViewModel>();
                scheduleList.ScheduleEmployeeViewModel = new ScheduleEmployeeViewModel();
                scheduleList.ScheduleHoursViewModel = new ScheduleHoursViewModel();
                scheduleList = result.ScheduleDetail;
            }
            _userDialog.HideLoading();
        }
        public async Task GetDetailer(int employeeid,string jobdate)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            DetailerList = await AdminService.GetEmployeeDetailer(employeeid,jobdate);
            if (DetailerList.Status.Count == 0)
            {
                _userDialog.Toast("No relatable data");
            }
            _userDialog.HideLoading();
        }


        public DateTime getEndDate()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddYears(10).AddDays(-1); //Some future date

            return endDate;
        }

        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }

        #endregion Commands
    }
}
