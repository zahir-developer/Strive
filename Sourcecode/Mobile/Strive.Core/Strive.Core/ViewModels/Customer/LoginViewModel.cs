using System;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Customer
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
        }

       

        public void DoLogin()
        {
            AdminService.Login("Admin", "Admin");
        }

        #region Properties

        public string loginEmailPhone { get; set; }
        public string loginPassword { get; set; }
        public bool rememberMe { get; set; } = false;
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
