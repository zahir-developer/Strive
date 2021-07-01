using System;
using System.Threading.Tasks;
using System.Timers;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;
using System.Linq;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockedInViewModel : BaseViewModel
    {
        public ClockedInViewModel()
        {
            //Timer checkForTime = new Timer(15000);
            //checkForTime.Elapsed += new ElapsedEventHandler(checkForTime_Elapsed);
            //checkForTime.Enabled = true;
            GetClockStatus();
            Init();     
        }

        public string Name { get; set; }
        public string Role { get; set; }
        public string CurrentDate { get; set; }
        public string ClockInTime { get; set; }


        public string WelcomeTitle
        {
            get
            {
                return Strings.Welcome + Name; 
            }
            set { }
        }

        async void GetClockStatus()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var request = new TimeClockRequest()
            {
                locationId = EmployeeData.selectedLocationId,
                employeeId = EmployeeData.EmployeeDetails.EmployeeLogin.EmployeeId,
                roleId = 5,
                date = DateUtils.GetTodayDateString()
            };
            var status = await AdminService.GetClockInStatus(request);
            if (status.TimeClock.Count > 0)
            {
                var SingleTimeClock = new TimeClockRoot();
                SingleTimeClock.TimeClock = status.TimeClock[0];
                EmployeeData.ClockInStatus = SingleTimeClock;
            }
            _userDialog.HideLoading();
        }

        void Init()
        {
            if(EmployeeData.EmployeeDetails != null)
            {
                var EmployeeDetail = EmployeeData.EmployeeDetails;
                Name = EmployeeDetail.EmployeeLogin.Firstname + " " + EmployeeDetail.EmployeeLogin.LastName;
                //Role = EmployeeData.EmployeeDetails.EmployeeRoles[0].RoleName;
                Role = EmployeeData.CurrentRole;
                CurrentDate = DateUtils.GetTodayDateString();
                ClockInTime = DateUtils.GetClockInTypeString(EmployeeData.ClockInStatus.TimeClock.inTime);
            }
        }

        void checkForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            NavigateBackCommand();
        }

        public void PrepareClockoutModel()
        {
            EmployeeData.ClockInStatus.TimeClock.outTime = DateUtils.GetStringFromDate(DateTime.Now);
            EmployeeData.ClockInStatus.TimeClock.isActive = false;
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockOutCommand()
        {
            PrepareClockoutModel();
            _userDialog.ShowLoading(Strings.Loading);
            var clockin = await AdminService.SaveClockInTime(EmployeeData.ClockInStatus);
            await _navigationService.Navigate<ClockOutViewModel>();
            await _navigationService.Close(this);
        }
    }
}
