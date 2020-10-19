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
using MvvmCross.Platforms.Android.Views;
using Strive.Core.ViewModels.Employee;

namespace StriveEmployee.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Login View")]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginViewScreen);

        }

        private void Buttons_Click(object sender, EventArgs e)
        {
            this.ViewModel.NavigateTO();
        }
    }
}