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
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Fragments;
using StriveEmployee.Android.Fragments.CheckOut;
using StriveEmployee.Android.Fragments.MyProfile;
using StriveEmployee.Android.Fragments.MyTicket;
using StriveEmployee.Android.Fragments.Payroll;
using StriveEmployee.Android.Fragments.Schedule;

namespace StriveEmployee.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Dashboard View", ScreenOrientation = ScreenOrientation.Portrait)]
    public class DashboardView : MvxAppCompatActivity<DashboardViewModel>
    {

        private BottomNavigationView bottom_NavigationView;
        private MvxFragment selected_MvxFragment;
        private MessengerFragment messenger_Fragment;
        private MyProfileFragment profile_Fragment;
        private ScheduleFragment schedule_Fragment;
        private MyTicketFragment myTicket_Fragment;
        private CheckOutFragment checkOut_Fragment;
        private PayRollFragment payRoll_Fragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DashboardViewScreen);
            this.ViewModel = new DashboardViewModel();
            messenger_Fragment = new MessengerFragment();
            profile_Fragment = new MyProfileFragment();
            schedule_Fragment = new ScheduleFragment();
            myTicket_Fragment = new MyTicketFragment();
            checkOut_Fragment = new CheckOutFragment(this);
            payRoll_Fragment = new PayRollFragment();
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
                case Resource.Id.menu_messenger:
                    selected_MvxFragment = messenger_Fragment;
                    break;

                case Resource.Id.menu_schedule:
                    selected_MvxFragment = schedule_Fragment;
                    break;

                case Resource.Id.menu_profile:
                    selected_MvxFragment = profile_Fragment;
                    break;

                //case Resource.Id.menu_myTickets:
                //    selected_MvxFragment = myTicket_Fragment;
                //    break;

                case Resource.Id.menu_checkOut:
                    selected_MvxFragment = checkOut_Fragment;
                    break;
                case Resource.Id.menu_payRoll:
                    selected_MvxFragment = payRoll_Fragment;
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
            selected_MvxFragment = messenger_Fragment;
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