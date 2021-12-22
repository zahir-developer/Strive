using System;
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android;
using Strive.Core;
using StriveCustomer.Android.MvvmCross;

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
    }
}
