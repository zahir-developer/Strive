using Acr.UserDialogs;
using MvvmCross;
using Strive.Core.Resources;


namespace Strive.Core.ViewModels.Customer
{
   public class ConfirmPasswordViewModel : BaseViewModel
    {

      //  IUserDialogs dialogs = Mvx.IoCProvider.Resolve<IUserDialogs>();
        #region Commands
        
        public async void SubmitCommand()
        {
            if(string.Equals(NewPassword,ConfirmPassword))
            {

            }
            else
            {
              //  dialogs.Alert(Strings.PasswordsNotSame);
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

        public string Submit
        {
            get
            {
                return Strings.Submit;
            }
            set { }
        }
        
        #endregion

    }
}
