using System.Threading.Tasks;
using Greeter.Extensions;
using Greeter.Services.Authentication;
using Xamarin.Essentials;

namespace Greeter.Modules.Login
{
    public partial class LoginViewController
    {
        readonly IAuthenticationService authenticationService;
        public LoginViewController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        async Task OnLogin()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                //TODO show no network alert
                return;
            }

            if (!IsValidData()) return;

            //var result = await authenticationService.LoginAsync("", "");

            //if(!result.IsSuccess())
            //{
            //TODO show failure message here
            //}

            //TODO handle success
        }

        bool IsValidData()
        {
            return true;
        }
    }
}