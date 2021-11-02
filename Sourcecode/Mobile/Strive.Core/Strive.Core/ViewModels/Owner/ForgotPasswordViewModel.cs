using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Navigation;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Owner
{
    public class ForgotPasswordViewModel : BaseViewModel
    {

        #region Commands

        public async void GetOTPCommand()
        {
            if (Validations.validateEmail(resetEmail))
            {
                _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
                var responseResult = await AdminService.CustomerForgotPassword(resetEmail);
                if (responseResult == null)
                    return;
                if (responseResult.Status == "true")
                {
                    CustomerInfo.resetEmail = resetEmail;
                    await _navigationService.Close(this);
                    await _navigationService.Navigate<OTPViewModel>();
                }
                else
                {
                    _userDialog.Alert(Strings.NotRegisteredEmail);
                }

            }
           
        }

        public void NavigateBackCommand()
        {
            _navigationService.Close(this);
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
