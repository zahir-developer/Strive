using System;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface ICheckoutApi
    {
        Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req);
    }

    public class CheckoutApi
    {
        readonly IApiService apiService = new ApiService();

        public Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req)
        {
            return apiService.DoApiCall<CheckoutResponse>(Urls.CHECKOUTS, HttpMethod.Post, null, req);
        }
    }
}
