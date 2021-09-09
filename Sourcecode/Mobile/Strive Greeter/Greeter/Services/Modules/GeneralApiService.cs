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
        //Task<BaseResponse> GetLocationWashTime(long locationId);
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

        //public Task<BaseResponse> GetLocationWashTime(long locationId)
        //{
        //    var url = Urls.GET_LOCATION_WASH_TIME + locationId;
        //    return apiService.DoApiCall<BaseResponse>(url);
        //}
    }
}
