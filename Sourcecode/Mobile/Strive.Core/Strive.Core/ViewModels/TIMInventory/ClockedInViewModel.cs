using System;
using System.Threading.Tasks;
using System.Timers;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;
using System.Linq;
using System.Collections.Generic;

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
                roleId = EmployeeData.SelectedRoleId,
                date = DateUtils.GetTodayDateString()
            };
            var status = await AdminService.GetClockInStatus(request);
            if (status.timeClock.Count > 0)
            {
                var SingleTimeClock = new TimeClockRoot();
                foreach(var item in status.timeClock)
                {
                    var inTime = item.inTime.Substring(0, 19);
                    if(EmployeeData.ClockInTime == inTime)
                    {
                        SingleTimeClock.TimeClock = item;
                        EmployeeData.ClockInStatus = SingleTimeClock;
                    }
                }
                //SingleTimeClock.TimeClock = status.timeClock[0];
                //EmployeeData.ClockInStatus = SingleTimeClock;
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
                ClockInTime = DateUtils.GetClockInTypeString(EmployeeData.ClockInTime);
                //ClockInTime = DateUtils.GetClockInTypeString(DateUtils.GetStringFromDate(DateTime.Now));
            }
        }

        void checkForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            NavigateBackCommand();
        }

        public TimeClockSave PrepareClockoutModel()
        {
            EmployeeData.ClockInStatus.TimeClock.outTime = DateUtils.GetStringFromDate(DateTime.Now);
            EmployeeData.ClockInStatus.TimeClock.isActive = false;

            List<TimeClock> clockInRequestList = new List<TimeClock>();            
            clockInRequestList.Add(EmployeeData.ClockInStatus.TimeClock);

            TimeClockRootList request = new TimeClockRootList()
            {
                timeClock = clockInRequestList
            };

            TimeClockSave saveRequest = new TimeClockSave()
            {
                timeClock = request
            };
            return saveRequest;
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockOutCommand()
        {
            var clockOutRequest = PrepareClockoutModel();
            _userDialog.ShowLoading(Strings.Loading);
            var clockin = await AdminService.SaveClockInTime(clockOutRequest);
            await _navigationService.Navigate<ClockOutViewModel>();
            await _navigationService.Close(this);
        }
    }
}
