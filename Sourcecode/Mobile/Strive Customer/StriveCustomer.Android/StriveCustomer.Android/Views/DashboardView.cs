using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Dashboard View")]
    public class DashboardView : MvxActivity<DashboardViewModel>
    {
        private BottomNavigationView bottomNav;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DashboardScreen);
            bottomNav = FindViewById<BottomNavigationView>(Resource.Id.bottomNav);
            bottomNav.InflateMenu(Resource.Menu.bottomNavMenu);
        }
        
    }
}