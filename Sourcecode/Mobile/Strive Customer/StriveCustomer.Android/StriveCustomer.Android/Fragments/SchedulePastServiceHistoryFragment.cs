using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class SchedulePastServiceHistoryFragment : MvxFragment<ScheduleViewModel>
    {
        private LinearLayout PastServiceList_LinearLayout;
        private View layout;
        private View moreInfo;
        private TextView[] TicketNumber;
        private LinearLayout[] moreInfo_LinearLayout;
        private Button[] tipButton;
        private TextView[] price;
        BottomSheetBehavior tipBottomSheet;
        Context context;
        
        public SchedulePastServiceHistoryFragment(BottomSheetBehavior sheetBehavior)
        {
            tipBottomSheet = sheetBehavior;            
        }
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
            context = this.Context;
            GetPastServices();
            PastServiceList_LinearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.ServiceHistory_LinearLayout);

            return rootview;
        }

        public async void GetPastServices()
        {
            if (this.ViewModel == null)
            {
                this.ViewModel = new ScheduleViewModel();
            }
            try
            {
                await this.ViewModel.GetPastServiceDetails();
                if (this.ViewModel.pastServiceHistory != null)
                {
                    if (this.ViewModel.pastServiceHistory.DetailsGrid.JobViewModel != null)
                    {
                        if (this.ViewModel.pastServiceHistory.DetailsGrid.JobViewModel.Count > 0)
                        {
                            var sortedBayJobDetail = this.ViewModel.pastServiceHistory.DetailsGrid.JobViewModel.OrderByDescending(x => DateTime.Parse(x.JobDate)).ToList();
                            TicketNumber = new TextView[this.ViewModel.pastServiceHistory.DetailsGrid.JobViewModel.Count];
                            moreInfo_LinearLayout = new LinearLayout[this.ViewModel.pastServiceHistory.DetailsGrid.JobViewModel.Count];
                            tipButton = new Button[this.ViewModel.pastServiceHistory.DetailsGrid.JobViewModel.Count];                            
                            price = new TextView[this.ViewModel.pastServiceHistory.DetailsGrid.JobViewModel.Count];
                            // for (int services = ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.Count-1; services >= 0; services--)
                            foreach (var services in sortedBayJobDetail)
                            {
                                if (Context != null)
                                {
                                    layout = LayoutInflater.From(Context).Inflate(Resource.Layout.ServiceHistoryItemView, PastServiceList_LinearLayout, false);

                                    var vehicleName = layout.FindViewById<TextView>(Resource.Id.makeModelColorValue_TextView);
                                    var detailVisitDate = layout.FindViewById<TextView>(Resource.Id.scheduleDetailVisit_TextView);
                                    var detailService = layout.FindViewById<TextView>(Resource.Id.detailServices_TextView);
                                    var barcode = layout.FindViewById<TextView>(Resource.Id.barcodeValue_TextView);
                                    price[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<TextView>(Resource.Id.schedulePrice_TextView);
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
                                    price[sortedBayJobDetail.IndexOf(services)].Text = services.Cost.ToString();

                                   // tipButton[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<Button>(Resource.Id.tipButton);
                                   // tipButton[sortedBayJobDetail.IndexOf(services)].Tag = sortedBayJobDetail.IndexOf(services);
                                    TicketNumber[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<TextView>(Resource.Id.scheduleTicket_TextView);
                                    TicketNumber[sortedBayJobDetail.IndexOf(services)].Text = services.TicketNumber;                                    
                                    //TicketNumber[services].PaintFlags = PaintFlags.UnderlineText;
                                    moreInfo_LinearLayout[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<LinearLayout>(Resource.Id.moreInfo_LinearLayout);
                                    moreInfo_LinearLayout[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Gone;
                                    TicketNumber[sortedBayJobDetail.IndexOf(services)].Tag = sortedBayJobDetail.IndexOf(services);
                                    TicketNumber[sortedBayJobDetail.IndexOf(services)].Click += SchedulePastServiceHistoryFragment_Click;
                                  //  tipButton[sortedBayJobDetail.IndexOf(services)].Click += TipButton_Click;
                                    //AssignListeners(sortedBayJobDetail.IndexOf(services));                           
                                    PastServiceList_LinearLayout.AddView(layout);
                                }
                            }
                        }
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

        private void TipButton_Click(object sender, EventArgs e)
        {
            
            var button = (Button)sender;
            var position = (int)button.Tag;
            ScheduleFragment.floatingActionButton.Visibility = ViewStates.Gone;
            ScheduleFragment.bottomNavigationView.Visibility = ViewStates.Gone;
            var TipAmounts = TipCalculation(position);            
            tipBottomSheet.State = BottomSheetBehavior.StateExpanded;            
        }

        private double[] TipCalculation(int position)
        {
            
            double[] tipAmounts= new double[4];
            tipAmounts[0] = double.Parse(price[position].Text) * 0.1;            
            tipAmounts[1] = double.Parse(price[position].Text) * 0.15;            
            tipAmounts[2] = double.Parse(price[position].Text) * 0.2;            
            tipAmounts[3] = double.Parse(price[position].Text) * 0.25;
            return tipAmounts;
        }

        private void SchedulePastServiceHistoryFragment_Click(object sender, EventArgs e)
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