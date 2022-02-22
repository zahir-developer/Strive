using System;
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android;
using Strive.Core;
using StriveCustomer.Android.MvvmCross;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace StriveCustomer.Android
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
            Xamarin.Essentials.Platform.Init(this);

        }
        public override void OnCreate()
        {
            AppCenter.Start("cff82a33-966f-4d0f-98a2-aa32df60da52",typeof(Analytics), typeof(Crashes));
            Crashes.NotifyUserConfirmation(UserConfirmation.AlwaysSend);
            base.OnCreate();
        }
    }
}
