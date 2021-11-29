using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Strive.Core.Services.Implementations
{
    public class CarwashLocationService : ICarwashLocationService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();
        public async Task<Locations> GetAllCarWashLocations()
        {
            return await _restClient.MakeApiCall<Locations>(ApiUtils.URL_GET_ALL_LOCATION_ADDRESS, HttpMethod.Get);
        }

        public async Task<washLocations> GetAllLocationStatus(LocationStatusReq request)
        {
            var uriBuilder = new UriBuilder(ApiUtils.URL_ALL_LOCATION_STATUS);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["Date"] = request.Date;
            query["LocationId"] = request.LocationId.ToString();
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.Uri.PathAndQuery.ToString();
            return await _restClient.MakeApiCall<washLocations>(url, HttpMethod.Get);
        }
    }
}
