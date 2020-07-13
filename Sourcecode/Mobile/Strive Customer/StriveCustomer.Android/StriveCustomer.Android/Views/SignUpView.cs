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
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "SignUp View")]
    public class SignUpView : MvxAppCompatActivity<SignUpViewModel>
    {
        private Button signUpButton;
        private TextView signUpTextView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SignUpScreen);
            signUpTextView = FindViewById<TextView>(Resource.Id.signUpTextView);
            signUpButton = FindViewById<Button>(Resource.Id.signUpButton);

            var bindingset = this.CreateBindingSet<SignUpView,SignUpViewModel>();

            bindingset.Bind(signUpTextView).To(svm => svm.SignUp);
            bindingset.Bind(signUpButton).For(svm => svm.Text).To(svm => svm.SignUp);

            bindingset.Apply();
        }
    }
}