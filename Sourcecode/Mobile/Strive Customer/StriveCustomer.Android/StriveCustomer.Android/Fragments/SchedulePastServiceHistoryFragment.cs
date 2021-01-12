using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class SchedulePastServiceHistoryFragment : MvxFragment<ScheduleViewModel>
    {
        private LinearLayout PastServiceList_LinearLayout;
        private View layout;
        private View moreInfo;
        int a = 0;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.ServiceHistoryFragment, null);
            this.ViewModel = new ScheduleViewModel();
            GetPastServices();
            PastServiceList_LinearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.ServiceHistory_LinearLayout);

            return rootview;
        }

        private void TicketNumber_Click(object sender, EventArgs e)
        {
            
        }

        public async void GetPastServices()
        {
            await this.ViewModel.GetPastServiceDetails();
            if(this.ViewModel.pastServiceHistory != null)
            {
                if(this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.Count > 0)
                {

                    for(int services = 0; services < this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.Count; services++)
                    {
                        layout = LayoutInflater.From(Context).Inflate(Resource.Layout.ServiceHistoryItemView, PastServiceList_LinearLayout, false);
                        var vehicleName = layout.FindViewById<TextView>(Resource.Id.makeModelColorValue_TextView);
                        var detailVisitDate = layout.FindViewById<TextView>(Resource.Id.scheduleDetailVisit_TextView);
                        var detailService = layout.FindViewById<TextView>(Resource.Id.detailServices_TextView);
                        var barcode = layout.FindViewById<TextView>(Resource.Id.barcodeValue_TextView);
                        var price = layout.FindViewById<TextView>(Resource.Id.schedulePrice_TextView); 
                        var additionalServices = layout.FindViewById<TextView>(Resource.Id.additionalServicesValue_TextView);
                       
                        vehicleName.Text = this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel[services].VehicleColor
                                        + this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel[services].VehicleMake
                                        + this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel[services].VehicleModel;
                        detailVisitDate.Text = this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel[services].JobDate;
                        detailService.Text = "";
                        barcode.Text = "";
                        price.Text = this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel[services].Cost.ToString();

                        PastServiceList_LinearLayout.AddView(layout);

                    }

                }
            }
        }
    }
}