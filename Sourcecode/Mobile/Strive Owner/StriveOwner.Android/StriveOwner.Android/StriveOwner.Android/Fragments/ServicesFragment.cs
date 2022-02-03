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
    public class ServicesFragment : MvxFragment<ServicesHomeViewModel>
    {
        private TextView noofwashes;
        private TextView noofdetails;
        private TextView washemployees;
        private TextView score;
        private TextView forecastedcars;
        private TextView avgcarwashtime;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Services_Fragment, null);
            this.ViewModel = new ServicesHomeViewModel();
            noofwashes = rootView.FindViewById<TextView>(Resource.Id.noofwashes);
            noofdetails = rootView.FindViewById<TextView>(Resource.Id.noofdetails);
            washemployees = rootView.FindViewById<TextView>(Resource.Id.washemployees);
            score = rootView.FindViewById<TextView>(Resource.Id.score);
            forecastedcars = rootView.FindViewById<TextView>(Resource.Id.forecastedcars);
            avgcarwashtime = rootView.FindViewById<TextView>(Resource.Id.avgcarwashtime);
            GetStatistics();
            return rootView;
        }
        public async void GetStatistics()
        {
            if (this.ViewModel == null) 
            {
                this.ViewModel = new ServicesHomeViewModel();
            }
            try
            {
                await this.ViewModel.getStatistics(OwnerTempData.LocationID);
                if (this.ViewModel.statisticsData != null)
                {
                    noofwashes.Text = this.ViewModel.statisticsData.WashesCount.ToString();
                    noofdetails.Text = this.ViewModel.statisticsData.DetailCount.ToString();
                    washemployees.Text = this.ViewModel.statisticsData.EmployeeCount.ToString();
                    score.Text = this.ViewModel.statisticsData.Score.ToString();
                    forecastedcars.Text = this.ViewModel.statisticsData.Currents.ToString() + "/" + ViewModel.statisticsData.ForecastedCar.ToString();
                    avgcarwashtime.Text = this.ViewModel.statisticsData.WashTime?.ToString();
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