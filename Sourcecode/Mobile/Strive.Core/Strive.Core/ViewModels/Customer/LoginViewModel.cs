using System;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Customer
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
        }

        public string Title
        {
            get
            {
                return Strings.CUSTOMER_APP_TITLE;
            }
            set
            { }
        }

        public void DoLogin()
        {
            AdminService.Login("Admin", "Admin");
        }
    }
}
