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
using Firebase;
using Firebase.Messaging;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Strive.Core.Utils.Employee;
using StriveEmployee.Android.Views;
using Xamarin.Essentials;

namespace StriveEmployee.Android
{
    [MvxActivityPresentation]
    [Activity(
       MainLauncher = true,
       Icon = "@mipmap/ic_launcher",
       Theme = "@style/SplashTheme",
       ScreenOrientation = ScreenOrientation.Portrait,LaunchMode = LaunchMode.SingleTop)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
        {
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            FirebaseApp.InitializeApp(Application.Context);
            FirebaseMessaging.Instance.AutoInitEnabled = true;
            NotificationClickedOn(Intent);
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Log.Info("onnewintent", "calling");
            NotificationClickedOn(intent);

        }
        private void NotificationClickedOn(Intent intent)
        {
            bool isNotification = intent.GetBooleanExtra("IsFromNotification", EmployeeTempData.FromNotification);
            if(Platform.CurrentActivity.Class.SimpleName == "LoginView")
            {
                Intent loginView = new Intent(this, typeof(LoginView));
                StartActivity(loginView);
            }
            if (isNotification)
            {
                EmployeeTempData.FromNotification = isNotification;
                Log.Info("onnewintent", "true");
            }
            else
            {
                EmployeeTempData.FromNotification = false;
                Log.Info("onnewintent", "false");

            }
        }
    }
}