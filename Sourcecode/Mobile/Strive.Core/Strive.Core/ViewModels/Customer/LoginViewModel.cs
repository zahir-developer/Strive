﻿using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.Customer
{
    public class LoginViewModel : BaseViewModel
    {
        
        public LoginViewModel()
        {
        }

        #region Commands
        
        public async void SignUpCommand()
        {
            //await _navigationService.Navigate<SignUpViewModel>();
        }

        public async void ForgotPasswordCommand()
        {
            await _navigationService.Navigate<ForgotPasswordViewModel>();
        }

        public async Task DoLoginCommand()
        {
            if (validateCommand())
            {
                _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
                var loginResponse = await AdminService.CustomerLogin(new CustomerLoginRequest(loginEmailPhone, loginPassword));
                if(loginResponse != null)
                {
                    ApiUtils.Token = loginResponse.Token;
                    if (!string.IsNullOrEmpty(loginResponse.Token))
                    {
                        await _navigationService.Navigate<DashboardViewModel>();
                    }
                }
                else
                {
                    _userDialog.Alert(Strings.UsernamePasswordIncorrect);
                }
                _userDialog.HideLoading();
            }            
        }
        public bool validateCommand()
        {
            bool isValid ;

            if (Validations.validateEmail(loginEmailPhone)
                || Validations.validatePhone(loginEmailPhone))
            {
                isValid = true;
            }
            else if (String.IsNullOrEmpty(loginEmailPhone)
                || String.IsNullOrEmpty(loginPassword))
            {
                isValid = false;
                _userDialog.Alert("Please provide all the details");
            }
            else
            {
                isValid = false;
                _userDialog.Alert("Please enter a valid UserName ");
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
