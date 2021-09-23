using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface IGeneralApiService
    {
        Task<LocationsResponse> GetLocations();
        Task<MakeResponse> GetAllMake();
        Task<GlobalDataResponse> GetGlobalData(string dataType);
        Task<LocationWashTimeResponse> GetLocationWashTime(long locationId);
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

        public Task<LocationWashTimeResponse> GetLocationWashTime(long locationId)
        {
            var parameters = new Dictionary<string, string>() { { nameof(locationId), locationId.ToString() } };
            return apiService.DoApiCall<LocationWashTimeResponse>(Urls.GET_LOCATION_WASH_TIME, HttpMethod.Get, parameters);
        }
    }
}
