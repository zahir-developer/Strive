using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross;
using Strive.Core.Models;
using Strive.Core.Models.TimInventory;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;

namespace Strive.Core.Services.Implementations
{
    public class LocationService : ILocationService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();

        public LocationService()
        {
        }

        public async Task<Locations> GetAllLocationAddress()
        {
            return await _restClient.MakeApiCall<Locations>(ApiUtils.URL_GET_ALL_LOCATION_ADDRESS, HttpMethod.Get);
        }
    }
}
