using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Owner
{
    public class OTPViewModel : BaseViewModel
    {
        public OTPViewModel()
        {
            this.resetEmail = CustomerInfo.resetEmail;
        }
        #region Commands
        public async void VerifyCommand()
        {
            if (OTPValue.Length < 4)
            {
                _userDialog.Alert(Strings.enterOTPError);
            }
            else
            {
                CustomerInfo.OTP = this.OTPValue;
                _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
                var otpResponse = await AdminService.CustomerVerifyOTP(new CustomerVerifyOTPRequest(resetEmail, OTPValue));
                if (otpResponse != null)
                {
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
        }
        public async void resendOTPCommand()
        {
            var responseResult = await AdminService.CustomerForgotPassword(resetEmail);
            if (responseResult != null)
            {
                if (responseResult.Status == "true")
                {
                    _userDialog.Toast(Strings.OTPSentEmail);
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
