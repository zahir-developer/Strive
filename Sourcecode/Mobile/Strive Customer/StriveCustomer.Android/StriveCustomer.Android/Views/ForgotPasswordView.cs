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
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "ForgotPassword View")]
    class ForgotPasswordView : MvxAppCompatActivity<ForgotPasswordViewModel>
    {
        private Button getOTP;
        private TextView forgotPasswordTextView;
        private TextView OtpTextView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ForgotPasswordScreen);

            forgotPasswordTextView = FindViewById<TextView>(Resource.Id.forgotPasswordTextView);
            OtpTextView = FindViewById<TextView>(Resource.Id.receiveOTPTextView);
            getOTP = FindViewById<Button>(Resource.Id.getOTPButton);

            var bindingset = this.CreateBindingSet<ForgotPasswordView, ForgotPasswordViewModel>();

            bindingset.Bind(forgotPasswordTextView).To(fsvm => fsvm.ForgotPassword);
            bindingset.Bind(OtpTextView).To(fsvm => fsvm.ReceiveOTP);
            bindingset.Bind(getOTP).For(fsvm => fsvm.Text).To(fsvm => fsvm.GetOTP);
            bindingset.Bind(getOTP).To(fsvm => fsvm.Commands["GetOTP"]);

            bindingset.Apply();

        }
    }
}