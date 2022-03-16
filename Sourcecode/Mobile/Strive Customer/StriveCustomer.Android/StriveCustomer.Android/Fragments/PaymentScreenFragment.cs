using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using Strive.Core.Models.Customer;
using Strive.Core.Services.Implementations;
using Strive.Core.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Strive.Core.Utils;
using Android.Support.V7.App;
using Android.Text;
using System.Text.RegularExpressions;
using Android.Support.Design.Widget;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    public class PaymentScreenFragment : MvxFragment<PaymentViewModel>
    {
        private TextView customerName;
        private TextView totalAmount;
        private EditText cardNo;
        private EditText expirationDate;
        //private EditText CVV;
        private Button payButton;
        private Button paymentBackButton;
        PaymentViewModel paymentVM;
        MyProfileInfoFragment infoFragment;
        public float Amount;
        public long JobID;
        private View rootView;


        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            // Use this to return your custom view for this Fragment
            rootView = inflater.Inflate(Resource.Layout.PaymentScreenFragment, container, false);
            customerName = rootView.FindViewById<TextView>(Resource.Id.customerName);
            totalAmount = rootView.FindViewById<TextView>(Resource.Id.totalAmount);
            cardNo = rootView.FindViewById<EditText>(Resource.Id.cardNo);
            expirationDate = rootView.FindViewById<EditText>(Resource.Id.expirationDate);
            //CVV = rootView.FindViewById<EditText>(Resource.Id.CVV);
            payButton = rootView.FindViewById<Button>(Resource.Id.payButton);
            paymentBackButton = rootView.FindViewById<Button>(Resource.Id.paymentBackButton);
            paymentVM = new PaymentViewModel();
            infoFragment = new MyProfileInfoFragment();
            if (CustomerVehiclesInformation.completeVehicleDetails != null)
            {
                if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null && CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.cardNumber != null)
                {
                    cardNo.Text = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.cardNumber;
                    if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.expiryDate.Contains("/"))
                    {
                        expirationDate.Text = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.expiryDate;
                    }
                    else 
                    { 
                        expirationDate.Text = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.expiryDate.Substring(0, 2) + "/" + CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.expiryDate.Substring(2, 2); 
                    }
                    
                    paymentVM.cardNumber = cardNo.Text;
                    paymentVM.expiryDate = expirationDate.Text;

                }
                
            }
            else
            {
                cardNo.Text = string.Empty;
                expirationDate.Text = string.Empty;

            }

            //#if DEBUG
            //            cardNo.Text = "6011000995500000";
            //            expirationDate.Text = "12/22";
            //            //CVV.Text = "291";
            //#endif
            GetTotal();
            GetPaymentDetails();
            payButton.Click += PayButton_Click;
            paymentBackButton.Click += PaymentBackButton_Click;
            return rootView;

        }

        private void PaymentBackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new MembershipSignatureFragment(true)).Commit();
        }

        public void GetTotal()
        {
            double MembershipAmount = (double)(VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
            var SelectedServices = VehicleAdditionalServiceViewModel.serviceList.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach (var Service in SelectedServices)
            {
                if (Service.Price != 0)
                {

                    Amount += (float)Service.Price;

                }
            }
            if (CustomerInfo.MembershipFee != 0)
            {
                MembershipAmount += CustomerInfo.MembershipFee;

            }
            if (MembershipDetails.modelUpcharge.upcharge.Count != 0)
            {
                MembershipAmount += MembershipDetails.modelUpcharge.upcharge[0].Price;
            }
            Amount += (float)MembershipAmount;
            paymentVM.finalmonthlycharge = 0;//Amount;

            //MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.monthlyCharge = Amount;

        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            //_userDialog.ShowLoading();
            //short ccv = 0;
            //if (CVV.Text != null && CVV.Text != "")
            //{
            //    ccv = Convert.ToInt16(CVV.Text);
            //}

            _ = PayAsync(cardNo.Text, expirationDate.Text); //, ccv);

        }
        public async Task PayAsync(string cardNo, string expiryDate) //, short ccv)
        {

            //_userDialog.ShowLoading();
            var totalAmnt = 0;// Amount;
            paymentVM.cardNumber = cardNo;
            paymentVM.expiryDate = expiryDate;
            if (cardNo.IsEmpty() || expiryDate.IsEmpty()) // || ccv == 0)
            {
                _userDialog.HideLoading();
                _userDialog.Alert("Please fill card details");
                return;
            }
            if(cardNo.Length < 16)
            {
                _userDialog.HideLoading();
                _userDialog.Alert("Invalid card number");
                return;
            }
            if (CustomerVehiclesInformation.completeVehicleDetails != null) 
            {
                if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
                {
                    if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.profileId != null)
                    {
                        paymentVM.accountId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.accountId;
                        paymentVM.profileId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.profileId;
                        //paymentVM.isAndroid = true;
                        try
                        {
                            await paymentVM.MembershipAgree();
                            Membershipstatus();
                        }
                        catch (Exception ex)
                        {
                            if (ex is OperationCanceledException)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        await Payment(cardNo, expiryDate);
                    }


                }
                else
                {

                    await Payment(cardNo, expiryDate);

                }
            }
            else
            {

                await Payment(cardNo, expiryDate);

            }


            //try
            //{
            //    var paymentAuthReq = new PaymentAuthReq
            //    {
            //        CardConnect = new Object(),

            //        PaymentDetail = new PaymentDetail()
            //        {
            //            Account = cardNo,
            //            Expiry = expiryDate,
            //            //CCV = ccv,
            //            Amount = Amount
            //        },

            //        BillingDetail = new BillingDetail()
            //        {
            //            Name = CustomerInfo.customerPersonalInfo.Status[0].FirstName,
            //            Address = CustomerInfo.customerPersonalInfo.Status[0].Address1,
            //            City = "Chennai",// status.City,
            //            Country = "India",//status.Country,
            //            Region = "Tamilnadu",//status.State,
            //            Postal = CustomerInfo.customerPersonalInfo.Status[0].Zip
            //        }

            //    };


            //    // Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));


            //    var apiService = new PaymentApiService();

            //    var paymentAuthResponse = await apiService.PaymentAuth(paymentAuthReq);

            //    // if (paymentAuthResponse.IsSuccess())
            //    if (paymentAuthResponse.Authcode != null)
            //    {
            //        var paymentCaptureReq = new PaymentCaptureReq
            //        {
            //            AuthCode = paymentAuthResponse?.Authcode,
            //            RetRef = paymentAuthResponse?.Retref,
            //            Amount = totalAmnt,
            //        };

            //        // Debug.WriteLine("" + JsonConvert.SerializeObject(paymentCaptureReq));
            //        var captureResponse = await apiService.PaymentCapture(paymentCaptureReq);

            //        if (captureResponse.Authcode != null)
            //        {
            //            var generalApiService = new GeneralApiService();
            //            var paymentStatusResponse = await generalApiService.GetGlobalData("PAYMENTSTATUS");
            //            //Debug.WriteLine("Payment Status Response : " + JsonConvert.SerializeObject(paymentStatusResponse));
            //            var paymentStatusId = paymentStatusResponse?.Codes.First(x => x.Name.Equals(PaymentViewModel.PaymentStatus.Success.ToString())).ID ?? -1;

            //            var paymentTypeResponse = await generalApiService.GetGlobalData("PAYMENTTYPE");
            //            //Debug.WriteLine("Payment Type Response : " + JsonConvert.SerializeObject(paymentTypeResponse));

            //            var paymentTypeId = paymentTypeResponse?.Codes.First(x => x.Name.Equals(PaymentViewModel.PaymentType.Card.ToString())).ID ?? -1;

            //            var addPaymentReqReq = new AddPaymentReq
            //            {
            //                SalesPaymentDto = new SalesPaymentDto()
            //                {
            //                    JobPayment = new JobPayment()
            //                    {
            //                        JobID = JobID,
            //                        Amount = Amount,
            //                        PaymentStatus = paymentStatusId
            //                    },

            //                    JobPaymentDetails = new List<JobPaymentDetail>() {
            //                                new JobPaymentDetail()
            //                                {
            //                                    Amount = Amount,
            //                                    PaymentType = paymentTypeId
            //                                }
            //                            }
            //                },
            //                LocationID = 1,//AppSettings.LocationID,
            //                JobID = JobID
            //            };


            //            //Debug.WriteLine("Add pay req : " + JsonConvert.SerializeObject(addPaymentReqReq));

            //            var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
            //            //Debug.WriteLine(JsonConvert.SerializeObject(paymentResponse));

            //            if (paymentResponse.Message == "true")
            //            {
            //                paymentVM.isAndroid = true;
            //                paymentVM.MembershipAgree();                            
            //                Snackbar.Make(rootView, "Membership has been created successfully", Snackbar.LengthShort).Show();
            //                AppCompatActivity activity = (AppCompatActivity)Context;
            //                MyProfileInfoNeeds.selectedTab = 0;
            //                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();

            //            }
            //            else
            //            {
            //                _userDialog.HideLoading();
            //                _userDialog.Alert("The operation cannot be completed at this time.Incorrect card details!");
            //            }
            //        }
            //        else
            //        {
            //            _userDialog.HideLoading();
            //            _userDialog.Alert("The operation cannot be completed at this time.Incorrect card details!");
            //        }
            //    }
            //    else
            //    {
            //        _userDialog.HideLoading();
            //        _userDialog.Alert("The operation cannot be completed at this time.Incorrect card details!");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    _userDialog.Alert("Incorrect card details!");
            //    System.Diagnostics.Debug.WriteLine("Exception happened and the reason is : " + ex.Message);

            //}

        }
        public async Task Payment(string cardNo, string expiryDate)
        {
            var paymentAuthReq = new PaymentAuthReq
            {
                CardConnect = new Object(),

                PaymentDetail = new PaymentDetail()
                {
                    Account = cardNo,
                    Expiry = expiryDate.Replace("/", ""),
                    //CCV = ccv,
                    Amount = 0,
                    OrderID = ""
                },

                BillingDetail = new BillingDetail()
                {
                    Name = CustomerInfo.customerPersonalInfo.Status[0].FirstName,
                    Address = CustomerInfo.customerPersonalInfo.Status[0].Address1,
                    City = "Chennai",// status.City,
                    Country = "India",//status.Country,
                    Region = "Tamilnadu",//status.State,
                    Postal = CustomerInfo.customerPersonalInfo.Status[0].Zip
                },

                Locationid = 1


            };


            //Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));

            try
            {
                var apiService = new PaymentApiService();

                var paymentAuthResponse = await apiService.PaymentAuthProfile(paymentAuthReq);

                // if (paymentAuthResponse.IsSuccess())
                if (paymentAuthResponse != null)
                {
                    paymentVM.accountId = paymentAuthResponse.AccountId;
                    paymentVM.profileId = paymentAuthResponse.ProfileId;
                    //paymentVM.isAndroid = true;
                    try
                    {
                        await paymentVM.MembershipAgree();
                        Membershipstatus();
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException)
                        {
                            return;
                        }
                    }

                }
                else
                {
                    _userDialog.HideLoading();
                   // _userDialog.Alert("Error, membership not created!");
                }

            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }

        }

        private async void Membershipstatus()
        {
            if (paymentVM.membershipStatus)
            {
                await _userDialog.AlertAsync("Amount will be charged from 1st of next month.");
            }
            else
            {
                await _userDialog.AlertAsync("Error, membership not created!");
            }
            AppCompatActivity activity = (AppCompatActivity)Context;
            MyProfileInfoNeeds.selectedTab = 0;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();
        }

        private void GetPaymentDetails()
        {
            customerName.Text = MyProfileCustomerInfo.FullName;
            UpdateAmountLblInDollar(Amount.ToString());
        }
        void UpdateAmountLblInDollar(string amt)
        {
            totalAmount.Text = $"${amt}";
        }

    }
}