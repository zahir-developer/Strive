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
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    public class VehicleInfoDisplayFragment : MvxFragment<VehicleInfoDisplayViewModel>
    {
        private TextView vehicleBarCode;
        private TextView vehicleMake;
        private TextView vehicleModel;
        private TextView vehicleColor;
        private TextView vehicleMembership;
        private Button backButton;
        private Button nextButton;
        private MyProfileInfoFragment profileFragment;
        private VehicleMembershipDetailsFragment membershipDetailFrag;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleInfoDisplay, null);
            profileFragment = new MyProfileInfoFragment();
            membershipDetailFrag = new VehicleMembershipDetailsFragment();
            vehicleBarCode = rootview.FindViewById<TextView>(Resource.Id.vehicleBarcode);
            vehicleMake = rootview.FindViewById<TextView>(Resource.Id.vehicleMake);
            vehicleModel = rootview.FindViewById<TextView>(Resource.Id.vehicleModel);
            vehicleColor = rootview.FindViewById<TextView>(Resource.Id.vehicleColor);
            vehicleMembership = rootview.FindViewById<TextView>(Resource.Id.vehicleMembershipName);
            backButton = rootview.FindViewById<Button>(Resource.Id.vehicleInfoBack);
            nextButton = rootview.FindViewById<Button>(Resource.Id.vehicleInfoNext);
            this.ViewModel = new VehicleInfoDisplayViewModel();
            backButton.Click += BackButton_Click;
            nextButton.Click += NextButton_Click; 
            getSelectVehicleInfo();
            return rootview;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, membershipDetailFrag).Commit();
        }
        

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, profileFragment).Commit();
        }

        public async void getSelectVehicleInfo()
        {
            await this.ViewModel.GetSelectedVehicleInfo();
            if (this.ViewModel.selectedVehicleInfo != null || this.ViewModel.selectedVehicleInfo.Status.Count > 0)
            {
                vehicleBarCode.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleMfr ?? "";
                vehicleMake.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleMfr ?? "";
                vehicleModel.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleModel ?? "";
                vehicleColor.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleColor ?? "";
                if(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
                {
                    vehicleMembership.Text = "Yes";
                    nextButton.Visibility = ViewStates.Visible;
                }
                else
                {
                    vehicleMembership.Text = "No";
                    nextButton.Visibility = ViewStates.Gone;
                }
               
            }
        }
    }
}