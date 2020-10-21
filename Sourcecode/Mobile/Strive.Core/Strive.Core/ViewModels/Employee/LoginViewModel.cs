using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {

        }


        #region Commands

        public async void ForgotPasswordCommand()
        {
            
        }

        public async Task DoLoginCommand()
        {
            if (validateCommand())
            {
                _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
                var loginResponse = await AdminService.EmployeeLogin( new EmployeeLoginRequest(loginEmailPhone, loginPassword));
                if (loginResponse != null)
                {
                    ApiUtils.Token = loginResponse.Token;
                    if (!string.IsNullOrEmpty(loginResponse.Token))
                    {
                       
                    }
                }
                else
                {
                    _userDialog.Alert(Strings.UsernamePasswordIncorrect);
                }
                _userDialog.HideLoading();
            }
            else
            {
                _userDialog.Alert(Strings.UsernamePasswordIncorrect);
            }


        }
        public bool validateCommand()
        {
            bool isValid;

            if (Validations.validateEmail(loginEmailPhone)
                || Validations.validatePhone(loginEmailPhone))
            {
                isValid = true;
            }
            else if (String.IsNullOrEmpty(loginEmailPhone)
                || String.IsNullOrEmpty(loginPassword))
            {
                isValid = false;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        public void RememberMeButtonCommand()
        {
            rememberMe = !rememberMe;
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
            set { }
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

