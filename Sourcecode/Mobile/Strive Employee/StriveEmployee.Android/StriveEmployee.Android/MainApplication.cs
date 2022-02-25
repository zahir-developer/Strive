using System;
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android;
using StriveEmployee.Android.MvvmCross;

namespace StriveEmployee.Android
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
            AppCenter.Start("6abbcdd4-bc5a-42b4-9077-db8006de8e3f", typeof(Analytics), typeof(Crashes));
            Crashes.NotifyUserConfirmation(UserConfirmation.AlwaysSend);
            base.OnCreate();
           
        }
    }
}
