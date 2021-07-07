using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

namespace StriveOwner.Android
{
    [MvxActivityPresentation]
    [Activity(
       MainLauncher = true,
       NoHistory = true,
       Icon = "@@drawable/strive_logo_page",
       Theme = "@style/SplashTheme",
       ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
        {
        }
    }
}