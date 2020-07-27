﻿using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Customer
{
    public class OTPViewModel : BaseViewModel
    {
        public OTPViewModel()
        {
            this.resetEmail = CustomerOTPInfo.resetEmail;
        }
        #region Commands
        public async void VerifyCommand()
        {
            if (string.IsNullOrEmpty(OTPValue))
            {
                _userDialog.Alert(Strings.enterOTPError);
            }
            else
            {
                CustomerOTPInfo.OTP = this.OTPValue;
                _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
                var otpResponse = await AdminService.CustomerVerifyOTP(new CustomerVerifyOTPRequest(resetEmail,OTPValue));
                if (otpResponse.Status == "true")
                {
                    await _navigationService.Close(this);
                    await _navigationService.Navigate<ConfirmPasswordViewModel>();
                }
                else
                {
                    _userDialog.Alert(Strings.enterOTPError);
                }
                
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
                return Strings.OTPSentEmail;
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

        public string resetEmail { get; set; }

        #endregion Properties



    }
}
