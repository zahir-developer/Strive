using System;
using System.Threading.Tasks;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
        }

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

        public string Title
        {
            get
            {
                return Strings.TIM_APP_TITLE;
            }
            set
            { }
        }

        public async Task NavigationToClockInCommand()
        {
            await _navigationService.Navigate<RootViewModel>();
        }

        public void PasswordToggleCommand()
        {
            isPasswordSecure = !_isPasswordSecure;
        }

        public void DoLogin()
        {
            AdminService.Login("Admin", "Admin");
        }
    }
}
