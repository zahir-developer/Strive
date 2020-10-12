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
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater,container,savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.TermsAndConditionsFragment,null);
            infoFragment = new MyProfileInfoFragment();
            signatureFragment = new MembershipSignatureFragment();
            vehicleMembershipVM = new VehicleMembershipDetailsViewModel();
            termsandconditionsTextView = rootview.FindViewById<TextView>(Resource.Id.termsandconditionsDetail);
            AgreeTextView = rootview.FindViewById<TextView>(Resource.Id.textAgree);
            DisagreeTextView = rootview.FindViewById<TextView>(Resource.Id.textDisagree);
            backButton = rootview.FindViewById<Button>(Resource.Id.signatureBack);
            AgreeTextView.PaintFlags = PaintFlags.UnderlineText;
            DisagreeTextView.PaintFlags = PaintFlags.UnderlineText;
            this.ViewModel = new TermsAndConditionsViewModel();
            AgreeTextView.Click += AgreeTextView_Click;
            DisagreeTextView.Click += DisagreeTextView_Click;
            backButton.Click += BackButton_Click;
            termsandconditionsTextView.Text = "In consideration of first month as a discounted/prorated price point,next month MUST BE PAID . Cancel any time after 1st month is paid.To understand this form is valid for an undetermined time period month to month.Cancellation must be given, in person, by the 25 of any month in order to avoid being charged on the first of the next month. Cancellation is INVALID unless signed AND approved by a manager of our management team. You must come in to one of our stores and sign a Cancellation Form in order for the request to be processed. (Only by filling out cancellation form). Agreement is by each vehicle.Vehicles may be switched or exchanged for another vehicle with a $20.00 service charge.New vehicle will be placed under a new contract and the old vehicle will be taken off any contractual agreement previously in place.All returned or NSF payments will be assessed an additional $20.00 if payment of contract is not collected after the 5 day of the due date. Mammoth will not serve clients with more than one NSF or chargeback from bad card.";
            return rootview;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, signatureFragment).Commit();
        }

        private async void DisagreeTextView_Click(object sender, EventArgs e)
        {
            var result =  await ViewModel.DisagreeMembership();
            if(result)
            {
                SignatureClass.signaturePoints = null;
                MyProfileInfoNeeds.selectedTab = 1;
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();
            }
        }

        private async void AgreeTextView_Click(object sender, EventArgs e)
        {
            if(CheckMembership.hasExistingMembership && CustomerVehiclesInformation.membershipDetails == null)
            {
                await vehicleMembershipVM.CancelMembership();
                CheckMembership.hasExistingMembership = false;
            }
            var result = await ViewModel.AgreeMembership();
            if(result)
            {
                SignatureClass.signaturePoints = null;
                MyProfileInfoNeeds.selectedTab = 1;
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();
            }
           
        }
    }
}