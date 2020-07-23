using Acr.UserDialogs;
using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;


namespace Strive.Core.ViewModels.Customer
{
   public class ConfirmPasswordViewModel : BaseViewModel
    {

        #region Commands
        
        public async void SubmitCommand()
        {
            if(string.Equals(NewPassword,ConfirmPassword))
            {
                 _userDialog.ShowLoading("Loading...",MaskType.Gradient);
                 var resetPasswordResponse = await AdminService.CustomerResetPassword(new CustomerResetPassword(SentOTP,ConfirmPassword,UserId));
            }
            else if(string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                _userDialog.Alert(Strings.PasswordEmpty);
            }
            else
            {
              _userDialog.Alert(Strings.PasswordsNotSame);
            }
        }

        #endregion

        #region Properties

        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPasswordTitle
        {
            get
            {
                return Strings.EnterNewPassword;
            }
            set { }
        }
        public string SentOTP { get; set; }

        public string Submit
        {
            get
            {
                return Strings.Submit;
            }
            set { }
        }
        
        public string UserId { get; set; }
        #endregion

    }
}
