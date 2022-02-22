using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android;
using StriveOwner.Android.MvvmCross;

namespace StriveOwner.Android
{
    [Application(UsesCleartextTraffic = true)]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication()
        {
        }

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            UserDialogs.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        }
        public override void OnCreate()
        {
            AppCenter.Start("04a4bc13-47f3-482b-aeb6-560821e76746",typeof(Analytics), typeof(Crashes));
            Crashes.NotifyUserConfirmation(UserConfirmation.AlwaysSend);
            base.OnCreate();
        }
    }
}