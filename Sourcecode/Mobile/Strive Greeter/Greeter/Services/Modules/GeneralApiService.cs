using System;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;

namespace Greeter.Services.Api
{
    public interface IGeneralApiService
    {
        Task<LocationsResponse> GetLocations();
        Task<MakeResponse> GetAllMake();
        Task<GlobalDataResponse> GetGlobalData(string dataType);
    }

    public class GeneralApiService : IGeneralApiService
    {
        readonly IApiService apiService = SingleTon.ApiService;

        public Task<LocationsResponse> GetLocations()
        {
            return apiService.DoApiCall<LocationsResponse>(Urls.LOCATIONS);
        }

        public Task<MakeResponse> GetAllMake()
        {
            return apiService.DoApiCall<MakeResponse>(Urls.ALL_MAKE);
        }

        public Task<GlobalDataResponse> GetGlobalData(string dataType)
        {
            var url = Urls.GLOBAL_DATA + dataType;
            return apiService.DoApiCall<GlobalDataResponse>(url);
        }
    }
}
