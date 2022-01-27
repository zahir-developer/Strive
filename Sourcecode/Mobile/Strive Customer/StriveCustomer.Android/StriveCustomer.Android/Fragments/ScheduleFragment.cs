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
            pastServiceHistoryFragment = new SchedulePastServiceHistoryFragment();
            vehicleListFragment = new ScheduleVehicleListFragment();

            CustomerScheduleInformation.ClearScheduleData();

            return rootView;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            scheduleAdapter = new ViewPagerAdapter(ChildFragmentManager);
            scheduleAdapter.AddFragment(vehicleListFragment, "Vehicle List");
            scheduleAdapter.AddFragment(pastServiceHistoryFragment, "Past Service History");
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