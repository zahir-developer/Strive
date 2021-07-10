using System;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;

namespace Greeter.Services.Api
{
    public interface IUserApi
    {
        Task<LocationsResponse> GetLocations();
    }

    public class UserApi
    {
        readonly IApiService apiService = new ApiService();

        public Task<LocationsResponse> GetLocations()
        {
            return apiService.DoApiCall<LocationsResponse>(Urls.LOCATIONS);
        }
    }
}
