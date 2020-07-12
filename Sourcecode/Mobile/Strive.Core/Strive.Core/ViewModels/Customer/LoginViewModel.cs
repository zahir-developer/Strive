using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using Strive.Core.Resources;
using Xamarin.Essentials;

namespace Strive.Core.ViewModels.Customer
{
    public class LoginViewModel : BaseViewModel
    {
        public static IMvxNavigationService _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        public LoginViewModel()
        {
           
        }

        public void DoLogin()
        {
            AdminService.Login("Admin", "Admin");
        }
      
        public async void signUpCommand()
        {
            await _navigationService.Navigate<SignUpViewModel>();
        }
        
        #region Properties

        public string loginEmailPhone { get; set; }
        public string loginPassword { get; set; }
        public bool rememberMe { get; set; } 
        public string Title
        {
            get
            {
                return Strings.CUSTOMER_APP_TITLE;
            }
            set
            { }
        }
       
        #endregion Properties
    }
}
