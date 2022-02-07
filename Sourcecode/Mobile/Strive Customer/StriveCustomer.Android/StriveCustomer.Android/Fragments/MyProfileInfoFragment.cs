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
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class MyProfileInfoFragment : MvxFragment<MyProfileInfoViewModel>,ViewPager.IOnPageChangeListener
    {
        TabLayout profileTabs;
        ViewPager profilePager;
        ViewPagerAdapter profileAdapter;
        PersonalInfoFragment personalInfo , personalInfo2;
        VehicleInfoFragment vehicleInfo;
        MyProfileInfoViewModel profile = new MyProfileInfoViewModel();
        PastDetailsFragment pastInfo;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.MyProfileInfoFragment, null);
            profileTabs = rootview.FindViewById<TabLayout>(Resource.Id.myProfileTab);
            profilePager = rootview.FindViewById<ViewPager>(Resource.Id.myProfilePager);
            personalInfo = new PersonalInfoFragment();
            vehicleInfo = new VehicleInfoFragment();
            pastInfo= new PastDetailsFragment();
            return rootview;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            profileAdapter = new ViewPagerAdapter(ChildFragmentManager);
            profileAdapter.AddFragment(personalInfo, "Personal Info");
            profileAdapter.AddFragment(vehicleInfo, "Signup Vehicles");
           // profileAdapter.AddFragment(pastInfo, "Past Details");
            profilePager.Adapter = profileAdapter;
            profileTabs.SetupWithViewPager(profilePager);
            profilePager.SetCurrentItem(MyProfileInfoNeeds.selectedTab, false);
            profilePager.SetOnPageChangeListener(this);
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
                personalInfo.GetClientInfo();
            }
            else if (position == 1)
            {
                vehicleInfo.GetVehicleList();
            }
            //else if (position == 2)
            //{
            //    pastInfo.GetPastDetails();
            //}

            

        }
    }
    public static class MyProfileInfoNeeds
    {
        public static int selectedTab { get; set; } = 0;
    }
}