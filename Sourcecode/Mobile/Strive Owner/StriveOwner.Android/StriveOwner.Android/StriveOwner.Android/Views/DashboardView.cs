using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Fragments;
using StriveOwner.Android.Fragments.CheckOut;
using StriveOwner.Android.Resources.Fragments;

namespace StriveOwner.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Dashboard View", ScreenOrientation = ScreenOrientation.Portrait)]
    public class DashboardView : MvxAppCompatActivity<DashboardViewModel>
    {

        private BottomNavigationView bottom_NavigationView;
        private MvxFragment selected_MvxFragment;
        private MessengerFragment messenger_Fragment;
        private CheckOutFragment checkOut_Fragment;
        private DashboardHomeFragment dashboardhome_Fragment;
        private InventoryMainFragment inventoryMain_Fragment;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DashboardViewScreen);
            this.ViewModel = new DashboardViewModel();
            messenger_Fragment = new MessengerFragment();
            checkOut_Fragment = new CheckOutFragment(this);
            dashboardhome_Fragment = new DashboardHomeFragment();
            inventoryMain_Fragment = new InventoryMainFragment(this);
            bottom_NavigationView = FindViewById<BottomNavigationView>(Resource.Id.dash_bottomNav);
            bottom_NavigationView.InflateMenu(Resource.Menu.bottom_nav_menu);
            bottom_NavigationView.NavigationItemSelected += Bottom_NavigationView_NavigationItemSelected;
            SelectInitial_Fragment();
        }

        private void Bottom_NavigationView_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            selected_MvxFragment = null;
            switch (e.Item.ItemId)
            {
                case Resource.Id.dashboardhome:
                    selected_MvxFragment = dashboardhome_Fragment;
                    break;

                case Resource.Id.dashboardinventory:
                    selected_MvxFragment = inventoryMain_Fragment;
                    break;

                case Resource.Id.dashboardmessenger:
                    selected_MvxFragment = messenger_Fragment;
                    break;

                case Resource.Id.dashboardcheckOut:
                    selected_MvxFragment = checkOut_Fragment;
                    break;
            }
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

        }

        private void SelectInitial_Fragment()
        {
            selected_MvxFragment = dashboardhome_Fragment;
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }

        
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                this.ViewModel.Logout();
            }

            return true;
        }


    }
}