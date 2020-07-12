using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Strive.Core.ViewModels.Customer;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "SignUp View")]
    public class SignUpView : MvxAppCompatActivity<SignUpViewModel>
    {
        
        private void Initialize()
        {
        }
    }
}