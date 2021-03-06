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

        public Task<LoginResponse> LoginAsync(string userId, string password)
        {
            IRestRequest request = new RestRequest(Urls.LOGIN, HttpMethod.Post);
            request.AddBody(new { userId, password });
            return networkService.ExecuteAsync<LoginResponse>(request);
        }
    }
}