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
using Strive.Core.ViewModels.Owner;

namespace StriveOwner.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "ConfirmPassword View")]
    public class ConfirmPasswordView : MvxAppCompatActivity<ConfirmPasswordViewModel>
    {
        private Button submitButton;
        private EditText newPassword;
        private EditText confirmPassword;
        private TextView confirmPasswordTitle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ConfirmPasswordScreen);

            submitButton = FindViewById<Button>(Resource.Id.submitButton);
            newPassword = FindViewById<EditText>(Resource.Id.newPasswordEditText);
            confirmPassword = FindViewById<EditText>(Resource.Id.confirmPasswordEditText);
            confirmPasswordTitle = FindViewById<TextView>(Resource.Id.newPasswordTitleTextView);

            var bindingset = this.CreateBindingSet<ConfirmPasswordView, ConfirmPasswordViewModel>();

            bindingset.Bind(confirmPasswordTitle).To(cpvm => cpvm.NewPasswordTitle);
            bindingset.Bind(newPassword).To(cpvm => cpvm.NewPassword);
            bindingset.Bind(confirmPassword).To(cpvm => cpvm.ConfirmPassword);
            bindingset.Bind(submitButton).For(cpvm => cpvm.Text).To(cpvm => cpvm.Submit);
            bindingset.Bind(submitButton).To(cpvm => cpvm.Commands["Submit"]);
          
            bindingset.Apply();
        }
    }
}