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
       Icon = "@drawable/strive_logo_splash",
       NoHistory = true,
        Theme ="@style/SplashTheme",
       ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() 
        {
        }
    }
}
