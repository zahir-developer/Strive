using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    public class VehicleInfoDisplayFragment : MvxFragment<VehicleInfoDisplayViewModel>
    {
        private TextView vehicleBarCode;
        private TextView vehicleMake;
        private TextView vehicleModel;
        private TextView vehicleColor;
        private TextView vehicleMembership;
        private TextView vehicleName;
        private Button backButton;
       // private Button nextButton;
        private ImageButton editMembershipButton;
        private TextView cardNumber;
        private TextView expiryDate;
        private CardView cardDetails_cardView;
        private TextView noCardDetails_Text;
        private MyProfileInfoFragment profileFragment;
        private VehicleMembershipDetailsFragment membershipDetailFrag;
        private VehicleMembershipFragment vehicleMembershipFrag;
        private Button cancelMembershipButton;
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
            vehicleMembershipFrag = new VehicleMembershipFragment(this.Activity);
            editMembershipButton = rootview.FindViewById<ImageButton>(Resource.Id.vehicleEdits);
            vehicleBarCode = rootview.FindViewById<TextView>(Resource.Id.vehicleBarcode);
            vehicleMake = rootview.FindViewById<TextView>(Resource.Id.vehicleMake);
            vehicleModel = rootview.FindViewById<TextView>(Resource.Id.vehicleModel);
            vehicleColor = rootview.FindViewById<TextView>(Resource.Id.vehicleColor);
            vehicleName = rootview.FindViewById<TextView>(Resource.Id.vehicleName);
            vehicleMembership = rootview.FindViewById<TextView>(Resource.Id.vehicleMembershipName);
            backButton = rootview.FindViewById<Button>(Resource.Id.vehicleInfoBack);
            //nextButton = rootview.FindViewById<Button>(Resource.Id.vehicleInfoNext);
            cardNumber = rootview.FindViewById<TextView>(Resource.Id.cardNumber);
            expiryDate = rootview.FindViewById<TextView>(Resource.Id.expiryDate);
            cardDetails_cardView = rootview.FindViewById<CardView>(Resource.Id.cardDetails_cardView);
            noCardDetails_Text = rootview.FindViewById<TextView>(Resource.Id.noCardDetailsText);
            cancelMembershipButton = rootview.FindViewById<Button>(Resource.Id.cancelMembershipButton);
            this.ViewModel = new VehicleInfoDisplayViewModel();
            backButton.Click += BackButton_Click;
            //nextButton.Click += NextButton_Click;
            editMembershipButton.Click += EditMembershipButton_Click;
            cancelMembershipButton.Click += CancelMembershipButton_Click;
            getSelectVehicleInfo();
            return rootview;
        }

        private void CancelMembershipButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, membershipDetailFrag).Commit();
        }

        private async void EditMembershipButton_Click(object sender, EventArgs e)
        {
            try 
            {
                var result = await this.ViewModel.MembershipExists();
                if (result)
                {
                    AppCompatActivity activity = (AppCompatActivity)Context;
                    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, vehicleMembershipFrag).Commit();
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

        private void NextButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, membershipDetailFrag).Commit();
        }
        

        private void BackButton_Click(object sender, EventArgs e)
        {
            MyProfileInfoNeeds.selectedTab = 1;
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, profileFragment).Commit();
        }

        public async void getSelectVehicleInfo()
        {
            CheckMembership.hasExistingMembership = false;
            CustomerVehiclesInformation.membershipDetails = null;

            try
            {
                await this.ViewModel.GetSelectedVehicleInfo();
                await this.ViewModel.GetCompleteVehicleDetails();
                MembershipDetails.colorNumber = this.ViewModel.clientVehicleDetail.Status.ColorId;
                MembershipDetails.modelNumber = this.ViewModel.clientVehicleDetail.Status.VehicleModelId ?? 0;
                MembershipDetails.vehicleMakeNumber = this.ViewModel.clientVehicleDetail.Status.VehicleMakeId;
                MembershipDetails.barCode = this.ViewModel.clientVehicleDetail.Status.Barcode;
                MembershipDetails.vehicleMfr = this.ViewModel.clientVehicleDetail.Status.VehicleMakeId;
                MembershipDetails.vehicleMakeName = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleMfr ?? "";
                MembershipDetails.modelName = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleModel ?? "";
                MembershipDetails.colorName = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleColor ?? "";
                if (this.ViewModel.selectedVehicleInfo != null || this.ViewModel.selectedVehicleInfo.Status.Count > 0)
                {
                    vehicleName.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleColor + " " +
                                       this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleMfr + " " +
                                       this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleModel;
                    vehicleBarCode.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().Barcode ?? "";
                    vehicleMake.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleMfr ?? "";
                    vehicleModel.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleModel ?? "";
                    vehicleColor.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleColor ?? "";
                    if (this.ViewModel.response != null)
                    {
                        cardNumber.Text = this.ViewModel.response.CardNumber;
                        if (this.ViewModel.response.ExpiryDate.Contains("/"))
                        {
                            expiryDate.Text = this.ViewModel.response.ExpiryDate;
                        }
                        else 
                        {
                            expiryDate.Text = this.ViewModel.response.ExpiryDate.Substring(0,2) + "/" + this.ViewModel.response.ExpiryDate.Substring(2, 2);                            
                        }
                        
                    }
                    else
                    {
                        cardDetails_cardView.Visibility = ViewStates.Gone;
                        noCardDetails_Text.Visibility = ViewStates.Visible;
                    }

                    if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
                    {
                        CheckMembership.hasExistingMembership = true;
                        vehicleMembership.Text = "Yes";
                        cancelMembershipButton.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        CheckMembership.hasExistingMembership = false;
                        vehicleMembership.Text = "No";
                        cancelMembershipButton.Visibility = ViewStates.Gone;
                    }

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
    }
    public class CheckMembership
    {
        public static bool hasExistingMembership { get; set; }
    }
}