using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class ForgotPasswordViewModel : BaseViewModel
    {

        #region Commands

        public async void GetOTPCommand()
        {
            if(Validations.validateEmail(resetEmail))
            {
                await AdminService.CustomerForgotPassword(resetEmail);
                await _navigationService.Navigate<OTPViewModel>();
            }
            else
            {
                _userDialog.Alert(Strings.ValidEmail);
            }        
        }

        #endregion Commands

        #region Properties

        public string resetEmail { get; set; }
        public string GetOTP
        {
            get
            {
                return Strings.GetOTP;
            }
        }
        public string ReceiveOTP
        {
            get
            {
               return Strings.ReceiveOTP;
            }
        }
        public string ForgotPassword
        {
            get 
            {
                return Strings.ForgotPassword_fpScreen;
            }
            set { }
        }

        #endregion Properties
    }
}
