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
        //public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        //{
        //    base.OnCreate(savedInstanceState, persistentState);
        //    SetContentView(Resource.Layout.activity_main);
        //}

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
        }
    }
}