using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Fragments;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Dashboard View", ScreenOrientation = ScreenOrientation.Portrait)]
    public class DashboardView : MvxAppCompatActivity<DashboardViewModel>
    {
        public BottomNavigationView bottomNav;
        public FloatingActionButton dashActionButton;
        private NotificationSettingsView notificationSettingsView;
        //public  BottomSheetBehavior tipBottomSheet;
        //private FrameLayout tipFrameLayout;
        //public TextView tipAmountOne;
        //public TextView tipAmountTwo;
        //public TextView tipAmountThree;
        //public TextView tipAmountFour;
        ScheduleFragment scheduleFrag;
        MvxFragment fragment = null;
        MapsFragment mapFrag = new MapsFragment();
        DealsFragment dealFrag = new DealsFragment();        
        PastDetailsFragment pastDetailsFrag = new PastDetailsFragment();
        MyProfileInfoFragment myProfileFrag = new MyProfileInfoFragment();
        ContactUsFragment contactFrag = new ContactUsFragment();
        MyProfileInfoViewModel MyProfileInfoViewModel = new MyProfileInfoViewModel();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DashboardScreen);
            notificationSettingsView = new NotificationSettingsView(this);
            bottomNav = FindViewById<BottomNavigationView>(Resource.Id.bottomNav);
            bottomNav.InflateMenu(Resource.Menu.bottomNavMenu);
            bottomNav.NavigationItemSelected += NavigateFrag;
            dashActionButton = FindViewById<FloatingActionButton>(Resource.Id.dashActionButton);
            scheduleFrag = new ScheduleFragment(this);
            ScheduleFragment.floatingActionButton = dashActionButton;
            ScheduleFragment.bottomNavigationView = bottomNav;
           // tipFrameLayout = FindViewById<FrameLayout>(Resource.Id.tipBottomSheet);
            //tipBottomSheet = BottomSheetBehavior.From(tipFrameLayout);
            //tipAmountOne = FindViewById<TextView>(Resource.Id.tipAmountOne);
            //tipAmountTwo = FindViewById<TextView>(Resource.Id.tipAmountTwo);
            //tipAmountThree = FindViewById<TextView>(Resource.Id.tipAmountThree);
            //tipAmountFour = FindViewById<TextView>(Resource.Id.tipAmountFour);
            dashActionButton.Click += DashActionButton_Click;
            setInitialFrag();
        }

        private void DashActionButton_Click(object sender, EventArgs e)        {
            
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFrag).Commit();
        }
        private void NavigateFrag(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            fragment = null;
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_Map:
                    fragment = mapFrag;
                    break;
                case Resource.Id.menu_Deals:
                    fragment = dealFrag;
                    break;
                case Resource.Id.menu_AboutUs:
                    fragment = myProfileFrag;
                    break;
                case Resource.Id.menu_Schedule:                    
                    fragment = contactFrag;
                    break;
            }
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
        }
        private void setInitialFrag()
        {
            fragment = mapFrag;
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
        }
        public override void OnBackPressed()
        {
            ViewModel.isAndroid = true;
            ViewModel.Logout();
        }
    }
}