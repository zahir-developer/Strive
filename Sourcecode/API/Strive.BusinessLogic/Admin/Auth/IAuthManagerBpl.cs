using Strive.BusinessEntities;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.Model;
using Strive.Common;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Auth
{
    public interface IAuthManagerBpl
    {
        Result Login(Authentication authentication, string secretKey, string tenantConString);
        Result GenerateTokenByRefreshKey(string token, string refreshToken, string secretKey);
        void Logout(string token, string secretKey);
        //Microsoft.Owin.Security.AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        //Task<Microsoft.AspNet.Identity.Owin.ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<dynamic> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
        int CreateLogin(UserType userType, HtmlTemplate htmlTemplate, string emailId, string mobileNumber);
        Result CreateCustomer(ClientDto client, string conn);
        bool ForgotPassword(string userId);
        Result ResetPassword(ResetPassword resetPassword);
        Result SendOTP(string emailId);
        Result VerifyOTP(string emailId, string otp);
        Result EmailIdExists(string email);
        Result GetModelByMakeId(int makeId);
        Result GetAllMake();
        Result GetAllColor();
    }
}
