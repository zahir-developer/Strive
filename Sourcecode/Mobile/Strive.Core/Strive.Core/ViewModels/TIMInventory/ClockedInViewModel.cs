using System;
using System.Threading.Tasks;
using System.Timers;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockedInViewModel : BaseViewModel
    {
        public ClockedInViewModel()
        {
            //Timer checkForTime = new Timer(15000);
            //checkForTime.Elapsed += new ElapsedEventHandler(checkForTime_Elapsed);
            //checkForTime.Enabled = true;
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

        void Init()
        {
            if(EmployeeData.EmployeeDetails != null)
            {
                var EmployeeDetail = EmployeeData.EmployeeDetails;
                Name = EmployeeDetail.FirstName + " " + EmployeeDetail.LastName;
                Role = EmployeeData.CurrentRole;
                CurrentDate = DateUtils.GetTodayDateString();
                ClockInTime = DateUtils.GetClockInTypeString(EmployeeData.ClockInStatus.inTime);
            }
        }

        void checkForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            NavigateBackCommand();
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockOutCommand()
        {
            EmployeeData.ClockInStatus.outTime = DateUtils.GetStringFromDate(DateTime.UtcNow);
            var clockin = await AdminService.SaveClockInTime(EmployeeData.ClockInStatus);
            await _navigationService.Navigate<ClockOutViewModel>();
        }
    }
}
