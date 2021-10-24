using System.Threading.Tasks;
using Greeter.DTOs;

namespace Greeter.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest req);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenReq req);
    }
}