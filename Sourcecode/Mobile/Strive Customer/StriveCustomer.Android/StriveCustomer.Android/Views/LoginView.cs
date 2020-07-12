using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
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
        private CheckBox rememberMe;
        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor preferenceEditor;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);
            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            preferenceEditor = sharedPreferences.Edit();
            emailPhoneInput = FindViewById<EditText>(Resource.Id.emailPhoneInputs);
            passwordInput = FindViewById<EditText>(Resource.Id.passwordInputs);
            signUp = FindViewById<TextView>(Resource.Id.signUpLinkText);
            signUp.PaintFlags = PaintFlags.UnderlineText;
            forgotPassword = FindViewById<TextView>(Resource.Id.forgotPasswordLink);
            forgotPassword.PaintFlags = PaintFlags.UnderlineText;
            rememberMe = FindViewById<CheckBox>(Resource.Id.rememberMeCheck);
            
            var bindingset = this.CreateBindingSet<LoginView,LoginViewModel>();

            bindingset.Bind(emailPhoneInput).To(lvm => lvm.loginEmailPhone);
            bindingset.Bind(passwordInput).To(lvm => lvm.loginPassword);
            
            bindingset.Apply();
            
            rememberMe.Checked = sharedPreferences.GetBoolean("rememberMe", false);
            this.isCredentialStored(rememberMe.Checked);
            rememberMe.Click += checkStoredCredentials;
            

        }

        private void checkStoredCredentials(object o,EventArgs e)
        {
            preferenceEditor.PutBoolean("rememberMe", rememberMe.Checked);
            preferenceEditor.PutString("loginId", emailPhoneInput.Text);
            preferenceEditor.PutString("password", passwordInput.Text);
            preferenceEditor.Apply();
        }
       
        private void isCredentialStored(bool isRemember)
        {
            if(isRemember)
            {
                var loginId = sharedPreferences.GetString("loginId", null);
                emailPhoneInput.SetText(loginId, null);
                var password = sharedPreferences.GetString("password",null);
                passwordInput.SetText(password,null);
            }
            else
            {
                preferenceEditor.PutBoolean("rememberMe",isRemember);
                preferenceEditor.PutString("loginId", emailPhoneInput.Text);
                preferenceEditor.PutString("password",passwordInput.Text);
                preferenceEditor.Apply();
            }
        }
    }
}