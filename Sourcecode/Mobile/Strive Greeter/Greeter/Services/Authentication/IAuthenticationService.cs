using System.Threading.Tasks;
using Greeter.DTOs;

namespace Greeter.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<BaseResponse> LoginAsync(string userId, string password);
    }
}