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
using Strive.Core.Utils;
using Strive.Core.ViewModels;
using Strive.Core.ViewModels.Customer;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    public class VehicleMembershipDetailsFragment : MvxFragment<VehicleMembershipDetailsViewModel>
    {
        private Button backButton;
        private Button cancelButton;
        private TextView membershipName;
        private TextView createdDate;
        private TextView cancelledDate;
        private TextView status;
        private VehicleInfoDisplayFragment infoDisplay;
        private MyProfileInfoFragment myProfileInfo;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater,container,savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleMembershipDetailsFragment,null);
            this.ViewModel = new VehicleMembershipDetailsViewModel();
            infoDisplay = new VehicleInfoDisplayFragment();
            myProfileInfo = new MyProfileInfoFragment();
            backButton = rootview.FindViewById<Button>(Resource.Id.vehicleMemberDetailsBack);
            cancelButton = rootview.FindViewById<Button>(Resource.Id.cancelMembership);
            membershipName = rootview.FindViewById<TextView>(Resource.Id.membershipNames);
            createdDate = rootview.FindViewById<TextView>(Resource.Id.membershipCreatedDate);
            cancelledDate = rootview.FindViewById<TextView>(Resource.Id.membershipDeletedDate);
            status = rootview.FindViewById<TextView>(Resource.Id.membershipActiveStatus);
            backButton.Click += BackButton_Click;
            cancelButton.Click += CancelButton_Click; 
            GetMembershipInfo();
            return rootview;
        }

        private async void CancelButton_Click(object sender, EventArgs e)
        {
            try 
            {
                await this.ViewModel.CancelMembership();
                MyProfileInfoNeeds.selectedTab = 1;
                if (ViewModel.result.PaymentCount == 0)
                {
                    BaseViewModel._userDialog.Alert("Membership cannot be cancelled before the first membership payment!");
                }
                else
                {
                    AppCompatActivity activity = (AppCompatActivity)Context;
                    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfileInfo).Commit();
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

        private async void GetMembershipInfo()
        {
            try
            {
                await this.ViewModel.GetMembershipInfo();
                if (!string.IsNullOrEmpty(this.ViewModel.MembershipName))
                {
                    membershipName.Text = this.ViewModel.MembershipName;
                }
                var CreatedDate = DateUtils.ConvertDateTimeFromZ(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.StartDate.ToString());
                var DeletedDate = DateUtils.ConvertDateTimeFromZ(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.EndDate.ToString());

                createdDate.Text = CreatedDate;
                cancelledDate.Text = DeletedDate;
                if (!CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.IsActive.HasValue)
                {
                    status.Text = "Active";
                }
                else
                {
                    status.Text = "InActive";
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

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoDisplay).Commit();
        }
    }
}