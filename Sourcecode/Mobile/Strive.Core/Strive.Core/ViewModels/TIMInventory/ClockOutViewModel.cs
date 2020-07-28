using System;
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
                Name = EmployeeDetail.FirstName;
                Role = EmployeeData.CurrentRole;
                CurrentDate = GeneralUtils.GetTodayDateString();
                ClockInTime = GetClockInTimeString();
                ClockOutTime = GetClockOutTimeString();
                TotalHours = GetTotalHours();
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        string GetClockOutTimeString()
        {
            return string.Format("{0:hh.mmtt}", EmployeeData.ClockOutTime);
        }

        string GetClockInTimeString()
        {
            return string.Format("{0:hh.mmtt}", EmployeeData.ClockInTime);
        }

        string GetTotalHours()
        {
            TimeSpan difference = EmployeeData.ClockOutTime - EmployeeData.ClockInTime;
            return string.Format("{0:D2}.{1:D2}", difference.Hours, difference.Minutes);
        }
    }
}
