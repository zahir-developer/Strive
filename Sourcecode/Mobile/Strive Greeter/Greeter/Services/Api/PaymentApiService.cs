using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public class PaymentApiService
    {
        readonly IApiService apiService = new ApiService();

        public Task<PayAuthResponse> PaymentAuth(PaymentAuthReq req)
        {
            return apiService.DoApiCall<PayAuthResponse>(Urls.PAYMENT_AUTH, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> PaymentCapture(PaymentCaptureReq req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.PAYMENT_CAPTURE, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> AddPayment(AddPaymentReq req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.ADD_PAYMENT, HttpMethod.Post, null, req);
        }
    }
}
