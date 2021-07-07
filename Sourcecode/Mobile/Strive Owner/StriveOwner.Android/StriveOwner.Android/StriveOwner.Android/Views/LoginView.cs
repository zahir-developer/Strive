using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Owner;

namespace StriveOwner.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Login View",
        MainLauncher = true, 
        NoHistory = true)]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        private Button sample;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginActivity);
            sample = this.FindViewById<Button>(Resource.Id.loginButton);
            sample.Click += Sample_Click;
        }

        private void Sample_Click(object sender, EventArgs e)
        {
            this.ViewModel.getNavigation();
        }
    }
}