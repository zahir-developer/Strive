using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross;
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
    public class SchedulePastServiceHistoryFragment : MvxFragment<ScheduleViewModel>,Tip
    {
        private LinearLayout PastServiceList_LinearLayout;
        private View layout;
        private View moreInfo;
        private TextView[] TicketNumber;
        private LinearLayout[] moreInfo_LinearLayout;
       // private Button[] tipButton;
        private TextView[] price;
       // BottomSheetBehavior tipBottomSheet;       
        private bool isPastServiceCalled;
        private static View viewInstance;
        private Context cxt;
        private CardView[] detailHistoryInfo;
       // private TextView tipLabel;
        public override bool UserVisibleHint { get => base.UserVisibleHint; set => base.UserVisibleHint = value; }

        public SchedulePastServiceHistoryFragment(BottomSheetBehavior sheetBehavior , Context context)
        {
           // tipBottomSheet = sheetBehavior;
            this.cxt = context;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var  rootview = this.BindingInflate(Resource.Layout.ServiceHistoryFragment, null);
            viewInstance = rootview;
            PastServiceList_LinearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.ServiceHistory_LinearLayout);
           // tipLabel = rootview.FindViewById<TextView>(Resource.Id.labelTip);
            
            if (!isPastServiceCalled)
            {
                this.ViewModel = new ScheduleViewModel();
                GetPastServices();
            }
           
            
            //ticketNumber.Add("-1");
            return rootview;
        }        

        public async Task GetPastServices()
        {
            if (this.ViewModel == null)
            {
                this.ViewModel = new ScheduleViewModel();
            }
            try
            {
                if (PastServiceList_LinearLayout == null && viewInstance != null) 
                {
                    PastServiceList_LinearLayout = viewInstance.FindViewById<LinearLayout>(Resource.Id.ServiceHistory_LinearLayout);
                }
                isPastServiceCalled = true;
                await this.ViewModel.GetPastServiceDetails();
                PastServiceList_LinearLayout.RemoveAllViews();
                UpdatePastService(this.ViewModel.pastServiceHistory);           

            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
        }
        private void UpdatePastService(ServiceHistoryModel pastServiceHistory)
        {
             
            if (pastServiceHistory != null)
            {
                if (pastServiceHistory.DetailsGrid.JobViewModel != null)
                {
                    if (pastServiceHistory.DetailsGrid.JobViewModel.Count > 0)
                    {
                        var sortedBayJobDetail = pastServiceHistory.DetailsGrid.JobViewModel.OrderByDescending(x => DateTime.Parse(x.JobDate)).ToList();
                        Tip.SavedList = sortedBayJobDetail;
                        TicketNumber = new TextView[pastServiceHistory.DetailsGrid.JobViewModel.Count];
                        moreInfo_LinearLayout = new LinearLayout[pastServiceHistory.DetailsGrid.JobViewModel.Count];
                       // tipButton = new Button[pastServiceHistory.DetailsGrid.JobViewModel.Count];
                        price = new TextView[pastServiceHistory.DetailsGrid.JobViewModel.Count];
                        detailHistoryInfo = new CardView[pastServiceHistory.DetailsGrid.JobViewModel.Count];
                        // for (int services = ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.Count-1; services >= 0; services--)
                        foreach (var services in sortedBayJobDetail)
                        {
                            if (cxt != null)
                            {
                                layout = LayoutInflater.From(cxt).Inflate(Resource.Layout.ServiceHistoryItemView, PastServiceList_LinearLayout, false);

                                var vehicleName = layout.FindViewById<TextView>(Resource.Id.makeModelColorValue_TextView);
                                var detailVisitDate = layout.FindViewById<TextView>(Resource.Id.scheduleDetailVisit_TextView);
                                var detailService = layout.FindViewById<TextView>(Resource.Id.detailServices_TextView);
                                var barcode = layout.FindViewById<TextView>(Resource.Id.barcodeValue_TextView);
                                price[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<TextView>(Resource.Id.schedulePrice_TextView);
                                var additionalServices = layout.FindViewById<TextView>(Resource.Id.additionalServicesValue_TextView);
                                var detailedServiceCost = layout.FindViewById<TextView>(Resource.Id.scheduleTicketValue_TextView);
                                detailHistoryInfo[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<CardView>(Resource.Id.DetailHistory_InfoCard);

                                vehicleName.Text = services.VehicleMake + "/"
                                                + services.VehicleModel + "/"
                                                + services.VehicleColor;

                                var dates = services.JobDate.ToString();
                                var splitDates = dates.Split("T");
                                var detailedVisitDate = splitDates[0].Split("-"); ;
                                var orderedDate = detailedVisitDate[1] + "/" + detailedVisitDate[2] + "/" + detailedVisitDate[0];

                                detailVisitDate.Text = orderedDate;
                                detailService.Text = services.ServiceTypeName;                                
                                if (services.AdditionalServices != null)
                                {
                                    additionalServices.Text = services.AdditionalServices.Replace(" ", "");
                                }
                                else
                                {
                                    additionalServices.Text = "No Added Service";
                                }
                                detailedServiceCost.Text = "$" + services.Cost;
                                barcode.Text = services.Barcode;
                                price[sortedBayJobDetail.IndexOf(services)].Text = services.Cost.ToString();                              

                                //tipButton[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<Button>(Resource.Id.tipButton);
                                //tipButton[sortedBayJobDetail.IndexOf(services)].Tag = sortedBayJobDetail.IndexOf(services);
                                
                                //if (services.PaymentDate != null)
                                //{
                                //    if (services.PaymentDate.Substring(0, 10) == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                                //    {

                                //        if (DateTime.Now.TimeOfDay.Hours >= 20)
                                //        {
                                //            tipButton[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Invisible;
                                //        }
                                //        else
                                //        {
                                //            if (services.TipAmount != "0.00")
                                //            {
                                //                tipButton[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Invisible;
                                //            }
                                //            else
                                //            {

                                //                tipButton[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Visible;
                                //            }

                                //        }

                                //    }
                                //    else
                                //    {
                                //        tipButton[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Invisible;
                                //    }
                                //}
                                //else
                                //{
                                //    tipButton[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Invisible;
                                //}
                                TicketNumber[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<TextView>(Resource.Id.scheduleTicket_TextView);
                                TicketNumber[sortedBayJobDetail.IndexOf(services)].Text = services.TicketNumber;
                                //TicketNumber[services].PaintFlags = PaintFlags.UnderlineText;
                                moreInfo_LinearLayout[sortedBayJobDetail.IndexOf(services)] = layout.FindViewById<LinearLayout>(Resource.Id.moreInfo_LinearLayout);
                                moreInfo_LinearLayout[sortedBayJobDetail.IndexOf(services)].Visibility = ViewStates.Gone;
                                TicketNumber[sortedBayJobDetail.IndexOf(services)].Tag = sortedBayJobDetail.IndexOf(services);
                                TicketNumber[sortedBayJobDetail.IndexOf(services)].Click += SchedulePastServiceHistoryFragment_Click;
                                detailHistoryInfo[sortedBayJobDetail.IndexOf(services)].Tag = sortedBayJobDetail.IndexOf(services);
                                detailHistoryInfo[sortedBayJobDetail.IndexOf(services)].Click += DetailHistoryInfo_Click;
                               // tipButton[sortedBayJobDetail.IndexOf(services)].Click += TipButton_Click;
                                //AssignListeners(sortedBayJobDetail.IndexOf(services));
                                PastServiceList_LinearLayout.AddView(layout);
                            }
                        }
                    }
                }
                else
                {
                   // tipLabel.Visibility = ViewStates.Gone;
                    if (UserVisibleHint)
                    {
                        BaseViewModel._userDialog.Toast("No Schedules have been found !");
                    }

                }
            }
            else
            {
                //tipLabel.Visibility = ViewStates.Gone;
                if (UserVisibleHint)
                { 
                    BaseViewModel._userDialog.Toast("No Schedules have been found !"); 
                }
                
            }
        }

        private void DetailHistoryInfo_Click(object sender, EventArgs e)
        {
            var cardView = (CardView)sender;
            int position = (int)cardView.Tag;
            if (moreInfo_LinearLayout[position].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[position].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[position].Visibility = ViewStates.Gone;
            }

        }

        //private void TipButton_Click(object sender, EventArgs e)
        //{

        //    var button = (Button)sender;
        //    var position = (int)button.Tag;
        //    ScheduleFragment.floatingActionButton.Visibility = ViewStates.Gone;
        //    ScheduleFragment.bottomNavigationView.Visibility = ViewStates.Gone;
        //    Tip.Tips= TipCalculation(position);
        //    Tip.position = position;
        //    ScheduleFragment.TipAmounts();
        //    tipBottomSheet.State = BottomSheetBehavior.StateExpanded;            
        //}

        //private double[] TipCalculation(int position)
        //{

        //    double[] tipAmounts= new double[4];
        //    tipAmounts[0] = double.Parse(price[position].Text) * 0.1;            
        //    tipAmounts[1] = double.Parse(price[position].Text) * 0.15;            
        //    tipAmounts[2] = double.Parse(price[position].Text) * 0.2;            
        //    tipAmounts[3] = double.Parse(price[position].Text) * 0.25;
        //    return tipAmounts;
        //}

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
    public interface Tip 
    {
        public static double[] Tips;
        public static int position;
        public static List<jobViewModel> SavedList;
    }

}