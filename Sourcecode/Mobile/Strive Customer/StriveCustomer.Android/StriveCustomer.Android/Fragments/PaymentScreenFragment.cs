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

namespace StriveCustomer.Android.Fragments
{
    public class PaymentScreenFragment : MvxFragment<PaymentViewModel>
    {
        private TextView customerName;
        private TextView totalAmount;
        private EditText cardNo;
        private EditText expirationDate;
        private EditText CVV;
        private Button payButton;
        private Button paymentBackButton;
        PaymentViewModel paymentVM;
        MembershipSignatureFragment signatureFragment;
        MyProfileInfoFragment infoFragment;
        public float Amount;
        public long JobID;

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
            var rootView = inflater.Inflate(Resource.Layout.PaymentScreenFragment, container, false);
            customerName = rootView.FindViewById<TextView>(Resource.Id.customerName);
            totalAmount = rootView.FindViewById<TextView>(Resource.Id.totalAmount);
            cardNo = rootView.FindViewById<EditText>(Resource.Id.cardNo);
            expirationDate = rootView.FindViewById<EditText>(Resource.Id.expirationDate);
            CVV = rootView.FindViewById<EditText>(Resource.Id.CVV);
            payButton = rootView.FindViewById<Button>(Resource.Id.payButton);
            paymentBackButton = rootView.FindViewById<Button>(Resource.Id.paymentBackButton);
            paymentVM = new PaymentViewModel();
            infoFragment = new MyProfileInfoFragment();
            signatureFragment = new MembershipSignatureFragment();            
#if DEBUG
            cardNo.Text = "6011000995500000";
            expirationDate.Text = "12/21";
            CVV.Text = "291";
#endif
            GetTotal();
            GetPaymentDetails();
            payButton.Click += PayButton_Click;
            paymentBackButton.Click += PaymentBackButton_Click;
            return rootView;


        }

        private void PaymentBackButton_Click(object sender, EventArgs e)
        {           
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, signatureFragment).Commit();
        }

        public void GetTotal()
        {
            double MembershipAmount = VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price;
            var SelectedServices = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach (var Service in SelectedServices)
            {
                if (Service.Price != null)
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
            paymentVM.finalmonthlycharge = Amount;

            //MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.monthlyCharge = Amount;

        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            _userDialog.ShowLoading();
            short ccv = 0;
            if (CVV.Text != null && CVV.Text != "")
            {
                ccv = Convert.ToInt16(CVV.Text);
            }
            
            _ = PayAsync(cardNo.Text, expirationDate.Text, ccv);

        }
        public async Task PayAsync(string cardNo, string expiryDate, short ccv)
        {

            //_userDialog.ShowLoading();
            var totalAmnt = Amount;

            if (cardNo.IsEmpty() || expiryDate.IsEmpty() || ccv == 0)
            {
                _userDialog.HideLoading();
                _userDialog.Alert("Please fill card details");
                return;
            }

            try
            {
                var paymentAuthReq = new PaymentAuthReq
                {
                    CardConnect = new Object(),

                    PaymentDetail = new PaymentDetail()
                    {
                        Account = cardNo,
                        Expiry = expiryDate,
                        CCV = ccv,
                        Amount = Amount
                    },

                    BillingDetail = new BillingDetail()
                    {
                        Name = CustomerInfo.customerPersonalInfo.Status[0].FirstName,
                        Address = CustomerInfo.customerPersonalInfo.Status[0].Address1,
                        City = "Chennai",// status.City,
                        Country = "India",//status.Country,
                        Region = "Tamilnadu",//status.State,
                        Postal = CustomerInfo.customerPersonalInfo.Status[0].Zip
                    }

                };


                // Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));


                var apiService = new PaymentApiService();

                var paymentAuthResponse = await apiService.PaymentAuth(paymentAuthReq);

                // if (paymentAuthResponse.IsSuccess())
                if (paymentAuthResponse.Authcode != null)
                {
                    var paymentCaptureReq = new PaymentCaptureReq
                    {
                        AuthCode = paymentAuthResponse?.Authcode,
                        RetRef = paymentAuthResponse?.Retref,
                        Amount = totalAmnt,
                    };

                    // Debug.WriteLine("" + JsonConvert.SerializeObject(paymentCaptureReq));
                    var captureResponse = await apiService.PaymentCapture(paymentCaptureReq);

                    if (captureResponse.Authcode != null)
                    {
                        var generalApiService = new GeneralApiService();
                        var paymentStatusResponse = await generalApiService.GetGlobalData("PAYMENTSTATUS");
                        //Debug.WriteLine("Payment Status Response : " + JsonConvert.SerializeObject(paymentStatusResponse));
                        var paymentStatusId = paymentStatusResponse?.Codes.First(x => x.Name.Equals(PaymentViewModel.PaymentStatus.Success.ToString())).ID ?? -1;

                        var paymentTypeResponse = await generalApiService.GetGlobalData("PAYMENTTYPE");
                        //Debug.WriteLine("Payment Type Response : " + JsonConvert.SerializeObject(paymentTypeResponse));

                        var paymentTypeId = paymentTypeResponse?.Codes.First(x => x.Name.Equals(PaymentViewModel.PaymentType.Card.ToString())).ID ?? -1;

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
                            LocationID = 1,//AppSettings.LocationID,
                            JobID = JobID
                        };


                        //Debug.WriteLine("Add pay req : " + JsonConvert.SerializeObject(addPaymentReqReq));

                        var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
                        //Debug.WriteLine(JsonConvert.SerializeObject(paymentResponse));


                        if (paymentResponse.Message == "true")
                        {
                            paymentVM.MembershipAgree();

                        }
                        else
                        {
                            _userDialog.HideLoading();
                            _userDialog.Alert("The operation cannot be completed at this time.Unexpected Error!");
                        }
                    }
                    else
                    {
                        _userDialog.HideLoading();
                        _userDialog.Alert("The operation cannot be completed at this time.Unexpected Error!");
                    }
                }
                else
                {
                    _userDialog.HideLoading();
                    _userDialog.Alert("The operation cannot be completed at this time.Unexpected Error!");
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception happened and the reason is : " + ex.Message);

            }
            MyProfileInfoNeeds.selectedTab = 1;
            AppCompatActivity activity = (AppCompatActivity)Context;
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