using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface IPaymentApiService
    {
        Task<BaseResponse> AddPayment(AddPaymentReq req);
        Task<PayAuthResponse> PaymentAuth(PaymentAuthReq req);
        Task<BaseResponse> PaymentCapture(PaymentCaptureReq req);
    }

    public class PaymentApiService : IPaymentApiService
    {
        //readonly IApiService apiService = new ApiService();

        public Task<PayAuthResponse> PaymentAuth(PaymentAuthReq req)
        {
            return SingleTon.ApiService.DoApiCall<PayAuthResponse>(Urls.PAYMENT_AUTH, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> PaymentCapture(PaymentCaptureReq req)
        {
            return SingleTon.ApiService.DoApiCall<BaseResponse>(Urls.PAYMENT_CAPTURE, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> AddPayment(AddPaymentReq req)
        {
            return SingleTon.ApiService.DoApiCall<BaseResponse>(Urls.ADD_PAYMENT, HttpMethod.Post, null, req);
        }
    }
}
