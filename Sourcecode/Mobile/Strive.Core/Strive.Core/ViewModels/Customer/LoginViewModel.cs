﻿using System;
using System.Threading.Tasks;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Customer
{
    public class LoginViewModel : BaseViewModel
    {

        
        public LoginViewModel()
        {
        }


        public void DoLoginCommand()
        {
            
          
            //AdminService.Login("Admin", "Admin");
        }


        #region Commands
        
        public async void SignUpCommand()
        {
            await _navigationService.Navigate<SignUpViewModel>();
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
                return Strings.ForgotPassword;
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
