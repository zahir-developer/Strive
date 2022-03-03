using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleFragment : MvxFragment<ScheduleViewModel>,ViewPager.IOnPageChangeListener
    {
        TabLayout scheduleTabs;
        ViewPager schedulePager;
        ViewPagerAdapter scheduleAdapter;
        SchedulePastServiceHistoryFragment pastServiceHistoryFragment;
        ScheduleVehicleListFragment vehicleListFragment;
        ScheduleWashHistoryFragment washHistoryFragment;
        BottomSheetBehavior tipBottomSheet;
        private FrameLayout tipFrameLayout;
        public TextView tipAmountOne;
        public TextView tipAmountTwo;
        public TextView tipAmountThree;
        public TextView tipAmountFour;
        public Button tipCancelButton;
        public static FloatingActionButton floatingActionButton;
        public static BottomNavigationView bottomNavigationView;
       
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
            washHistoryFragment = new ScheduleWashHistoryFragment();
            tipFrameLayout = rootView.FindViewById<FrameLayout>(Resource.Id.tipBottomSheet);
            tipBottomSheet = BottomSheetBehavior.From(tipFrameLayout);
            tipAmountOne = rootView.FindViewById<TextView>(Resource.Id.tipAmountOne);
            tipAmountTwo = rootView.FindViewById<TextView>(Resource.Id.tipAmountTwo);
            tipAmountThree = rootView.FindViewById<TextView>(Resource.Id.tipAmountThree);
            tipAmountFour = rootView.FindViewById<TextView>(Resource.Id.tipAmountFour);
            tipCancelButton = rootView.FindViewById<Button>(Resource.Id.tipCancelButton);
            pastServiceHistoryFragment = new SchedulePastServiceHistoryFragment(tipBottomSheet);
            

            tipAmountOne.Click += TipAmountOne_Click;
            tipAmountTwo.Click += TipAmountTwo_Click;
            tipAmountThree.Click += TipAmountThree_Click;
            tipAmountFour.Click += TipAmountFour_Click;
            tipCancelButton.Click += TipCancelButton_Click;
            CustomerScheduleInformation.ClearScheduleData();

            return rootView;
        }

        private void TipCancelButton_Click(object sender, EventArgs e)
        {            
            tipBottomSheet.State = BottomSheetBehavior.StateHidden;
            floatingActionButton.Visibility=ViewStates.Visible;
            bottomNavigationView.Visibility = ViewStates.Visible;
        }

        private void TipAmountFour_Click(object sender, EventArgs e)
        {
            ViewModel.WashTip = double.Parse(e.ToString());
            ViewModel.TipPayment();
        }

        private void TipAmountThree_Click(object sender, EventArgs e)
        {
            ViewModel.WashTip = double.Parse(e.ToString());
            ViewModel.TipPayment();
        }

        private void TipAmountTwo_Click(object sender, EventArgs e)
        {
            ViewModel.WashTip = double.Parse(e.ToString());
            ViewModel.TipPayment();
        }

        private void TipAmountOne_Click(object sender, EventArgs e)
        {
            ViewModel.WashTip = double.Parse(e.ToString());
            ViewModel.TipPayment();
        }

        public void TipAmounts(double[] TipAmounts)
        {
            tipAmountOne.Text = "$" + TipAmounts[0].ToString();
            tipAmountTwo.Text = "$" + TipAmounts[1].ToString();
            tipAmountThree.Text = "$" + TipAmounts[2].ToString();
            tipAmountFour.Text = "$" + TipAmounts[3].ToString();
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

        public void OnPageSelected(int position)
        {
            if (position == 0) 
            { 
            
            }
            if (position == 1) 
            {
                pastServiceHistoryFragment.GetPastServices();
            }
        }
    }
}