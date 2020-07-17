using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
           
        }

        #region Properties
        private bool _isPasswordSecure = true;

        public bool isPasswordSecure {
            get
            {
                return _isPasswordSecure;
            }
            set
            {
                SetProperty(ref _isPasswordSecure, value);
            }
        }

        public string UserId { get; set; } = "caradmin@strive.com";

        public string Password { get; set; } = "pass@123";

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
            if (await ValidateCredentialsAsync())
            {
                //_userDialog.ShowLoading("Logging in", Acr.UserDialogs.MaskType.Gradient);
                //await _navigationService.Navigate<RootViewModel>();
                await AdminService.EmployeeLogin(new EmployeeLoginRequest(UserId, Password));
                //_userDialog.HideLoading();
            }
        }

        async Task<bool> ValidateCredentialsAsync()
        {
            bool isValid = true;
            if(!Validations.validateEmail(UserId))
            {
                await _userDialog.AlertAsync("Invalid Email", "Alert");
                return !isValid;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                await _userDialog.AlertAsync("Enter Password", "Alert");
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
