using Strive.BusinessEntities;
using Strive.Common;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Auth
{
    public interface IAuthManagerBpl
    {
        Result Login(Authentication authentication, string secretKey, string tenantConString);
        Result GenerateTokenByRefreshKey(string token, string refreshToken, string secretKey);
        void Logout(string token, string secretKey);
        Microsoft.Owin.Security.AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<Microsoft.AspNet.Identity.Owin.ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<dynamic> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
    }
}
