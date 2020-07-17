using Strive.BusinessEntities;
using Strive.Common;

namespace Strive.BusinessLogic.Auth
{
    public interface IAuthManagerBpl
    {
        Result Login(Authentication authentication, string secretKey, string tenantConString);
        Result GenerateTokenByRefreshKey(string token, string refreshToken, string secretKey);
        void Logout(string token, string secretKey);
    }
}
