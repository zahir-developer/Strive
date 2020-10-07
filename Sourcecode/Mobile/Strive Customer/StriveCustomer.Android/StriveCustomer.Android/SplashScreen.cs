﻿using System;
using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;

namespace StriveCustomer.Android
{
    [MvxActivityPresentation]
    [Activity(
       MainLauncher = true,
       Icon = "@drawable/Strive_Logo_Page",
       Theme = "@style/SplashTheme",
       NoHistory = true,
       ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
        {
        }
    }
}
