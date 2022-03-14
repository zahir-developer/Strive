using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleFragment : MvxFragment<ScheduleViewModel>,ViewPager.IOnPageChangeListener,Tip
    {
        TabLayout scheduleTabs;
        ViewPager schedulePager;
        ViewPagerAdapter scheduleAdapter;
        SchedulePastServiceHistoryFragment pastServiceHistoryFragment;
        ScheduleVehicleListFragment vehicleListFragment;
        ScheduleWashHistoryFragment washHistoryFragment;
        BottomSheetBehavior tipBottomSheet;        
        private FrameLayout tipFrameLayout;
        public static TextView tipAmountOne;
        public static TextView tipAmountTwo;
        public static TextView tipAmountThree;
        public static TextView tipAmountFour;
        public static TextView tipCustom;
        public Button tipCancelButton;
        public static FloatingActionButton floatingActionButton;
        public static BottomNavigationView bottomNavigationView;
        private TextView amount;
        private Context context;

        public ScheduleFragment(Context context)
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
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleScreenFragment, null);

            scheduleTabs = rootView.FindViewById<TabLayout>(Resource.Id.Schedule_TabLayout);
            schedulePager = rootView.FindViewById<ViewPager>(Resource.Id.Schedule_ProfilePager);
            
            vehicleListFragment = new ScheduleVehicleListFragment();           
            tipFrameLayout = rootView.FindViewById<FrameLayout>(Resource.Id.tipBottomSheet);
            tipBottomSheet = BottomSheetBehavior.From(tipFrameLayout);
            tipAmountOne = rootView.FindViewById<TextView>(Resource.Id.tipAmountOne);
            tipAmountTwo = rootView.FindViewById<TextView>(Resource.Id.tipAmountTwo);
            tipAmountThree = rootView.FindViewById<TextView>(Resource.Id.tipAmountThree);
            tipAmountFour = rootView.FindViewById<TextView>(Resource.Id.tipAmountFour);
            tipCancelButton = rootView.FindViewById<Button>(Resource.Id.tipCancelButton);
            pastServiceHistoryFragment = new SchedulePastServiceHistoryFragment(tipBottomSheet,context);
            washHistoryFragment = new ScheduleWashHistoryFragment(context, tipBottomSheet);
            tipBottomSheet.SetBottomSheetCallback(new BottomSheet(tipBottomSheet));
            
            tipAmountOne.Click += TipAmountOne_Click;
            tipAmountTwo.Click += TipAmountTwo_Click;
            tipAmountThree.Click += TipAmountThree_Click;
            tipAmountFour.Click += TipAmountFour_Click;
            tipCancelButton.Click += TipCancelButton_Click;
            tipCustom.Click += TipCustom_Click;
            CustomerScheduleInformation.ClearScheduleData();

            return rootView;
        }

        private void TipCustom_Click(object sender, EventArgs e)
        {
            TipDetails();
            ViewModel.GetCustomTip();
        }

        private void TipCancelButton_Click(object sender, EventArgs e)
        {            
            tipBottomSheet.State = BottomSheetBehavior.StateHidden;            
        }
        private void OnTipCalled()         
        {           
            tipBottomSheet.State = BottomSheetBehavior.StateHidden;
            floatingActionButton.Visibility = ViewStates.Visible;
            bottomNavigationView.Visibility = ViewStates.Visible;
        }
        private void TipDetails() 
        {
            ViewModel = new ScheduleViewModel();         
            ScheduleViewModel.VehicleId = int.Parse(Tip.SavedList[Tip.position].VehicleId);
            ScheduleViewModel.Jobid = Tip.SavedList[Tip.position].JobId;
            ScheduleViewModel.TicketNumber = Tip.SavedList[Tip.position].TicketNumber;
            ScheduleViewModel.JobPaymentId = int.Parse(Tip.SavedList[Tip.position].JobPaymentId);
            ScheduleViewModel.JobLocationId = Tip.SavedList[Tip.position].LocationId;
        }
        private void TipAmountFour_Click(object sender, EventArgs e)
        {
            amount = (TextView)sender;
            TipDetails();
            ViewModel.WashTip = double.Parse(Tip.Tips[3].ToString("0.00")); 
            ViewModel.TipPayment();
            OnTipCalled();
        }

        private void TipAmountThree_Click(object sender, EventArgs e)
        {
            amount = (TextView)sender;
            TipDetails();
            ViewModel.WashTip = double.Parse(Tip.Tips[2].ToString("0.00"));            
            ViewModel.TipPayment();
            OnTipCalled();
        }

        private void TipAmountTwo_Click(object sender, EventArgs e)
        {
            amount = (TextView)sender;
            TipDetails();
            ViewModel.WashTip = double.Parse(Tip.Tips[1].ToString("0.00"));           
            ViewModel.TipPayment();
            OnTipCalled();
        }

        private void TipAmountOne_Click(object sender, EventArgs e)
        {
            amount = (TextView)sender;
            TipDetails();
            ViewModel.WashTip = double.Parse(Tip.Tips[0].ToString("0.00"));
            ViewModel.TipPayment();
            OnTipCalled();
        }

        public static void TipAmounts()
        {
            tipAmountOne.Text = "10% - $" + Tip.Tips[0].ToString("0.00");
            tipAmountTwo.Text = "15% - $" + Tip.Tips[1].ToString("0.00");
            tipAmountThree.Text = "20% - $" + Tip.Tips[2].ToString("0.00");
            tipAmountFour.Text = "25% - $" + Tip.Tips[3].ToString("0.00");
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            scheduleAdapter = new ViewPagerAdapter(ChildFragmentManager);
            scheduleAdapter.AddFragment(vehicleListFragment, "Signup Vehicles");
            scheduleAdapter.AddFragment(pastServiceHistoryFragment, "Detail History");
            scheduleAdapter.AddFragment(washHistoryFragment,"Wash History");
            schedulePager.Adapter = scheduleAdapter;
            scheduleTabs.SetupWithViewPager(schedulePager);
            schedulePager.SetOnPageChangeListener(this);             
            //schedulePager.SetCurrentItem(MyProfileInfoNeeds.selectedTab, false);
        }

        public void OnPageScrollStateChanged(int state)
        {
            
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            
        }

        public async void OnPageSelected(int position)
        {
            if(position == 0)
            {
                OnTipCalled();
            }
            if (position == 1) 
            {
                await pastServiceHistoryFragment.GetPastServices();                
            }
            if (position == 2)
            {
                OnTipCalled();
                await washHistoryFragment.GetPastWashDetails();
            }
        }
    }
    public class BottomSheet : BottomSheetBehavior.BottomSheetCallback
    {
        private BottomSheetBehavior tipBottomSheet;

        public BottomSheet(BottomSheetBehavior tipBottomSheet)
        {
            this.tipBottomSheet = tipBottomSheet;
        }

        public override void OnSlide(View bottomSheet, float slideOffset)
        {
           
        }

        public override void OnStateChanged(View bottomSheet, int newState)
        {
            if(tipBottomSheet.State == BottomSheetBehavior.StateHidden) 
            {
                ScheduleFragment.floatingActionButton.Visibility = ViewStates.Visible;
                ScheduleFragment.bottomNavigationView.Visibility = ViewStates.Visible;
            }           
            
        }
    }
}