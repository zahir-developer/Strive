using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using StriveEmployee.Android.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StriveEmployee.Android.Fragments.Schedule
{
    public class ScheduleMainFragment : MvxFragment
    {
        private TabLayout schedule_TabLayout;
        private ViewPager schedule_ViewPager;
        private ViewPagerAdapter schedule_ViewPagerAdapter;
        private ScheduleFragment scheduleFragment;
        private ScheduleDetailerFragment scheduleDetailerFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleMain_Fragment, null);
            schedule_TabLayout = rootView.FindViewById<TabLayout>(Resource.Id.schedule_TabLayout);
            schedule_ViewPager = rootView.FindViewById<ViewPager>(Resource.Id.schedule_ViewPager);
            scheduleFragment = new ScheduleFragment();
            scheduleDetailerFragment = new ScheduleDetailerFragment();
            return rootView;            
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            schedule_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            schedule_ViewPagerAdapter.AddFragment(scheduleFragment, "Schedule");
            schedule_ViewPagerAdapter.AddFragment(scheduleDetailerFragment, "Detailer");
            

            schedule_ViewPager.Adapter = schedule_ViewPagerAdapter;
            schedule_TabLayout.SetupWithViewPager(schedule_ViewPager);
           // schedule_ViewPager.SetCurrentItem(MyProfileInfoNeeds.selectedTab, false);
           // schedule_ViewPager.AddOnPageChangeListener(this);
        }
    }
}