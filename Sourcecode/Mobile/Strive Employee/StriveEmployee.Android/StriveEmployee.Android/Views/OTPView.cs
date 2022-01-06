using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Employee;

namespace StriveEmployee.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "OTP View", ScreenOrientation = ScreenOrientation.Portrait)]
    class OTPView : MvxAppCompatActivity<OTPViewModel>
    {
        private TextView enterOTPTitleTextView;
        private TextView OTPTextView;
        private TextView notReceiveOTPTextView;
        private TextView resendOTPTextView;
        private Button verifyButton;
        private EditText otpBox1;
        private EditText otpBox2;
        private EditText otpBox3;
        private EditText otpBox4;
        StringBuilder builder = new StringBuilder();
        private string otpValue;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.OTPScreen);

            enterOTPTitleTextView = FindViewById<TextView>(Resource.Id.enterOTPTitleTextView);
            OTPTextView = FindViewById<TextView>(Resource.Id.OTPTextView);
            notReceiveOTPTextView = FindViewById<TextView>(Resource.Id.notReceiveOTPTextView);
            resendOTPTextView = FindViewById<TextView>(Resource.Id.resendOTPTextView);
            resendOTPTextView.PaintFlags = PaintFlags.UnderlineText;
            resendOTPTextView.Click += resendOTP;
            verifyButton = FindViewById<Button>(Resource.Id.verifyButton);
            otpBox1 = FindViewById<EditText>(Resource.Id.otpBox1);
            otpBox2 = FindViewById<EditText>(Resource.Id.otpBox2);
            otpBox3 = FindViewById<EditText>(Resource.Id.otpBox3);
            otpBox4 = FindViewById<EditText>(Resource.Id.otpBox4);

            otpBox1.TextChanged += changeFocus;
            otpBox2.TextChanged += changeFocus;
            otpBox3.TextChanged += changeFocus;
            otpBox4.TextChanged += changeFocus;

            var bindingset = this.CreateBindingSet<OTPView, OTPViewModel>();

            bindingset.Bind(enterOTPTitleTextView).To(ovm => ovm.EnterOTP);
            bindingset.Bind(OTPTextView).To(ovm => ovm.SentOTP);
            bindingset.Bind(notReceiveOTPTextView).To(ovm => ovm.NotReceiveOTP);
            bindingset.Bind(resendOTPTextView).To(ovm => ovm.ResendOTP);
            bindingset.Bind(verifyButton).For(ovm => ovm.Text).To(ovm => ovm.VerifyOTP);
            bindingset.Bind(otpValue).To(ovm => ovm.OTPValue);

            bindingset.Apply();

            verifyButton.Click += bindOTP;
        }
        private void changeFocus(object o, EventArgs e)
        {
            if (otpBox1.IsFocused)
                otpBox2.RequestFocus();
            else if (otpBox2.IsFocused)
                otpBox3.RequestFocus();
            else
                otpBox4.RequestFocus();
        }
        private void bindOTP(object o, EventArgs e)
        {
            builder.Clear();
            builder.Append(otpBox1.Text.ToString());
            builder.Append(otpBox2.Text.ToString());
            builder.Append(otpBox3.Text.ToString());
            builder.Append(otpBox4.Text.ToString());
            otpValue = builder.ToString();
            ViewModel.OTPValue = otpValue;
            ViewModel.VerifyCommand();
        }
        private void resendOTP(object o, EventArgs e)
        {
            ViewModel.resendOTPCommand();
        }
    }
}