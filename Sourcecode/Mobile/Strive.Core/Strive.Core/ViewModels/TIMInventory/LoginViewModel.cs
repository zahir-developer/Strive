using System;
using System.Threading.Tasks;
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

        public string UserId { get; set; }

        public string Password { get; set; }

        public string Title
        {
            get
            {
                return Strings.TIM_APP_TITLE;
            }
            set
            { }
        }
        #endregion Properties

        #region Commands
        public async Task NavigationToClockInCommand()
        {
            //if(Validations.validateEmail(UserId))
            //{
            await _navigationService.Navigate<RootViewModel>();
            //}
            //else
            //{
            //    _userDialog.Alert("Invalid Email","Alert");
            //}
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
