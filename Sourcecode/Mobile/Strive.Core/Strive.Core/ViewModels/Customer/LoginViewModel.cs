using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Resources;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.Customer
{
    public class LoginViewModel : BaseViewModel
    {


        public LoginViewModel()
        {
        }


        public void DoLoginCommand()
        {
            if (Validations.validateEmail(loginEmailPhone) 
                || Validations.validatePhone(loginEmailPhone))
            {
               
            }
            else if(String.IsNullOrEmpty(loginEmailPhone))
            {
                
            }
            else
            {

            }
            
            if(String.IsNullOrEmpty(loginPassword))
            {

            }
            //AdminService.Login("Admin", "Admin");
        }


        #region Commands
        
        public async void SignUpCommand()
        {
            await _navigationService.Navigate<SignUpViewModel>();
        }

        public async void ForgotPasswordCommand()
        {
            await _navigationService.Navigate<ForgotPasswordViewModel>();
        }

        #endregion Commands


        #region Properties

        public string loginEmailPhone { get; set; }
        public string loginPassword { get; set; }
        public bool rememberMe { get; set; }
        public string Title
        {
            get
            {
                return Strings.CUSTOMER_APP_TITLE;
            }
            set{ }
        }

        public string Login
        {
            get
            {
                return Strings.Login;
            }
            set { }
        }

        public string RememberPassword 
        {
            get
            {
                return Strings.RememberPassword;
            }
            set { }
        }

        public string ForgotPassword
        {
            get
            {
                return Strings.ForgotPassword_loginScreen;
            }
            set { }
        }

        public string NewAccount
        {
            get 
            {
                return Strings.NewAccount;
            }
            set { }
        }

        public string SignUp
        {
            get
            {
                return Strings.SignUp;
            }
            set { }
        }

        #endregion Properties
    }
}
