using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class OTPViewModel : BaseViewModel
    {
        #region Commands
        public async void VerifyCommand()
        {
            if (string.IsNullOrEmpty(OTPValue))
            {
                _userDialog.Alert("Enter OTP Value");
            }
            else
            {
                await _navigationService.Navigate<ConfirmPasswordViewModel>();
            }
        }



        #endregion Commands

        #region Properties

        public string EnterOTP
        {
            get
            {
                return Strings.EnterOTP;
            }
            set { }
        }
        public string SentOTP
        {
            get
            {
                return Strings.OTPSent;
            }
            set { }
        }
        public string NotReceiveOTP
        {
            get 
            {
                return Strings.NotReceiveOTP;
            }
            set { }
        }
        public string ResendOTP
        {
            get
            {
                return Strings.ResendOTP;
            }
            set { }
        }
        public string VerifyOTP
        {
            get
            {
                return Strings.OTPVerify;
            }
            set { }
        }

        public string OTPValue { get; set; }

        #endregion Properties



    }
}
