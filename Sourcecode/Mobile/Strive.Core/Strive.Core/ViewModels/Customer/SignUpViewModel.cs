using Acr.UserDialogs;
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
            if(Validations.validateEmail(signUpEmail) || String.IsNullOrEmpty(signUpEmail))
            {

            }
            if(Validations.validatePhone(signUpMobile))
            {

            }
            if(String.IsNullOrEmpty(signUpName))
            {

            }
            if (string.Equals(signUpPassword, signUpConfirmPassword))
            {

            }
            else
            {

            }
        }


        #endregion Commands

        #region Properties

        public string signUpMobile { get; set; }
        public string signUpEmail { get; set; }
        public string signUpName { get; set; }
        public string signUpPassword { get; set; }
        public string signUpConfirmPassword { get; set; }
        
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
