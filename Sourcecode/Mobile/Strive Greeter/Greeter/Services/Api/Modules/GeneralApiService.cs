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
        //Task<LocationsResponse> GetLocations();
        Task<MakeResponse> GetAllMake();
        Task<GlobalDataResponse> GetGlobalData(string dataType);
        Task<LocationWashTimeResponse> GetLocationWashTime(long locationId);
        //Task<LocationWashTimeResponse> GetAllLocationWashTime();
    }

    public class GeneralApiService : IGeneralApiService
    {
        readonly IApiService apiService = SingleTon.ApiService;

        //public Task<LocationsResponse> GetLocations()
        //{
        //    return apiService.DoApiCall<LocationsResponse>(Urls.LOCATIONS);
        //}

        public Task<MakeResponse> GetAllMake()
        {
            return apiService.DoApiCall<MakeResponse>(Urls.ALL_MAKE);
        }

        public Task<GlobalDataResponse> GetGlobalData(string dataType)
        {
            var url = Urls.GLOBAL_DATA + dataType;
            return apiService.DoApiCall<GlobalDataResponse>(url);
        }

        public Task<LocationWashTimeResponse> GetLocationWashTime(long locationId = 0)
        {
            //string formattedCurrentDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API);
            string formattedCurrentDate = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT_FOR_API);
            var parameters = new Dictionary<string, string>() { { nameof(locationId), locationId.ToString()} ,{ "date" , formattedCurrentDate} };
            return apiService.DoApiCall<LocationWashTimeResponse>(Urls.GET_LOCATION_WASH_TIME, HttpMethod.Get, parameters);
        }

        //public Task<LocationWashTimeResponse> GetAllLocationWashTime()
        //{
        //    //string formattedCurrentDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API);
        //    string formattedCurrentDate = DateTime.Now.ToString();
        //    var parameters = new Dictionary<string, string>() { { "locationId", "0" }, { "date", formattedCurrentDate } };
        //    return apiService.DoApiCall<LocationWashTimeResponse>(Urls.GET_LOCATION_WASH_TIME, HttpMethod.Get, parameters);
        //}
    }
}
