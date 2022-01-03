using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {

        }

        #region Properties
        private bool _isPasswordSecure = true;

        public bool isPasswordSecure
        {
            get
            {
                return _isPasswordSecure;
            }
            set
            {
                SetProperty(ref _isPasswordSecure, value);
            }
        }

        private string _UserId;

        public string UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                SetProperty(ref _UserId, value);
            }
        }

        private string _Password;

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                SetProperty(ref _Password, value);
            }
        }

        public string UserIdText
        {
            get
            {
                return Strings.UserId;
            }
        }
        public string PasswordText
        {
            get
            {
                return Strings.Password;
            }
        }

        public string Title
        {
            get
            {
                return Strings.TIM_APP_TITLE;
            }
            set
            { }
        }

        private EmployeeLoginRequest request;
        #endregion Properties

        #region Commands

        public async Task LoginCommand()
        {
            EmployeeData.ClockInStatus = null;
            if (await ValidateCredentialsAsync())
            {
                _userDialog.ShowLoading(Strings.LoggingIn, Acr.UserDialogs.MaskType.Gradient);
                var response = await AdminService.EmployeeLogin(new EmployeeLoginRequest(UserId, Password));
                if (response.Token != null)
                {
                    EmployeeData.EmployeeDetails = response.EmployeeDetails;
                    ApiUtils.Token = response.Token;
                    UserId = string.Empty;
                    Password = string.Empty;

                    //var request = new TimeClockRequest()
                    //{
                    //    locationId = 1,
                    //    employeeId = response.EmployeeDetails.EmployeeLogin.EmployeeId,
                    //    roleId = 5,
                    //    date = DateUtils.GetTodayDateString()
                    //};
                    //var status = await AdminService.GetClockInStatus(request);
                    //if (status.TimeClock.Count > 0)
                    //{
                    //    var SingleTimeClock = new TimeClockRoot();
                    //    SingleTimeClock.TimeClock = status.TimeClock[0];
                    //    EmployeeData.ClockInStatus = SingleTimeClock;
                    //}

                    await _navigationService.Navigate<LocationSelectViewModel>();
                    //_UserId = "";
                    //_Password = "";
                    //await RaiseAllPropertiesChanged();
                }
            }
        }

        async Task<bool> ValidateCredentialsAsync()
        {
            bool isValid = true;
            if (!Validations.validateEmail(UserId))
            {
                await _userDialog.AlertAsync(Strings.ValidEmail, Strings.Alert);
                return !isValid;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                await _userDialog.AlertAsync(Strings.EnterPassword, Strings.Alert);
                return !isValid;
            }
            return isValid;
        }

        public void PasswordToggleCommand()
        {
            isPasswordSecure = !_isPasswordSecure;
        }

        public void DoLogin()
        {
            AdminService.Login("Admin", "Admin");
        }

        #endregion Commands
    }
}
