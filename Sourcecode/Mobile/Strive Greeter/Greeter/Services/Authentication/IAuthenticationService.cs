using System.Threading.Tasks;
using Greeter.DTOs;

namespace Greeter.Services.Authentication
{
    public interface IAuthenticationService
    {
        //Task<LoginResponse> LoginAsync(string userId, string password);
        Task<LoginResponse> DoLogin(LoginRequest req);
    }
}