﻿using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface ICheckoutApi
    {
        Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req);
    }

    public class CheckoutApi : ICheckoutApi
    {
        readonly IApiService apiService = new ApiService();

        public Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req)
        {
            return apiService.DoApiCall<CheckoutResponse>(Urls.CHECKOUTS, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> HoldCheckout(HoldCheckoutReq req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.DO_PAYMENT, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> Pay(DoPaymentReq req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.HOLD_CHECKOUT, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> CompleteCheckout(CompleteCheckoutReq req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.COMPLETE_CHECKOUT, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> DoCheckout(DoCheckoutReq req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.DO_CHECKOUT, HttpMethod.Post, null, req);
        }
    }
}
