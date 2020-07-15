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
    [Activity(Label = "OTP View")]
    class OTPView : MvxAppCompatActivity<OTPViewModel>
    {
        private TextView enterOTPTitleTextView;
        private TextView OTPTextView;
        private TextView notReceiveOTPTextView;
        private TextView resendOTPTextView;
        private Button resendButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.OTPScreen);

            enterOTPTitleTextView = FindViewById<TextView>(Resource.Id.enterOTPTitleTextView);
            OTPTextView = FindViewById<TextView>(Resource.Id.OTPTextView);
            notReceiveOTPTextView = FindViewById<TextView>(Resource.Id.notReceiveOTPTextView);
            resendOTPTextView = FindViewById<TextView>(Resource.Id.resendOTPTextView);
            resendButton = FindViewById<Button>(Resource.Id.resendButton);

            var bindingset = this.CreateBindingSet<OTPView, OTPViewModel>();

            bindingset.Bind(enterOTPTitleTextView).To(ovm => ovm.EnterOTP);
            bindingset.Bind(OTPTextView).To(ovm => ovm.SentOTP);
            bindingset.Bind(notReceiveOTPTextView).To(ovm => ovm.NotReceiveOTP);
            bindingset.Bind(resendOTPTextView).To(ovm => ovm.ResendOTP);
            bindingset.Bind(resendButton).For(ovm => ovm.Text).To(ovm => ovm.VerifyOTP);

            bindingset.Apply();
        }
    }
}