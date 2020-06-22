using System;
using System.Net.Http;
using System.Threading.Tasks;
using Strive.Core.Models;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;

namespace Strive.Core.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IRestClient _restClient;

        private const string URL_LOGIN = "/api/login";

        public AdminService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<object> Login(string username, string password)
        {
            return await _restClient.MakeApiCall<object>(URL_LOGIN, HttpMethod.Post, new User());
        }
    }
}
