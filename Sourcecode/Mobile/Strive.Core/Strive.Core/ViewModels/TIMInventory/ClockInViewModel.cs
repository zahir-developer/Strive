using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using System.Collections.Generic;
using Strive.Core.Utils.TimInventory;
using System;
using System.Globalization;
using Strive.Core.Resources;
using System.Collections.ObjectModel;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockInViewModel : BaseViewModel
    {
        private List<EmployeeRole> _RolesList;
        //private ObservableCollection<EmployeeRole> _RolesList;

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

        public TimeClockRootList clockInStatus { get; set; }

        public ClockInViewModel()
        {
            InitList();
        }

        private void InitList()
        {
            _RolesList = new List<EmployeeRole>();
            if (EmployeeData.EmployeeDetails != null)
            {
                List<EmployeeRoleApi> roles = EmployeeData.EmployeeDetails.EmployeeRoles;
                foreach (var role in roles)
                {
                    if (role.RoleName != null)
                        AddRole(role.RoleName);
                }
            }
            //_RolesList.Add(new EmployeeRole() { Title = Strings.Washer, ImageUri = ImageUtils.ICON_WASHER, Tag = 0, ImageUriHover = ImageUtils.ICON_WASHER });
            //_RolesList.Add(new EmployeeRole() { Title = Strings.Cashier, ImageUri = ImageUtils.ICON_CASHIER, Tag = 1, ImageUriHover = ImageUtils.ICON_CASHIER });
            //_RolesList.Add(new EmployeeRole() { Title = Strings.Detailer, ImageUri = ImageUtils.ICON_DETAILER, Tag = 2, ImageUriHover = ImageUtils.ICON_DETAILER });
            //_RolesList.Add(new EmployeeRole() { Title = Strings.FinishBay, ImageUri = ImageUtils.ICON_FINISH_BAY, Tag = 3, ImageUriHover = ImageUtils.ICON_FINISH_BAY});
            //_RolesList.Add(new EmployeeRole() { Title = Strings.GreetBay, ImageUri = ImageUtils.ICON_GREET_BAY, Tag = 4, ImageUriHover = ImageUtils.ICON_GREET_BAY });
            //_RolesList.Add(new EmployeeRole() { Title = Strings.Manager, ImageUri = ImageUtils.ICON_MANAGER, Tag = 5, ImageUriHover = ImageUtils.ICON_MANAGER });
            //_RolesList.Add(new EmployeeRole() { Title = Strings.Runner, ImageUri = ImageUtils.ICON_RUNNER, Tag = 6, ImageUriHover = ImageUtils.ICON_RUNNER });
            //_RolesList.Add(new EmployeeRole() { Title = Strings.Unknown, ImageUri = ImageUtils.ICON_UNKNOWN, Tag = 7, ImageUriHover = ImageUtils.ICON_UNKNOWN });
        }

        void AddRole(string roleName)
        {
            roleName = roleName.ToUpper();
            switch(roleName)
            {
                case "WASHER":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.Washer, ImageUri = ImageUtils.ICON_WASHER, Tag = 6, ImageUriHover = ImageUtils.ICON_WASHER });
                    break;
                case "CASHIER":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.Cashier, ImageUri = ImageUtils.ICON_CASHIER, Tag = 4, ImageUriHover = ImageUtils.ICON_CASHIER });
                    break;
                case "DETAILER":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.Detailer, ImageUri = ImageUtils.ICON_DETAILER, Tag = 5, ImageUriHover = ImageUtils.ICON_DETAILER });
                    break;
                case "FINISH BAY":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.FinishBay, ImageUri = ImageUtils.ICON_FINISH_BAY, Tag = 9, ImageUriHover = ImageUtils.ICON_FINISH_BAY });
                    break;
                case "GREET BAY":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.GreetBay, ImageUri = ImageUtils.ICON_GREET_BAY, Tag = 8, ImageUriHover = ImageUtils.ICON_GREET_BAY });
                    break;
                case "MANAGER":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.Manager, ImageUri = ImageUtils.ICON_MANAGER, Tag = 3, ImageUriHover = ImageUtils.ICON_MANAGER });
                    break;
                case "ADMIN":
                    _RolesList.Add(new EmployeeRole() { Title = "Admin", ImageUri = ImageUtils.ICON_MANAGER, Tag = 1, ImageUriHover = ImageUtils.ICON_MANAGER });
                    break;
                case "RUNNER":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.Runner, ImageUri = ImageUtils.ICON_RUNNER, Tag = 7, ImageUriHover = ImageUtils.ICON_RUNNER });
                    break;
                case "UNKNOWN":
                    _RolesList.Add(new EmployeeRole() { Title = Strings.Unknown, ImageUri = ImageUtils.ICON_UNKNOWN, Tag = 10, ImageUriHover = ImageUtils.ICON_UNKNOWN });
                    break;
                case "OPERATOR":
                    _RolesList.Add(new EmployeeRole() { Title = "Operator", ImageUri = ImageUtils.ICON_UNKNOWN, Tag = 2, ImageUriHover = ImageUtils.ICON_UNKNOWN });
                    break;
                case "CUSTOMER":
                    _RolesList.Add(new EmployeeRole() { Title = "Customer", ImageUri = ImageUtils.ICON_UNKNOWN, Tag = 11, ImageUriHover = ImageUtils.ICON_UNKNOWN });
                    break;
                default:
                    break;
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }

        public async Task NavigateClockedInCommand()
        {
            if (FirstSelectedRole == null)
            {
                await _userDialog.AlertAsync(Strings.SelectRoleAlert);
                return;
            }
            EmployeeData.CurrentRole = FirstSelectedRole.Title;
            EmployeeData.SelectedRoleId = FirstSelectedRole.Tag;
            var clockInRequest = PrepareClockInRequest();            
            var inTimeString = DateUtils.GetStringFromDate(DateTime.Now);
            EmployeeData.ClockInTime = inTimeString.Substring(0, 19);
            
            _userDialog.ShowLoading(Strings.Loading);
            var clockin = await AdminService.SaveClockInTime(clockInRequest);
            await _navigationService.Navigate<ClockedInViewModel>();
            await _navigationService.Close(this);
        }

        private TimeClockSave PrepareClockInRequest()
        {
            List<TimeClock> clockInRequestList = new List<TimeClock>();
            TimeClock clockInRequest = new TimeClock()
            {
                timeClockId = 0,
                employeeId = EmployeeData.EmployeeDetails.EmployeeLogin.EmployeeId,
                locationId = EmployeeData.selectedLocationId,               
                roleId = FirstSelectedRole.Tag,
                eventDate = DateUtils.GetStringFromDate(DateTime.Now),
                inTime = DateUtils.GetStringFromDate(DateTime.Now),
                //outTime = DateUtils.GetStringFromDate(DateTime.Now),
                outTime = null,
                eventType = 1,
                updatedFrom = "",
                status = true,
                comments = "",
                isActive = true,
                isDeleted = false,
                createdBy = 0,
                createdDate = DateUtils.GetStringFromDate(DateTime.Now),
                updatedBy = 0,
                updatedDate = DateUtils.GetStringFromDate(DateTime.Now)
            };
            clockInRequestList.Add(clockInRequest);
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
            _userDialog.Toast("You have selected " + role.Title +" role");
        }

        public void RoleDeSelectedCommand(EmployeeRole role)
        {
            role.ImageUri = role.ImageUriHover;
            _userDialog.Toast("You have deselected " + role.Title + " role");
        }               

        public async Task getClockInStatus()
        {
            EmployeeData.SelectedRoleId = FirstSelectedRole.Tag;
            EmployeeData.CurrentRole = FirstSelectedRole.Title;
            var request = new TimeClockRequest()
            {
                locationId = EmployeeData.selectedLocationId,
                employeeId = EmployeeData.EmployeeDetails.EmployeeLogin.EmployeeId,
                roleId = FirstSelectedRole.Tag,
                date = DateUtils.GetTodayDateString()
            };
            var status = await AdminService.GetClockInStatus(request);

            clockInStatus = status;                       
        }

        public async void NavToClockOut()
        {
            await _navigationService.Navigate<ClockedInViewModel>();
        }
    }
}
