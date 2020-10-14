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

namespace Strive.Core.Services.Implementations
{
    public class CarwashLocationService : ICarwashLocationService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();
        public async Task<Locations> GetAllCarWashLocations()
        {
            return await _restClient.MakeApiCall<Locations>(ApiUtils.URL_GET_ALL_LOCATION_ADDRESS, HttpMethod.Get);
        }
    }
}
