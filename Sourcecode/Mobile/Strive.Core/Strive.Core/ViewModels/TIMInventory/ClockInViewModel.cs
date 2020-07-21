using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using System.Collections.Generic;
using Strive.Core.Utils.TimInventory;
using System;
using System.Globalization;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockInViewModel : BaseViewModel
    {
        private List<EmployeeRole> _RolesList;

        public List<EmployeeRole> RolesList {
            get
            {
                return _RolesList;
            }
            private set
            {
                SetProperty(ref _RolesList, value);
            }
        }

        private EmployeeRole FirstSelectedRole { get; set; }

        private EmployeeRole SecondSelectedRole { get; set; }

        public ClockInViewModel()
        {
            InitList();
        }

        private void InitList()
        {
            if (EmployeeData.EmployeeDetails != null)
            {

            }
            _RolesList = new List<EmployeeRole>
            {
                new EmployeeRole("Washer","icon-washer",0,"icon-washer"),
                new EmployeeRole("Detailer","icon-cashier",1,"icon-cashier"),
                new EmployeeRole("Runner","icon-detailer",2,"icon-detailer"),
                new EmployeeRole("Cashier","icon-finish-bay",3,"icon-finish-bay"),
                new EmployeeRole("Manager","icon-greetbay",4,"icon-greetbay"),
                new EmployeeRole("Greet Bay","icon-manager",5,"icon-manager"),
                new EmployeeRole("Finish Bay","icon-runner",6,"icon-runner"),
                new EmployeeRole("Unknown","icon-unknown",7,"icon-unknown")
            };
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockedInCommand()
        {
            if (FirstSelectedRole == null)
            {
                await _userDialog.AlertAsync("Select a Role to Clock in");
                return;
            }
            EmployeeData.CurrentRole = FirstSelectedRole.Title;
            EmployeeData.ClockInTime = GetClockInTime();
            await _navigationService.Navigate<ClockedInViewModel>();
        }

        public void RoleDecisionCommand(int index)
        {
            if (FirstSelectedRole == null)
            {
                FirstSelectedRole = RolesList[index];
                RoleSelectedCommand(FirstSelectedRole);
            }
            else
            {
                SecondSelectedRole = RolesList[index];
                if(FirstSelectedRole == SecondSelectedRole)
                {
                    RoleDeSelectedCommand(FirstSelectedRole);
                    FirstSelectedRole = null;
                    SecondSelectedRole = null;
                }
                else
                {
                    RoleDeSelectedCommand(FirstSelectedRole);
                    RoleSelectedCommand(SecondSelectedRole);
                    FirstSelectedRole = SecondSelectedRole;
                    SecondSelectedRole = null;
                }
            }
           
            RaiseAllPropertiesChanged();
        }

        void RoleSelectedCommand(EmployeeRole role)
        {
            string imagepath = role.ImageUriHover;
            imagepath += "-hover";
            role.ImageUri = imagepath;
        }

        public void RoleDeSelectedCommand(EmployeeRole role)
        {
            role.ImageUri = role.ImageUriHover;
        }

        string GetClockInTime()
        {
            var Time = DateTime.Now;
            return Time.Hour + "." + Time.Minute + Time.ToString("tt", CultureInfo.InvariantCulture);
        }
    }
}
