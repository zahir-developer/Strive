using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Greeter.Extensions;
using Greeter.Storyboards;

namespace Greeter.Modules.Pay
{
    public partial class PaymentViewController
    {
        public long JobID;
        public string Make;
        public string Model;
        public string Color;
        public string ServiceName;
        public float Amount;
        public bool IsFromNewService = true;

        public PaymentViewController()
        {

        }

        async Task PayAsync(string cardNo, string expiryDate, short ccv, float tipAmount)
        {
            if (cardNo.IsEmpty() || expiryDate.IsEmpty() || ccv == 0)
            {
                ShowAlertMsg(Common.Messages.CARD_DETAILS_EMPTY_MISSING_MSG);
                return;
            }

            Amount += tipAmount;

            try
            {
                //var paymentAuthReq = new PaymentAuthReq
                //{
                //    PaymentDetail = new PaymentDetail()
                //    {
                //        Account = cardNo,
                //        Expiry = expiryDate,
                //        CCV = ccv,
                //        Amount = Amount,
                //    }
                //};

                //Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));

                //ShowActivityIndicator();
                //var paymentAuthResponse = await new PaymentApiService().PaymentAuth(paymentAuthReq);

                //if (paymentAuthResponse.IsSuccess())
                //{
                //    var paymentCaptureReq = new PaymentCaptureReq
                //    {
                //            AuthCode = "",
                //            RetRef = "",
                //            Amount = amount,
                //    };
                //Debug.WriteLine(JsonConvert.SerializeObject(paymentCaptureReq));
                //    var captureResponse = await new PaymentApiService().PaymentCapture(paymentCaptureReq);

                //    if (captureResponse.IsSuccess())
                //    {
                //        var addPaymentReqReq = new AddPaymentReq
                //        {
                //            JobPayment = new JobPayment() {
                //                JobId = JobID,
                //                Amount = amount,
                //                PaymentStatus = 34
                //            }
                //        };

                //Debug.WriteLine(JsonConvert.SerializeObject(addPaymentReqReq));
                //        var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
                //        HideActivityIndicator();

                //        if (paymentResponse.IsSuccess())
                //        {
                            var vc = (PaymentSucessViewController)GetViewController(GetHomeStorybpard(), nameof(PaymentSucessViewController));
                vc.TicketID = JobID;
                vc.Make = Make;
                vc.Model = Model;
                vc.Color = Color;
                vc.ServiceName = ServiceName;
                vc.Amount = Amount;
                vc.IsFromNewService = IsFromNewService;
                NavigateToWithAnim(vc);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened and the reason is : " + ex.Message);
                HideActivityIndicator();
            }
        }
    }
}
