using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;

namespace Strive.Core.Services.Implementations
{
    public class GeneralApiService : IGeneralApiService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();

        //public Task<LocationsResponse> GetLocations()
        //{
        //    return apiService.DoApiCall<LocationsResponse>(Urls.LOCATIONS);
        //}

        //public Task<MakeResponse> GetAllMake()
        //{
        //    return apiService.DoApiCall<MakeResponse>(Urls.ALL_MAKE);
        //}

        public Task<GlobalDataResponse> GetGlobalData(string dataType)
        {
            var url = ApiUtils.GLOBAL_DATA + dataType;
            return _restClient.MakeApiCall<GlobalDataResponse>(url, HttpMethod.Get);
        }

        //public Task<LocationWashTimeResponse> GetLocationWashTime(long locationId = 0)
        //{
        //    //string formattedCurrentDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API);
        //    string formattedCurrentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    var parameters = new Dictionary<string, string>() { { nameof(locationId), locationId.ToString() }, { "date", formattedCurrentDate } };
        //    return apiService.DoApiCall<LocationWashTimeResponse>(Urls.GET_LOCATION_WASH_TIME, HttpMethod.Get, parameters);
        //}
    }
}
