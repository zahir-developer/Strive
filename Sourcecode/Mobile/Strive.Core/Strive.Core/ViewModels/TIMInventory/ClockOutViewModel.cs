﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockOutViewModel : BaseViewModel
    {
        public ClockOutViewModel()
        {
            Init();
        }

        public string Name { get; set; }
        public string Role { get; set; }
        public string CurrentDate { get; set; }
        public string ClockInTime { get; set; }
        public string ClockOutTime { get; set; }
        public string TotalHours { get; set; }

        public string WelcomeTitle
        {
            get
            {
                return Strings.WelcomeBack + Name + "!";
            }
            set { }
        }

        void Init()
        {
            if (EmployeeData.EmployeeDetails != null)
            {
                var EmployeeDetail = EmployeeData.EmployeeDetails;
                var ClockInStatus = EmployeeData.ClockInStatus;
                Name = EmployeeDetail.FirstName;
                Role = EmployeeData.CurrentRole;
                CurrentDate = DateUtils.GetTodayDateString();
                ClockInTime = DateUtils.GetClockInTypeString(EmployeeData.ClockInStatus.inTime);
                ClockOutTime = DateUtils.GetClockInTypeString(EmployeeData.ClockInStatus.outTime);
                TotalHours = DateUtils.GetTimeDifferenceString(ClockInStatus.outTime, ClockInStatus.inTime);
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }
    }
}
