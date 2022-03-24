using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

namespace StriveEmployee.Android
{
    [MvxActivityPresentation]
    [Activity(
       MainLauncher = true,
       NoHistory = true,
       Icon = "@mipmap/ic_launcher",
       Theme = "@style/SplashTheme",
       ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
        {
        }
    }
}