using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee.CheckOut;
using StriveOwner.Android.Adapter.CheckOut;

namespace StriveOwner.Android.Fragments.CheckOut
{
    public class CheckOutFragment : MvxFragment<CheckOutViewModel>
    {
        private RecyclerView Checkout_RecyclerView;
        private CheckOutDetailsAdapter checkOutDetailsAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.CheckOut_Fragment, null);
            this.ViewModel = new CheckOutViewModel();

            Checkout_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.checkout_RecyclerView);
            GetCheckoutDetails();
            return rootView;
        }

        private async void GetCheckoutDetails()
        {
            await ViewModel.GetCheckOutDetails();
                if (ViewModel.CheckOutVehicleDetails != null)
            {
                if (ViewModel.CheckOutVehicleDetails != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails != null
                    || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count > 0)
                {
                    checkOutDetailsAdapter = new CheckOutDetailsAdapter(Context, ViewModel.CheckOutVehicleDetails);
                    var layoutManager = new LinearLayoutManager(Context);
                    Checkout_RecyclerView.SetLayoutManager(layoutManager);
                    Checkout_RecyclerView.SetAdapter(checkOutDetailsAdapter);
                }
            }
        }
    }
}