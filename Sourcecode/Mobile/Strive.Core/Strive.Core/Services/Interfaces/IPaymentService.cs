using System;
using System.Threading.Tasks;
using Strive.Core.Models;
using Strive.Core.Models.Customer;

namespace Strive.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponsePayment> AddPayment(AddPaymentReq req);
        Task<PayAuthResp> PaymentAuth(PaymentAuthRequest req);
        Task<PayAuthResponse> PaymentAuthProfile(PaymentAuthReq req);
        Task<PayAuthResponse> PaymentCapture(PaymentCaptureReq req);
    }
}
