using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Services.Implementations;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.Customer
{
    public class ScheduleViewModel : BaseViewModel
    {
        #region Properties

        public VehicleList scheduleVehicleList { get; set; }

        public PastClientServices pastClientServices { get; set; }
        public ServiceHistoryModel pastServiceHistory { get; set; }
        public ServiceHistoryModel pastServiceHistory1 { get; set; }

        //Properties Used in Tip Feature
        public double WashTip { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string ProfileId { get; set; }
        public string AccountId { get; set; }
        public static int VehicleId { get; set; }
        public static long Jobid { get; set; }
        public static string TicketNumber { get; set; }
        public static int JobPaymentId { get; set; }
        #endregion Properties

        #region Commands

        public async Task GetScheduleVehicleList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            scheduleVehicleList = new VehicleList();
            scheduleVehicleList.Status = new List<VehicleDetail>();
            CustomerVehiclesInformation.vehiclesList = new VehicleList();
            CustomerVehiclesInformation.vehiclesList.Status = new List<VehicleDetail>();
            scheduleVehicleList = await AdminService.GetClientVehicle(CustomerInfo.ClientID);
            if (scheduleVehicleList == null || scheduleVehicleList.Status.Count == 0)
            {
                _userDialog.Alert("No associated vehicles were found.");
            }
            else
            {
                CustomerVehiclesInformation.vehiclesList = scheduleVehicleList;
            }
            _userDialog.HideLoading();
        }

        public async Task<PastClientServices> GetPastDetailsServices()
        {
            var result = await AdminService.GetPastClientServices(CustomerInfo.ClientID);
            if (result == null)
            {
                return result = null;
            }
            else
            {
                pastClientServices = new PastClientServices();
                pastClientServices.PastClientDetails = new List<PastClientDetails>();
                pastClientServices = result;
                return pastClientServices;
            }
        }


        public async Task GetPastServiceDetails()
        {
            _userDialog.ShowLoading(Strings.Loading);
            
            int ClientId = CustomerInfo.ClientID;
            string JobType = "Detail";
            int LocationId = 0;
            string JobDate = string.Empty;
            ServiceHistoryModel result = await AdminService.GetSchedulePastService(JobType, JobDate, LocationId, ClientId);
            if(result == null)
            {
                _userDialog.Toast("No Schedules have been found !");
            }
            else
            {
                pastServiceHistory = result;
            }
            _userDialog.HideLoading();
        }
        public async Task GetPastWashDetails()
        {
            _userDialog.ShowLoading(Strings.Loading);

            int ClientId = CustomerInfo.ClientID;
            string JobType = "Wash";
            int LocationId = 0;
            string JobDate = string.Empty;
            ServiceHistoryModel result = await AdminService.GetSchedulePastService(JobType, JobDate, LocationId, ClientId);
            if (result == null)
            {
                _userDialog.Toast("No Schedules have been found !");
            }
            else
            {
                pastServiceHistory = result;
            }
            _userDialog.HideLoading();
        }

        public void LogoutCommand()
        {
            var confirmconfig = new ConfirmConfig
            {
                Title = Strings.LogoutTitle,
                Message = Strings.LogoutMessage,
                CancelText = Strings.LogoutCancelButton,
                OkText = Strings.LogoutSuccessButton,
                OnAction = success =>
                {
                    if (success)
                    {
                        CustomerInfo.Clear();
                        _navigationService.Close(this);
                        _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
                    }
                }

            };
            _userDialog.Confirm(confirmconfig);
        }
        public async void TipPayment()
        {
            CustomerVehiclesInformation.completeVehicleDetails = new ClientVehicleRootView();
            CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails = new VehicleMembershipDetailsView();
            CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership = new ClientVehicleMembershipView();
            CustomerVehiclesInformation.completeVehicleDetails = await AdminService.GetVehicleMembership(VehicleId);
            if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null && CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.cardNumber != null)
            {

                CardNumber = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.cardNumber;
                AccountId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.accountId;
                ProfileId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.profileId;
                ExpiryDate = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.expiryDate;
            }
            //_navigationService.Navigate<PaymentViewModel>();

            var paymentAuthReq = new PaymentAuthRequest
            {
                CardConnect = new object(),
                PaymentDetail = new PaymentDetail()
                {
                    Account = "6011000995500000",//CardNumber,
                    Expiry = ExpiryDate,
                    Amount = (float)WashTip,
                    OrderID = ""
                },
                BillingDetail = new BillingDetail()
                {

                } 
            };

            //Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));
            _userDialog.ShowLoading();


            var apiService = new PaymentApiService();
            var paymentAuthResponse = await apiService.PaymentAuth(paymentAuthReq);

            if (paymentAuthResponse != null && paymentAuthResponse.SucessType.Equals("b", StringComparison.OrdinalIgnoreCase) || paymentAuthResponse.SucessType.Equals("c", StringComparison.OrdinalIgnoreCase))
            {
                _userDialog.Alert(paymentAuthResponse.ErrorMessage);
            }
            Console.WriteLine(paymentAuthResponse.IsSuccess());
            if ((!paymentAuthResponse.IsSuccess()) && paymentAuthResponse.SucessType.Equals("a", StringComparison.OrdinalIgnoreCase))
            {
                var paymentCaptureReq = new PaymentCaptureReq
                {
                    AuthCode = paymentAuthResponse?.Authcode,
                    RetRef = paymentAuthResponse?.Retref,
                    Amount = (float)WashTip,
                };

                //Debug.WriteLine("" + JsonConvert.SerializeObject(paymentCaptureReq));
                var captureResponse = await apiService.PaymentCapture(paymentCaptureReq);

                if (!captureResponse.IsSuccess())
                {
                    var generalApiService = new GeneralApiService();
                    var paymentStatusResponse = await generalApiService.GetGlobalData("PAYMENTSTATUS");
                    //Debug.WriteLine("Payment Status Response : " + JsonConvert.SerializeObject(paymentStatusResponse));
                    var paymentStatusId = paymentStatusResponse?.Codes.First(x => x.Name.Equals(PaymentStatus.Success.ToString())).ID ?? -1;

                    var paymentTypeResponse = await generalApiService.GetGlobalData("PAYMENTTYPE");
                    //Debug.WriteLine("Payment Type Response : " + JsonConvert.SerializeObject(paymentTypeResponse));

                    var paymentTypeId = paymentTypeResponse?.Codes.First(x => x.Name.Equals(PaymentType.Tips.ToString())).ID ?? -1;

                    var addPaymentReqReq = new AddPaymentReq
                    {
                            JobPaymentDetails = new List<JobPaymentDetail>() {
                                    new JobPaymentDetail()
                                    {
                                        Amount = (float)WashTip,
                                        PaymentType = paymentTypeId,
                                        //JobPaymentID = JobPaymentId

                                    }
                                }
                    };

                    //Debug.WriteLine("Add pay req : " + JsonConvert.SerializeObject(addPaymentReqReq));

                    var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
                    _userDialog.HideLoading();
                    if(paymentResponse.Message == "true")
                    {
                        //_userDialog.Alert("Tip Added Successfully");
                       await  _navigationService.Navigate<ScheduleViewModel>();
                    }

                }
            }
        }
        #endregion Commands
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
