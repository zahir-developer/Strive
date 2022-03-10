﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels;
using Strive.Core.ViewModels.Customer;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class ScheduleWashHistoryFragment : MvxFragment<ScheduleViewModel>
    {
        private LinearLayout WashHistoryList_LinearLayout;
        private View layout;
        private View moreInfo;
        private TextView[] TicketNumber;
        private LinearLayout[] moreInfo_LinearLayout;
        private Context context;
        private bool isWashServiceCalled;
        private static View viewInstance;
        public ScheduleWashHistoryFragment(Context context)
        {
            this.context = context;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.WashHistoryFragment, null);
            viewInstance = rootview;
            WashHistoryList_LinearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.WashHistory_LinearLayout);
            if (!isWashServiceCalled)
            {
                this.ViewModel = new ScheduleViewModel();
                GetPastWashDetails();

            }
            return rootview;
        }

        public async Task GetPastWashDetails()
        {
            if (this.ViewModel == null)
            {
                this.ViewModel = new ScheduleViewModel();
            }
            try
            {
                if (WashHistoryList_LinearLayout == null && viewInstance != null)
                {
                    WashHistoryList_LinearLayout = viewInstance.FindViewById<LinearLayout>(Resource.Id.ServiceHistory_LinearLayout);
                }
                isWashServiceCalled = true;
                await this.ViewModel.GetPastWashDetails();
                WashHistoryList_LinearLayout.RemoveAllViews();
                UpdateWashServices(this.ViewModel.pastServiceHistory);

            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
        }
        private void UpdateWashServices(ServiceHistoryModel pastServiceHistory)
        {
            if (pastServiceHistory != null)
            {
                if (pastServiceHistory.DetailsGrid.JobViewModel != null)
                {
                    if (pastServiceHistory.DetailsGrid.JobViewModel.Count > 0)
                    {
                        var sortedBayJobDetail = pastServiceHistory.DetailsGrid.JobViewModel.OrderByDescending(x => DateTime.Parse(x.JobDate)).ToList();
                        TicketNumber = new TextView[pastServiceHistory.DetailsGrid.JobViewModel.Count];
                        moreInfo_LinearLayout = new LinearLayout[pastServiceHistory.DetailsGrid.JobViewModel.Count];
                        // for (int services = ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.Count-1; services >= 0; services--)
                        foreach (var services in sortedBayJobDetail)
                        {
                            if (context != null)
                            {
                                layout = LayoutInflater.From(context).Inflate(Resource.Layout.WashHistoryItemView, WashHistoryList_LinearLayout, false);

                                var vehicleName = layout.FindViewById<TextView>(Resource.Id.makeModelColorValue_TextView);
                                var detailVisitDate = layout.FindViewById<TextView>(Resource.Id.scheduleDetailVisit_TextView);
                                var detailService = layout.FindViewById<TextView>(Resource.Id.detailServices_TextView);
                                var barcode = layout.FindViewById<TextView>(Resource.Id.barcodeValue_TextView);
                                var price = layout.FindViewById<TextView>(Resource.Id.schedulePrice_TextView);
                                var additionalServices = layout.FindViewById<TextView>(Resource.Id.additionalServicesValue_TextView);
                                var detailedServiceCost = layout.FindViewById<TextView>(Resource.Id.scheduleTicketValue_TextView);

                                vehicleName.Text = services.VehicleMake + "/"
                                                + services.VehicleModel + "/"
                                                + services.VehicleColor;

                                var dates = services.JobDate.ToString();
                                var splitDates = dates.Split("T");
                                var detailedVisitDate = splitDates[0].Split("-"); ;
                                var orderedDate = detailedVisitDate[1] + "/" + detailedVisitDate[2] + "/" + detailedVisitDate[0];

                                detailVisitDate.Text = orderedDate;
                                detailService.Text = services.ServiceTypeName;
                                // additionalServices.Text = services.OutsideService;
                                detailedServiceCost.Text = "$" + services.Cost;
                                barcode.Text = services.Barcode;
                                price.Text = services.Cost.ToString();

                                TicketNumber[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<TextView>(Resource.Id.scheduleTicket_TextView);
                                TicketNumber[sortedBayJobDetail.IndexOf(services)].Text = services.TicketNumber;
                                //TicketNumber[services].PaintFlags = PaintFlags.UnderlineText;
                                moreInfo_LinearLayout[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<LinearLayout>(Resource.Id.moreInfo_LinearLayout);
                                moreInfo_LinearLayout[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Gone;
                                TicketNumber[sortedBayJobDetail.IndexOf(services)].Tag = sortedBayJobDetail.IndexOf(services);
                                TicketNumber[sortedBayJobDetail.IndexOf(services)].Click += WashHistoryFragment_Click;
                                //AssignListeners(sortedBayJobDetail.IndexOf(services));                           
                                WashHistoryList_LinearLayout.AddView(layout);
                            }
                        }
                    }
                }
            }
            else
            {
                if (UserVisibleHint)
                {
                    BaseViewModel._userDialog.Toast("No Schedules have been found !");
                }
            }
        }
        private void WashHistoryFragment_Click(object sender, EventArgs e)
        {
            var textView = (TextView)sender;
            int position = (int)textView.Tag;
            if (moreInfo_LinearLayout[position].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[position].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[position].Visibility = ViewStates.Gone;
            }
        }

    }

}