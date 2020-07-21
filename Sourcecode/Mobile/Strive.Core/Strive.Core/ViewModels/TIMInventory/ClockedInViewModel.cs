using System;
using System.Threading.Tasks;
using System.Timers;
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
                return "Welcome " + Name; 
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
                CurrentDate = GetTodayDateString();
                ClockInTime = GetClockInTimeString();
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
            EmployeeData.ClockOutTime = DateTime.Now;
            await _navigationService.Navigate<ClockOutViewModel>();
        }

        string GetTodayDateString()
        {
            var Date = DateTime.Now;
            return Date.Day + "/" + Date.Month.ToString("D2") + "/" + Date.Year;
        }

        string GetClockInTimeString()
        {
            return string.Format("{0:hh.mmtt}", EmployeeData.ClockInTime);
        }
    }
}
