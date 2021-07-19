using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using Greeter.Storyboards;
using Newtonsoft.Json;

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
        public string CustName;
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
                var paymentAuthReq = new PaymentAuthReq
                {
                    PaymentDetail = new PaymentDetail()
                    {
                        Account = cardNo,
                        Expiry = expiryDate,
                        CCV = ccv,
                        Amount = Amount,
                    }
                };

                //Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));
                ShowActivityIndicator();

                var apiService = new PaymentApiService();

                var paymentAuthResponse = await apiService.PaymentAuth(paymentAuthReq);

                if (paymentAuthResponse.IsSuccess())
                {
                    var paymentCaptureReq = new PaymentCaptureReq
                    {
                        AuthCode = paymentAuthResponse?.Authcode,
                        RetRef = paymentAuthResponse?.Retref,
                        Amount = Amount,
                    };

                    Debug.WriteLine(JsonConvert.SerializeObject(paymentCaptureReq));
                    var captureResponse = await apiService.PaymentCapture(paymentCaptureReq);

                    if (captureResponse.IsSuccess())
                    {
                        var generalApiService = new GeneralApiService();
                        var paymentStatusResponse = await generalApiService.GetGlobalData("PAYMENTSTATUS");
                        Debug.WriteLine("Payment Status Response : " + JsonConvert.SerializeObject(paymentStatusResponse));
                        var paymentStatusId = paymentStatusResponse?.Codes.First(x => x.Name.Equals(PaymentStatus.Success.ToString())).ID ?? -1;

                        var paymentTypeResponse = await generalApiService.GetGlobalData("PAYMENTTYPE");
                        Debug.WriteLine("Payment Type Response : " + JsonConvert.SerializeObject(paymentTypeResponse));
                        var paymentTypeId = paymentStatusResponse?.Codes.First(x => x.Name.Equals(PaymentType.Card.ToString())).ID ?? -1;

                        var addPaymentReqReq = new AddPaymentReq
                        {
                            JobPayment = new JobPayment()
                            {
                                JobId = JobID,
                                Amount = Amount,
                                PaymentStatus = paymentStatusId
                            },

                            JobPaymentDetail = new JobPaymentDetail()
                            {
                                 Amount = Amount,
                                 PaymentType = paymentTypeId
                            }
                        };

                        Debug.WriteLine(JsonConvert.SerializeObject(addPaymentReqReq));

                        //        var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
                        //        HideActivityIndicator();

                        //        if (paymentResponse.IsSuccess())
                        //        {
                        //var vc = (PaymentSucessViewController)GetViewController(GetHomeStorybpard(), nameof(PaymentSucessViewController));
                        //vc.TicketID = JobID;
                        //vc.Make = Make;
                        //vc.Model = Model;
                        //vc.Color = Color;
                        //vc.ServiceName = ServiceName;
                        //vc.Amount = Amount;
                        //vc.IsFromNewService = IsFromNewService;
                        //NavigateToWithAnim(vc);
                        //        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened and the reason is : " + ex.Message);
                HideActivityIndicator();
            }
        }

        public enum PaymentStatus
        {
            Success
        }

        public enum PaymentType
        {
            Card,
            Account
        }
    }
}
