using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Owner;
using Strive.Core.ViewModels.Owner;

namespace StriveOwner.Android.Resources.Fragments
{
    public class RevenueFragment : MvxFragment<RevenueHomeViewModel>
    {
        private TextView avgmoneyewashpercar;
        private TextView avgmoneyedetailpercar;
        private TextView avgmoneyeextraservicepercar;
        private TextView avgmoneytotalpercar;
        private TextView laborcostpercar;
        private TextView detailcostpercar;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Revenue_Fragment, null);
            this.ViewModel = new RevenueHomeViewModel();
            avgmoneyewashpercar = rootView.FindViewById<TextView>(Resource.Id.avgmoneyewashpercar);
            avgmoneyedetailpercar = rootView.FindViewById<TextView>(Resource.Id.avgmoneyedetailpercar);
            avgmoneyeextraservicepercar = rootView.FindViewById<TextView>(Resource.Id.avgmoneyeextraservicepercar);
            avgmoneytotalpercar = rootView.FindViewById<TextView>(Resource.Id.avgmoneytotalpercar);
            laborcostpercar = rootView.FindViewById<TextView>(Resource.Id.laborcostpercar);
            detailcostpercar = rootView.FindViewById<TextView>(Resource.Id.detailcostpercar);
            GetStatistics();
          
            return rootView;
        }
        private async void GetStatistics()
        {
            await this.ViewModel.getStatistics(OwnerTempData.LocationID);
            avgmoneyewashpercar.Text = this.ViewModel.statisticsData.AverageWashPerCar.ToString();
            avgmoneyedetailpercar.Text = this.ViewModel.statisticsData.AverageDetailPerCar.ToString();
            avgmoneyeextraservicepercar.Text = this.ViewModel.statisticsData.AverageExtraServicePerCar.ToString();
            avgmoneytotalpercar.Text = this.ViewModel.statisticsData.AverageTotalPerCar.ToString();
            laborcostpercar.Text = this.ViewModel.statisticsData.LabourCostPerCarMinusDetail.ToString();
            detailcostpercar.Text = this.ViewModel.statisticsData.DetailCostPerCar.ToString();

        }
    }
}