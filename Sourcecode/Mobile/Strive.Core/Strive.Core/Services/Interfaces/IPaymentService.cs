using System;
using System.Threading.Tasks;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;

namespace Strive.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<TipPaymentResponse> AddPayment(AddPaymentReq req);
        Task<PayAuthResp> PaymentAuth(PaymentAuthRequest req);
        Task<PayAuthResponse> PaymentAuthProfile(PaymentAuthReq req);
        Task<PayAuthResponse> PaymentCapture(PaymentCaptureReq req);
        Task<PayAuthResponse> PaymentAuthTips(PaymentAuthTip req);
    }
}
