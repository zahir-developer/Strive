using Strive.Core.Resources;
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
            await _navigationService.Navigate<OTPViewModel>();
        }

        #endregion Commands

        #region Properties

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
