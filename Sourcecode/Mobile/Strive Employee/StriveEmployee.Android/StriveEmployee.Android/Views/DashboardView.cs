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
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Employee;

namespace StriveEmployee.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Dashboard View")]
    public class DashboardView : MvxAppCompatActivity<DashboardViewModel>
    {

        private BottomNavigationView bottomNav;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DashboardViewScreen);
            bottomNav = FindViewById<BottomNavigationView>(Resource.Id.dash_bottomNav);
            bottomNav.InflateMenu(Resource.Menu.bottom_nav_menu);
        }
    }
}