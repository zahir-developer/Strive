using System;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Network;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Greeter.Modules.User
{
    public class LoginViewModel : MvxViewModel
    {
        public LoginViewModel()
        {
        }

        // Todo : Mvvm cross view model methods. we can use in case of need otherwose we can remove it.
        //public override void Prepare()
        //{
        //    // This is the first method to be called after construction
        //}

        //public override Task Initialize()
        //{
        //    // Async initialization, YEY!

        //    return base.Initialize();
        //}

        private string email;
        private string pswd;

        public string Email { get => email; set => email = value; }

        public string Pswd { get => pswd; set => pswd = value; }


        //Click Commands
        public IMvxCommand LoginCmd => new MvxCommand(DoLogin);
        async void DoLogin()
        {
            // Validate Fields
            if (email.IsEmpty() && pswd.IsEmpty())
            {
                //ShowAlertMsg(Common.Messages.USER_ID_AND_PSWD_EMPTY);
                return;
            }
            if (email.IsEmpty())
            {
                //ShowAlertMsg(Common.Messages.USER_ID_EMPTY);
                return;
            }
            if (pswd.IsEmpty())
            {
                //ShowAlertMsg(Common.Messages.PSWD_EMPTY);
                return;
            }

            var req = new LoginRequest() { Email = email, Pswd = pswd };
            var response = await new ApiService(new NetworkService()).DoLogin(req);
            AppSettings.Token = response.AuthToken;
        }
    }
}
