using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class SignUpViewModel : BaseViewModel
    {

        #region Commands

        public async void SignUpCommand()
        {
            if (!Validations.validateEmail(signUpEmail) || String.IsNullOrEmpty(signUpEmail))
            {
                _userDialog.Alert(Strings.ValidEmail);
            }
            else if (!Validations.validatePhone(signUpMobile) || String.IsNullOrEmpty(signUpMobile))
            {
                _userDialog.Alert(Strings.ValidMobile);
            }
            else if (String.IsNullOrEmpty(signUpName))
            {
                _userDialog.Alert(Strings.ValidName);
            }
            else if (string.IsNullOrEmpty(signUpPassword) || string.IsNullOrEmpty(signUpConfirmPassword))
            {
                _userDialog.Alert(Strings.PasswordEmpty);
            }
            else if (!string.Equals(signUpPassword, signUpConfirmPassword))
            {
                _userDialog.Alert(Strings.PasswordsNotSame);
            }
            else
            {
                CustomerSignUp customerSignUp = new CustomerSignUp();

                customerSignUp.emailId = signUpEmail;
                customerSignUp.mobileNumber = signUpMobile;
                customerSignUp.passwordHash = signUpPassword;
                customerSignUp.createdDate = createdDate ;
                
                _userDialog.ShowLoading("Loading...", Acr.UserDialogs.MaskType.Gradient);
                
                var response = await AdminService.CustomerSignUp(customerSignUp);
                await _navigationService.Close(this);
            }
        }


        #endregion Commands

        #region Properties

        public string signUpMobile { get; set; }
        public string signUpEmail { get; set; }
        public string signUpName { get; set; }
        public string signUpPassword { get; set; }
        public string signUpConfirmPassword { get; set; }
        public int authId { get; set; } 
        public int lockoutEnabled { get; set; } 
        public int securityStamp { get; set; } 
        public string passwordHash { get; set; }
       
        public string createdDate = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");
        public string userGuid { get; set; }
        public int emailVerified { get; set; } 
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
