using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    public class TermsAndConditionsFragment : MvxFragment<TermsAndConditionsViewModel>
    {
        private TextView AgreeTextView;
        private TextView DisagreeTextView;
        private TextView termsandconditionsTextView;
        private Button backButton;
        private VehicleMembershipDetailsViewModel vehicleMembershipVM;
        MyProfileInfoFragment infoFragment;
        MembershipSignatureFragment signatureFragment;
        PaymentScreenFragment paymentScreenFragment;
        public double Total = 0;
        public List<string> SelectedAdditionalServices = new List<string>();
        private TextView MembershipName;
        private TextView AdditionalServicesCost;
        private TextView SwitchMembershipFee;
        private TextView UpchargesCost;
        private TextView OptionCode;
        private TextView AdvanceFee;
        private TextView Date;
        private TextView TotalCost;
        private TextView AdditionalServices;
        private TextView YearlyTotal;
        private TextView MonthlyCharges;
        private TextView StartingDate;
        private TextView EndingDate;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.TermsAndConditionsFragment, null);
            infoFragment = new MyProfileInfoFragment();
            paymentScreenFragment = new PaymentScreenFragment();
            signatureFragment = new MembershipSignatureFragment();
            vehicleMembershipVM = new VehicleMembershipDetailsViewModel();
            termsandconditionsTextView = rootview.FindViewById<TextView>(Resource.Id.termsandconditionsDetail);
            AgreeTextView = rootview.FindViewById<TextView>(Resource.Id.textAgree);
            DisagreeTextView = rootview.FindViewById<TextView>(Resource.Id.textDisagree);
            backButton = rootview.FindViewById<Button>(Resource.Id.signatureBack);
            AgreeTextView.PaintFlags = PaintFlags.UnderlineText;
            DisagreeTextView.PaintFlags = PaintFlags.UnderlineText;
            this.ViewModel = new TermsAndConditionsViewModel();
            MembershipName = rootview.FindViewById<TextView>(Resource.Id.membershipName);
            AdditionalServicesCost = rootview.FindViewById<TextView>(Resource.Id.additionalServicesCost);
            SwitchMembershipFee = rootview.FindViewById<TextView>(Resource.Id.switchMembershipFee);
            SwitchMembershipFee.Visibility = ViewStates.Gone;
            UpchargesCost = rootview.FindViewById<TextView>(Resource.Id.upcharges);
            UpchargesCost.Visibility = ViewStates.Gone;
            OptionCode = rootview.FindViewById<TextView>(Resource.Id.optionCode);
            AdvanceFee = rootview.FindViewById<TextView>(Resource.Id.advanceFee);
            Date = rootview.FindViewById<TextView>(Resource.Id.date);
            TotalCost = rootview.FindViewById<TextView>(Resource.Id.total);
            AdditionalServices = rootview.FindViewById<TextView>(Resource.Id.additionalSevices);
            YearlyTotal = rootview.FindViewById<TextView>(Resource.Id.yearlyTotal);
            MonthlyCharges = rootview.FindViewById<TextView>(Resource.Id.monthlyCharges);
            StartingDate = rootview.FindViewById<TextView>(Resource.Id.startingDate);
            EndingDate = rootview.FindViewById<TextView>(Resource.Id.endingDate);

            string Datenow = DateTime.Now.Date.ToString("yyyy-MM-dd");
            StartingDate.Text = new DateTime(DateTime.Now.Date.AddMonths(1).Year, DateTime.Now.Date.AddMonths(1).Month, 1).ToString("yyyy-MM-dd");
            EndingDate.Text = "Open";
            Date.Text = Datenow.Substring(0, 10);
            if (MembershipDetails.selectedMembershipDetail.MembershipName.Length < 8)
            {

                string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName + "- $" + (VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
                MembershipName.Text = membershipname;
            }

            else
            {
                string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName.Substring(8) + "- $" + (VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
                MembershipName.Text = membershipname;
            }

            GetTotal();
            AgreeTextView.Click += AgreeTextView_Click;
            DisagreeTextView.Click += DisagreeTextView_Click;
            backButton.Click += BackButton_Click;
            //termsandconditionsTextView.Text = "In consideration of first month as a discounted/prorated price point,next month MUST BE PAID . Cancel any time after 1st month is paid.To understand this form is valid for an undetermined time period month to month.Cancellation must be given, in person, by the 25 of any month in order to avoid being charged on the first of the next month. Cancellation is INVALID unless signed AND approved by a manager of our management team. You must come in to one of our stores and sign a Cancellation Form in order for the request to be processed. (Only by filling out cancellation form). Agreement is by each vehicle.Vehicles may be switched or exchanged for another vehicle with a $20.00 service charge.New vehicle will be placed under a new contract and the old vehicle will be taken off any contractual agreement previously in place.All returned or NSF payments will be assessed an additional $20.00 if payment of contract is not collected after the 5 day of the due date. Mammoth will not serve clients with more than one NSF or chargeback from bad card.";
           // termsandconditionsTextView.Text = "• In consideration of first month as a discounted/prorated price point, next month MUST BE PAID. Cancel any time after 1st month is paid. • I understand this form is valid for an undetermined time period month to month.Cancellation must be given, in person, by the 25th of any month in order to avoid being charged on the first of the next month.Cancellation is INVALID unless signed AND approved by a manager of our management team.You must come in to one of our stores and sign a Cancellation Form in order for the request to be processed. (Only by filling out cancellation form)  • Agreement is by each vehicle.Vehicles may be switched or exchanged for another vehicle with a $20.00 service charge.New vehicle will be placed under a new contract and the old vehicle will be taken off any contractual agreement previously in place. • All returned or NSF payments will be assessed an additional $20.00 if payment of contract is not collected after the 5th day of the due date. Mammoth will not serve clients with more than 1 NSF. • Payment amounts may change during the contract agreement depending on cancellations and/ or additional contracts of any other vehicle under the contract.Variations of 1st, 2nd and 3rd vehicles can / will cause a price increase or decrease. • Additional services outside of Monthly contract agreement will be paid at time of service rendered.If for some reason payment was not made at time of service, the credit card information NEW MAMMOTH DETAIL SALON has on file will be used to pay for the additional services rendered. • NEW MAMMOTH DETAIL SALON has normal business hours posted, however, weather will dictate if the business will be closed from time to time.Store hours change throughout the year due to Daylight Savings Time. • Normal wash times are 30 to 80 minutes, although longer wash times may occur during uncommon rushes. • Please Note: Prices May change without notice.Please keep in mind the two Alpharetta Locations are billed separately from Holcomb Bridge Location";
            return rootview;
        }
        public void GetTotal()
        {
            double MembershipAmount = VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price;
            var SelectedServices = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach (var Service in SelectedServices)
            {
                if (Service.Price != null)
                {

                    //Console.WriteLine(Service.Upcharges.Split("-")[1].Substring(3));
                    Total += (double)Service.Price;
                    SelectedAdditionalServices.Add(Service.ServiceName.Trim() + "-$" + string.Format("{0:0.00}", Service.Price));

                }
                else
                {
                    SelectedAdditionalServices.Add(Service.ServiceName.Trim() + "-$0");
                }
            }
            if (CustomerInfo.MembershipFee != 0)
            {
                MembershipAmount += CustomerInfo.MembershipFee;
                SwitchMembershipFee.Visibility = ViewStates.Visible;
            }
            if (MembershipDetails.modelUpcharge.upcharge.Count != 0)
            {
                UpchargesCost.Visibility = ViewStates.Visible;
                MembershipAmount += MembershipDetails.modelUpcharge.upcharge[0].Price;
                UpchargesCost.Text = "Wash Upcharges:   " + "$" + string.Format("{0:0.00}", MembershipDetails.modelUpcharge.upcharge[0].Price);
            }
            string ValuesToDisplay = ""+string.Join(", ", SelectedAdditionalServices);
            AdditionalServices.Text = ValuesToDisplay;
            AdditionalServicesCost.Text = "$" + string.Format("{0:0.00}", Total);
            Total += MembershipAmount;
            MonthlyCharges.Text = "$" + string.Format("{0:0.00}", Total);
            TotalCost.Text = "$" + string.Format("{0:0.00}", Total);
            YearlyTotal.Text = "$" + string.Format("{0:0.00}", Total * 12);
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, signatureFragment).Commit();
        }

        private async void DisagreeTextView_Click(object sender, EventArgs e)
        {
            var result = await ViewModel.DisagreeMembership();
            if (result)
            {
                SignatureClass.signaturePoints = null;
                MyProfileInfoNeeds.selectedTab = 1;
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();
            }
        }

        private void AgreeTextView_Click(object sender, EventArgs e)
        {
            // MyProfileInfoNeeds.selectedTab = 2;
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, paymentScreenFragment).Commit();
            //if (CheckMembership.hasExistingMembership && CustomerVehiclesInformation.membershipDetails == null)
            //{
            //    await vehicleMembershipVM.CancelMembership();
            //    CheckMembership.hasExistingMembership = false;
            //}
            //var result = await ViewModel.AgreeMembership();
            //if(result)
            //{
            //    SignatureClass.signaturePoints = null;
            //    MyProfileInfoNeeds.selectedTab = 1;
            //    AppCompatActivity activity = (AppCompatActivity)Context;
            //    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();
            //}

        }
    }
}