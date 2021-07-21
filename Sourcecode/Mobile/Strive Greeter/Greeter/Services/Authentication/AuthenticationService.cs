using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using Greeter.Services.Network;

namespace Greeter.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        //readonly INetworkService networkService;
        //public AuthenticationService(INetworkService networkService)
        //    => this.networkService = networkService;

        //public Task<LoginResponse> LoginAsync(string userId, string password)
        //{
        //    IRestRequest request = new RestRequest(Urls.LOGIN, HttpMethod.Post);
        //    request.AddBody(new { userId, password });
        //    return networkService.ExecuteAsync<LoginResponse>(request);
        //}

        readonly IApiService apiService = new ApiService();

        public Task<LoginResponse> LoginAsync(LoginRequest req)
        {
            return apiService.DoApiCall<LoginResponse>(Urls.LOGIN, HttpMethod.Post, null, req, false);
        }

        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenReq req)
        {
            return apiService.DoApiCall<RefreshTokenResponse>(Urls.REFRESH_TOKEN, HttpMethod.Post, null, req, false);
        }

        public async Task<RefreshTokenResponse> ResfreshApiCall(string token, string refreshToken)
        {
            var req = new RefreshTokenReq()
            {
                Token = token,
                RefreshToken = refreshToken
            };

            var response = await RefreshToken(req);

            if (response.IsSuccess())
            {
                return response;
            }

            return null;
        }
    }
}