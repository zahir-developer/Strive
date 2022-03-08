using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;

namespace Strive.Core.Services.Implementations
{
    public class PaymentApiService : IPaymentService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();

        public Task<PayAuthResp> PaymentAuth(PaymentAuthRequest req)
        {
            return _restClient.MakeApiCall<PayAuthResp>(ApiUtils.PAYMENT_AUTH, HttpMethod.Post, req);
        }
        public Task<PayAuthResponse> PaymentAuthProfile(PaymentAuthReq req)
        {
            return _restClient.MakeApiCall<PayAuthResponse>(ApiUtils.PAYMENT_AUTH_PROFILE, HttpMethod.Post, req);
        }
        public Task<PayAuthResponse> PaymentCapture(PaymentCaptureReq req)
        {
            return _restClient.MakeApiCall<PayAuthResponse>(ApiUtils.PAYMENT_CAPTURE, HttpMethod.Post, req);
        }
        public Task<PayAuthResponse> PaymentAuthTips(PaymentAuthTip req)
        {
            return _restClient.MakeApiCall<PayAuthResponse>(ApiUtils.PAYMENT_AUTHTIP, HttpMethod.Post, req);
        }
        public Task<TipPaymentResponse> AddPayment(AddPaymentReq req)
        {
            return _restClient.MakeApiCall<TipPaymentResponse>(ApiUtils.ADD_PAYMENT, HttpMethod.Post, req);

        }
    }
}
