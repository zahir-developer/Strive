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
        private EditText signUpMobileNumber;
        private EditText signUpEmailId;
        private EditText signUpFirstName;
        private EditText signUpLastName;
        private EditText signUpPassword;
        private EditText signUpConfirmPassword;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SignUpScreen);
            signUpTextView = FindViewById<TextView>(Resource.Id.signUpTextView);
            signUpButton = FindViewById<Button>(Resource.Id.signUpButton);
            signUpMobileNumber = FindViewById<EditText>(Resource.Id.signUpMobile);
            signUpEmailId = FindViewById<EditText>(Resource.Id.signUpEmail);
            signUpFirstName = FindViewById<EditText>(Resource.Id.signUpFirstName);
            signUpLastName = FindViewById<EditText>(Resource.Id.signUpLastName);
            signUpPassword = FindViewById<EditText>(Resource.Id.signUpPassword);
            signUpConfirmPassword = FindViewById<EditText>(Resource.Id.signUpConfirmPassword);
            var signUpName = signUpFirstName + " " + signUpLastName;
            var bindingset = this.CreateBindingSet<SignUpView,SignUpViewModel>();

            bindingset.Bind(signUpTextView).To(svm => svm.SignUp);
           // bindingset.Bind(signUpButton).For(svm => svm.Text).To(svm => svm.SignUp);
            bindingset.Bind(signUpMobileNumber).To(svm => svm.signUpMobile);
            bindingset.Bind(signUpEmailId).To(svm => svm.signUpEmail);
            //bindingset.Bind(signUpName).To(svm => svm.signUpName);
            bindingset.Bind(signUpPassword).To(svm => svm.signUpPassword);
            bindingset.Bind(signUpConfirmPassword).To(svm => svm.signUpConfirmPassword);
            bindingset.Bind(signUpButton).To(svm => svm.Commands["SignUp"]);

            bindingset.Apply();
        }
    }
}