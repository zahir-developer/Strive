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
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class VehicleInfoFragment : MvxFragment<VehicleInfoViewModel>
    {
        private Button addButton;
        private VehicleInfoEditFragment infoEditFragment;
        private RecyclerView vehicleview;
        VehicleDetailsAdapter vehicleDetailsAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleInfoFragment, null);
            this.ViewModel = new VehicleInfoViewModel();
            GetVehicleList();
            infoEditFragment = new VehicleInfoEditFragment();
            addButton = rootview.FindViewById<Button>(Resource.Id.vehicleInfoAdd);
            vehicleview = rootview.FindViewById<RecyclerView>(Resource.Id.availableVehicles);
            
            addButton.Click += AddButton_Click;
            return rootview;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            CheckMembership.hasExistingMembership = false;
            CustomerVehiclesInformation.membershipDetails = null;
            MembershipDetails.clearMembershipData();
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoEditFragment).Commit();
        }
        public async void GetVehicleList()
        {
            if (this.ViewModel == null)
            {
                ViewModel = new VehicleInfoViewModel();
            }
            await this.ViewModel.GetCustomerVehicleList();
            if(!(this.ViewModel.vehicleLists?.Status.Count == 0) || !(this.ViewModel.vehicleLists == null))
            {
                vehicleDetailsAdapter = new VehicleDetailsAdapter(Context, this.ViewModel.vehicleLists);
                var layoutManager = new LinearLayoutManager(Context);
                vehicleview.SetLayoutManager(layoutManager);
                vehicleview.SetAdapter(vehicleDetailsAdapter);    
            }
            
        }
    }
}