using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using Greeter.Storyboards;
using Newtonsoft.Json;

namespace Greeter.Modules.Pay
{
    public partial class PaymentViewController
    {

        public int JobID;
        public string TicketNumber;
        public string Make;
        public string Model;
        public string Color;
        public string ServiceName;
        public string AdditionalServiceName;
        public float Amount;
        public string CustName;
        public string Barcode;
        public string CheckInTime;
        public string CheckOutTime;
        public string PhoneNumber;
        public string ShopPhoneNumber;
        public ServiceType ServiceType;
        //public bool IsFromNewService = true;
        public CreateServiceRequest Service;
        //public bool IsMembershipService;

        string CardNumber;

        public PaymentViewController()
        {

        }

        async Task PayAsync(string cardNo, string expiryDate, short ccv, float tipAmount)
        {
            var totalAmnt = Amount + tipAmount;

            if (cardNo.IsEmpty() || expiryDate.IsEmpty() || ccv == 0)
            {
                ShowAlertMsg(Common.Messages.CARD_DETAILS_EMPTY_MISSING_MSG);
                return;
            }

            if (cardNo.Length < 15)
            {
                ShowAlertMsg(Common.Messages.CARD_NUMBER_LENGTH_WARNING_MSG);
                return;
            }

            try
            {
                var paymentAuthReq = new PaymentAuthReq
                {
                    PaymentDetail = new PaymentDetail()
                    {
                        Account = cardNo,
                        Expiry = expiryDate,
                        CCV = ccv,
                        Amount = Amount
                    }
                };

                //Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));
                ShowActivityIndicator();

                var apiService = new PaymentApiService();

                var paymentAuthResponse = await apiService.PaymentAuth(paymentAuthReq);

                if (paymentAuthResponse != null && paymentAuthResponse.SucessType.Equals("b", StringComparison.OrdinalIgnoreCase) || paymentAuthResponse.SucessType.Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    ShowAlertMsg(paymentAuthResponse.ErrorMessage);
                }

                if (paymentAuthResponse.IsSuccess() && paymentAuthResponse.SucessType.Equals("a", StringComparison.OrdinalIgnoreCase))
                {
                    var paymentCaptureReq = new PaymentCaptureReq
                    {
                        AuthCode = paymentAuthResponse?.Authcode,
                        RetRef = paymentAuthResponse?.Retref,
                        Amount = totalAmnt,
                    };

                    Debug.WriteLine("" + JsonConvert.SerializeObject(paymentCaptureReq));
                    var captureResponse = await apiService.PaymentCapture(paymentCaptureReq);

                    if (captureResponse.IsSuccess())
                    {
                        var generalApiService = new GeneralApiService();
                        var paymentStatusResponse = await generalApiService.GetGlobalData("PAYMENTSTATUS");
                        //Debug.WriteLine("Payment Status Response : " + JsonConvert.SerializeObject(paymentStatusResponse));
                        var paymentStatusId = paymentStatusResponse?.Codes.First(x => x.Name.Equals(PaymentStatus.Success.ToString())).ID ?? -1;

                        var paymentTypeResponse = await generalApiService.GetGlobalData("PAYMENTTYPE");
                        //Debug.WriteLine("Payment Type Response : " + JsonConvert.SerializeObject(paymentTypeResponse));

                        var paymentTypeId = paymentTypeResponse?.Codes.First(x => x.Name.Equals(PaymentType.Card.ToString())).ID ?? -1;

                        var addPaymentReqReq = new AddPaymentReq
                        {
                            SalesPaymentDto = new SalesPaymentDto()
                            {
                                JobPayment = new JobPayment()
                                {
                                    JobID = JobID,
                                    Amount = Amount,
                                    PaymentStatus = paymentStatusId
                                },

                                JobPaymentDetails = new List<JobPaymentDetail>() {
                                    new JobPaymentDetail()
                                    {
                                        Amount = Amount,
                                        PaymentType = paymentTypeId
                                    }
                                }
                            },
                            LocationID = AppSettings.LocationID,
                            TicketNumber = TicketNumber
                        };

                        if (tipAmount != 0)
                        {
                            var tipsTypeId = paymentTypeResponse?.Codes.First(x => x.Name.Equals(PaymentType.Tips.ToString(), StringComparison.OrdinalIgnoreCase)).ID ?? -1;

                            var tipAmountObj = new JobPaymentDetail()
                            {
                                Amount = tipAmount,
                                PaymentType = tipsTypeId
                            };

                            addPaymentReqReq.SalesPaymentDto.JobPaymentDetails.Add(tipAmountObj);
                        }

                        //Debug.WriteLine("Add pay req : " + JsonConvert.SerializeObject(addPaymentReqReq));

                        var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
                        HideActivityIndicator();

                        if (paymentResponse.IsSuccess())
                        {
                            var nc = NavigationController;
                            var navigationViewControllers = NavigationController.ViewControllers.ToList();
                            navigationViewControllers.RemoveAt(navigationViewControllers.Count - 1);
                            NavigationController.ViewControllers = navigationViewControllers.ToArray();

                            var vc = (PaymentSucessViewController)GetViewController(GetHomeStorybpard(), nameof(PaymentSucessViewController));
                            vc.TicketID = TicketNumber;
                            vc.Make = Make;
                            vc.Model = Model;
                            vc.Color = Color;
                            vc.Barcode = Barcode;
                            vc.CheckInTime = CheckInTime;
                            vc.CheckOutTime = CheckOutTime;
                            vc.ServiceName = ServiceName;
                            vc.Amount = totalAmnt;
                            vc.CustomerName = CustName;
                            vc.AdditionalServiceName = AdditionalServiceName;
                            vc.Service = Service;
                            vc.ServiceType = ServiceType;
                            vc.CardNumber = cardNo;
                            vc.ShopPhoneNumber = ShopPhoneNumber;
                            //vc.IsMembershipService = IsMembershipService;
                            //vc.IsFromNewService = IsFromNewService;
                            nc.PushViewController(vc, true);
                            //NavigateToWithAnim(vc);
                        }
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
            Account,
            Tips
        }
    }
}
