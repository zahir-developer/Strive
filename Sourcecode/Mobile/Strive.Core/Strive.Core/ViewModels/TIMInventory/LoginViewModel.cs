using System;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.TIMInventory
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
                return Strings.TIM_APP_TITLE;
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
