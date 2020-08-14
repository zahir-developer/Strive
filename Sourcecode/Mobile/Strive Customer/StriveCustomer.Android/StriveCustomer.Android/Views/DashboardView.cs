using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
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
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Fragments;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Dashboard View")]
    public class DashboardView : MvxAppCompatActivity<DashboardViewModel>
    {
        private BottomNavigationView bottomNav;
        private NotificationSettingsView notificationSettingsView;
        MvxFragment fragment = null;
        MapsFragment mapFrag = new MapsFragment();
        DealsFragment dealFrag = new DealsFragment();
        ScheduleFragment scheduleFrag = new ScheduleFragment();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DashboardScreen);
            notificationSettingsView = new NotificationSettingsView(this);
            bottomNav = FindViewById<BottomNavigationView>(Resource.Id.bottomNav);
            bottomNav.InflateMenu(Resource.Menu.bottomNavMenu);
            bottomNav.NavigationItemSelected += NavigateFrag;
        }

        private void NavigateFrag(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            fragment = null;
            //if(fragment == null)
            //{
            //    SupportFragmentManager.BeginTransaction().Remove(fragment).Commit();
            //}
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_Map:
                    fragment = mapFrag;
                    break;
                case Resource.Id.menu_Deals:
                    fragment = dealFrag;
                    break;
                case Resource.Id.menu_AboutUs:
                    fragment = dealFrag;
                    break;
                case Resource.Id.menu_Schedule:
                    fragment = scheduleFrag;
                    break;
            }
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();

        }
    }
}