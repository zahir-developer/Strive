using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class VehicleAdditionalServicesFragment : MvxFragment
    {
        private ListView additionalService;
        private AdditionalServicesAdapter additionalServicesAdapter;
        private VehicleUpChargesFragment upChargesFragment;
        private MembershipSignatureFragment membershipSignature;
        private Button backButton;
        private Button nextButton;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleAdditionalServicesFragment, null);
            upChargesFragment = new VehicleUpChargesFragment();
            membershipSignature = new MembershipSignatureFragment();
            additionalService = rootview.FindViewById<ListView>(Resource.Id.additionalOptions);
            backButton = rootview.FindViewById<Button>(Resource.Id.serviceBack);
            nextButton = rootview.FindViewById<Button>(Resource.Id.serviceNext);
            backButton.Click += BackButton_Click;
            nextButton.Click += NextButton_Click;
            additionalServicesAdapter = new AdditionalServicesAdapter(Context,MembershipDetails.filteredList.ServicesWithPrice);
            additionalService.SetAdapter(additionalServicesAdapter);
            return rootview;
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, membershipSignature).Commit();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, upChargesFragment).Commit();
        }
    }
}