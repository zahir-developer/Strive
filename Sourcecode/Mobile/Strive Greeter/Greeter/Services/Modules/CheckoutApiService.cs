using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface ICheckoutApiService
    {
        Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req);
    }

    public class CheckoutApiService : ICheckoutApiService
    {
        //readonly IApiService apiService = new ApiService();

        public Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req)
        {
            return SingleTon.ApiService.DoApiCall<CheckoutResponse>(Urls.CHECKOUTS, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> HoldCheckout(HoldCheckoutReq req)
        {
            return SingleTon.ApiService.DoApiCall<BaseResponse>(Urls.HOLD_CHECKOUT, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> CompleteCheckout(CompleteCheckoutReq req)
        {
            return SingleTon.ApiService.DoApiCall<BaseResponse>(Urls.COMPLETE_CHECKOUT, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> DoCheckout(DoCheckoutReq req)
        {
            return SingleTon.ApiService.DoApiCall<BaseResponse>(Urls.DO_CHECKOUT, HttpMethod.Post, null, req);
        }
    }
}
