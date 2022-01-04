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
                var ClockInStatus = EmployeeData.ClockInStatus;
                Name = EmployeeDetail.EmployeeLogin.Firstname;
                Role = EmployeeData.CurrentRole;
                CurrentDate = DateUtils.GetTodayDateString();
                ClockInTime = DateUtils.GetClockInTypeString(EmployeeData.ClockInTime);
                ClockOutTime = DateUtils.GetClockInTypeString(EmployeeData.ClockInStatus.TimeClock.outTime);
                TotalHours = DateUtils.GetTimeDifferenceString(ClockInStatus.TimeClock.outTime, EmployeeData.ClockInTime);
            }
        }

        public async Task LogoutCommand()
        {
            await _navigationService.Navigate<LoginViewModel>();
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }
    }
}
