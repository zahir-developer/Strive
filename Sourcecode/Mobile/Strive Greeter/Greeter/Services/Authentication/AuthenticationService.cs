using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly INetworkService networkService;
        public AuthenticationService(INetworkService networkService)
            => this.networkService = networkService;

        public Task<BaseResponse> LoginAsync(string userId, string password)
        {
            IRestRequest request = new RestRequest(Urls.LOGIN_API, HttpMethod.Post);
            request.AddBody(new { userId, password });
            return networkService.ExecuteAsync<BaseResponse>(request);
        }
    }
}