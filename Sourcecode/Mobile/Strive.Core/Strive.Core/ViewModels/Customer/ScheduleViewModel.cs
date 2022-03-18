using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Services.Implementations;
using Strive.Core.Utils;
using Xamarin.Essentials;

namespace Strive.Core.ViewModels.Customer
{
    public class ScheduleViewModel : BaseViewModel
    {
        #region Properties

        public VehicleList scheduleVehicleList { get; set; }

        public PastClientServices pastClientServices { get; set; }
        public ServiceHistoryModel pastServiceHistory { get; set; }
        public ServiceHistoryModel pastServiceHistory1 { get; set; }
        public static bool IsDetail { get; set; }

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
        public static int JobLocationId { get; set; }
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
            if(result == null)            {
                
                if (platform == DevicePlatform.iOS)
                {
                    _userDialog.Toast("No Schedules have been found !");
                }                
            }
            else
            {
                IsDetail = true;
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
                if (platform == DevicePlatform.iOS)
                {
                    _userDialog.Toast("No Schedules have been found !");
                }
                
            }
            else
            {
                IsDetail = false;
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
            _userDialog.ShowLoading();
            CustomerVehiclesInformation.completeVehicleDetails = new ClientVehicleRootView();
            CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails = new VehicleMembershipDetailsView();
            CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership = new ClientVehicleMembershipView();
            
            CustomerVehiclesInformation.completeVehicleDetails = await AdminService.GetVehicleMembership(VehicleId);
            if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null && CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.profileId != null)
            {

                CardNumber = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.cardNumber;
                AccountId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.accountId;
                ProfileId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.profileId;
                ExpiryDate = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.expiryDate;

                var apiService = new PaymentApiService();
                var paymentCaptureReq = new PaymentAuthTip
                {
                    Profile = ProfileId,
                    Amount = (float)WashTip,
                    LocationId = JobLocationId
                };
                _userDialog.ShowLoading();
                //Debug.WriteLine("" + JsonConvert.SerializeObject(paymentCaptureReq));
                var captureResponse = await apiService.PaymentAuthTips(paymentCaptureReq);

                if (captureResponse!=null)
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
                        JobPaymentDetails = new JobPaymentDetail
                        {
                            Amount = (float)WashTip,
                            //PaymentType = 0,
                            JobPaymentID = JobPaymentId,
                            PaymentType = paymentTypeId
                        }

                    };

                    //Debug.WriteLine("Add pay req : " + JsonConvert.SerializeObject(addPaymentReqReq));
                    _userDialog.ShowLoading();
                    var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
                    _userDialog.HideLoading();
                    if (paymentResponse.JobPaymentDetailId != 0)
                    {
                        await _userDialog.AlertAsync("Tip Added Successfully");
                        if (platform == DevicePlatform.iOS)
                        {
                            await _navigationService.Navigate<ScheduleViewModel>();
                        }
                        else 
                        {
                            await _navigationService.Navigate<DashboardViewModel>();
                        }

                    }

                }
                else
                {
                    
                    _userDialog.Alert("Tip Authentication failed");
                }
            }
            else
            {
                
                _userDialog.Alert("You are not eligible for Tip Payment");
            }
            _userDialog.HideLoading();
        }
        public async void  GetCustomTip()
        {
            PromptResult CustomTip = await _userDialog.PromptAsync(new PromptConfig { InputType = InputType.Name, OkText = "Ok", CancelText = "Cancel", Title = "Enter Tip Amount" });
            if (CustomTip.Text != null)
            {
                if (CustomTip.Ok)
                {
                    double result;
                    if (double.TryParse(CustomTip.Text, out result))
                    {
                        WashTip = double.Parse(CustomTip.Text);
                        TipPayment();
                    }
                    else
                    {
                        _userDialog.Toast("Please Enter A Valid Input");
                    }
                }
                else
                {

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
