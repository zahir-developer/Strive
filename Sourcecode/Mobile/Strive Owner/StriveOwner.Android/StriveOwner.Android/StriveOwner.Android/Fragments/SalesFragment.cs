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
using OperationCanceledException = System.OperationCanceledException;

namespace StriveOwner.Android.Resources.Fragments
{
    public class SalesFragment : MvxFragment<SalesHomeViewModel>
    {
        private TextView washsales;
        private TextView detailsales;
        private TextView extraservicesales;
        private TextView merchandisesales;
        private TextView totalsales;
        private TextView monthclientsales;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Sales_Fragment, null);
            this.ViewModel = new SalesHomeViewModel();

            washsales = rootView.FindViewById<TextView>(Resource.Id.washsales);
            detailsales = rootView.FindViewById<TextView>(Resource.Id.detailsales);
            extraservicesales = rootView.FindViewById<TextView>(Resource.Id.extraservicesales);
            merchandisesales = rootView.FindViewById<TextView>(Resource.Id.merchandisesales);
            totalsales = rootView.FindViewById<TextView>(Resource.Id.totalsales);
            monthclientsales = rootView.FindViewById<TextView>(Resource.Id.monthclientsales);
            GetStatistics();
            return rootView;
        }
        public async void GetStatistics()
        {
            if (this.ViewModel == null)
            {
                ViewModel = new SalesHomeViewModel();
            }
            try
            {
                await this.ViewModel.getStatistics(OwnerTempData.LocationID);
                if (this.ViewModel != null && this.ViewModel.statisticsData != null)
                {
                    washsales.Text = this.ViewModel.statisticsData.WashSales.ToString();
                    detailsales.Text = this.ViewModel.statisticsData.DetailSales.ToString();
                    extraservicesales.Text = this.ViewModel.statisticsData.ExtraServiceSales.ToString();
                    merchandisesales.Text = this.ViewModel.statisticsData.MerchandizeSales.ToString();
                    totalsales.Text = this.ViewModel.statisticsData.TotalSales.ToString();
                    monthclientsales.Text = this.ViewModel.statisticsData.MonthlyClientSales.ToString();
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
}