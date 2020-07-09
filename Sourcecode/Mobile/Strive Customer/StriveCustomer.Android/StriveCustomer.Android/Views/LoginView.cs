using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Login View")]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        private EditText emailPhoneInput;
        private EditText passwordInput;
        private TextView signUp;
        private TextView forgotPassword;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);
            emailPhoneInput = FindViewById<EditText>(Resource.Id.emailPhoneInputs);
            passwordInput = FindViewById<EditText>(Resource.Id.passwordInputs);
            signUp = FindViewById<TextView>(Resource.Id.signUpLinkText);
            signUp.PaintFlags = PaintFlags.UnderlineText;
            forgotPassword = FindViewById<TextView>(Resource.Id.forgotPasswordLink);
            forgotPassword.PaintFlags = PaintFlags.UnderlineText;
            var bindingset = this.CreateBindingSet<LoginView,LoginViewModel>();

            bindingset.Bind(emailPhoneInput).To(lvm => lvm.loginEmailPhone);
            bindingset.Bind(passwordInput).To(lvm => lvm.loginPassword);
            
            
            bindingset.Apply();
        }
    }
}