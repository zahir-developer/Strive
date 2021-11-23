using System;
using System.Threading.Tasks;
using Strive.Core.Models;
using Strive.Core.Models.Customer;

namespace Strive.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponsePayment> AddPayment(AddPaymentReq req);
        Task<PayAuthResponse> PaymentAuth(PaymentAuthReq req);
        Task<PayAuthResponse> PaymentCapture(PaymentCaptureReq req);
    }
}
