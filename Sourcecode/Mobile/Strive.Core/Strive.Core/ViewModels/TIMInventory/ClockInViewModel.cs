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
            _RolesList = new List<EmployeeRole>();
            //if (EmployeeData.EmployeeDetails != null)
            //{
            //    List<EmployeeRoleApi> roles = EmployeeData.EmployeeDetails.EmployeeRole;
            //    foreach (var role in roles)
            //    {
            //        if (role.RoleName != null)
            //            AddRole(role.RoleName);
            //    }
            //}
            _RolesList.Add(new EmployeeRole() { Title = "Washer", ImageUri = ImageUtils.ICON_WASHER, Tag = 0, ImageUriHover = ImageUtils.ICON_WASHER });
            _RolesList.Add(new EmployeeRole() { Title = "Cashier", ImageUri = ImageUtils.ICON_CASHIER, Tag = 1, ImageUriHover = ImageUtils.ICON_CASHIER });
            _RolesList.Add(new EmployeeRole() { Title = "Detailer", ImageUri = ImageUtils.ICON_DETAILER, Tag = 2, ImageUriHover = ImageUtils.ICON_DETAILER });
            _RolesList.Add(new EmployeeRole() { Title = "Finish Bay", ImageUri = ImageUtils.ICON_FINISH_BAY, Tag = 3, ImageUriHover = ImageUtils.ICON_FINISH_BAY});
            _RolesList.Add(new EmployeeRole() { Title = "Greet Bay", ImageUri = ImageUtils.ICON_GREET_BAY, Tag = 4, ImageUriHover = ImageUtils.ICON_GREET_BAY });
            _RolesList.Add(new EmployeeRole() { Title = "Manager", ImageUri = ImageUtils.ICON_MANAGER, Tag = 5, ImageUriHover = ImageUtils.ICON_MANAGER });
            _RolesList.Add(new EmployeeRole() { Title = "Runner", ImageUri = ImageUtils.ICON_RUNNER, Tag = 6, ImageUriHover = ImageUtils.ICON_RUNNER });
            _RolesList.Add(new EmployeeRole() { Title = "Unknown", ImageUri = ImageUtils.ICON_UNKNOWN, Tag = 7, ImageUriHover = ImageUtils.ICON_UNKNOWN });
        }

        void AddRole(string roleName)
        {
            roleName = roleName.ToUpper();
            switch(roleName)
            {
                case "WASHER":
                    _RolesList.Add(new EmployeeRole() { Title = "Washer", ImageUri = ImageUtils.ICON_WASHER, Tag = 0, ImageUriHover = ImageUtils.ICON_WASHER });
                    break;
                default:
                    break;
            }
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
            EmployeeData.ClockInTime = DateTime.Now;
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
            return Time.Hour + "." + Time.Minute.ToString("D2") + Time.ToString("tt", CultureInfo.InvariantCulture);
        }
    }
}
