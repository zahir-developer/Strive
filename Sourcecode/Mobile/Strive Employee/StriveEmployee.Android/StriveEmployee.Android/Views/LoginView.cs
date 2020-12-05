﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
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
        private Button login_Button;
        private CheckBox rememberMe_CheckBox;
        private EditText emailPhone_EditText;
        private EditText password_EditText;
        private TextView loginHeading_TextView;
        private TextView rememberMe_TextView;
        private TextView forgotPassword_TextView;
        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor preferenceEditor;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginViewScreen);

            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            preferenceEditor = sharedPreferences.Edit();

            login_Button = this.FindViewById<Button>(Resource.Id.loginButton);
            rememberMe_CheckBox = this.FindViewById<CheckBox>(Resource.Id.rememberMeCheck);
            emailPhone_EditText = this.FindViewById<EditText>(Resource.Id.emailPhoneInputs);
            password_EditText = this.FindViewById<EditText>(Resource.Id.passwordInputs);
            loginHeading_TextView = this.FindViewById<TextView>(Resource.Id.loginHeading);
            rememberMe_TextView = this.FindViewById<TextView>(Resource.Id.rememberMeLabel);
            forgotPassword_TextView = this.FindViewById<TextView>(Resource.Id.forgotPasswordLink);

            forgotPassword_TextView.PaintFlags = PaintFlags.UnderlineText;
            rememberMe_CheckBox.Click += RememberMe_CheckBox_Click;

            var bindingset = this.CreateBindingSet<LoginView, LoginViewModel>();

            bindingset.Bind(emailPhone_EditText).To(lvm => lvm.loginEmailPhone);
            bindingset.Bind(password_EditText).To(lvm => lvm.loginPassword);
            bindingset.Bind(loginHeading_TextView).To(lvm => lvm.Login);
            bindingset.Bind(login_Button).For(lvm => lvm.Text).To(lvm => lvm.Login);
            bindingset.Bind(login_Button).To(lvm => lvm.Commands["DoLogin"]);
            bindingset.Bind(rememberMe_TextView).To(lvm => lvm.RememberPassword);
            bindingset.Bind(forgotPassword_TextView).To(lvm => lvm.ForgotPassword);
            
            bindingset.Apply();

            basicSetup();

        }
        private void RememberMe_CheckBox_Click(object sender, EventArgs e)
        {
            preferenceEditor.PutBoolean("rememberMe", rememberMe_CheckBox.Checked);
            preferenceEditor.PutString("loginId", emailPhone_EditText.Text);
            preferenceEditor.PutString("password", password_EditText.Text);
            preferenceEditor.Apply();
        }

        private async void isCredentialStored(bool isRemember)
        {
            if (isRemember)
            {
                var loginId = sharedPreferences.GetString("loginId", null);
                emailPhone_EditText.SetText(loginId, null);
                var password = sharedPreferences.GetString("password", null);
                password_EditText.SetText(password, null);
                if (!String.IsNullOrEmpty(emailPhone_EditText.Text) && !String.IsNullOrEmpty(password_EditText.Text))
                {
                    //await ViewModel.DoLoginCommand();
                }
            }
            else
            {
                preferenceEditor.PutBoolean("rememberMe", isRemember);
                preferenceEditor.PutString("loginId", emailPhone_EditText.Text);
                preferenceEditor.PutString("password", password_EditText.Text);
                preferenceEditor.Apply();
            }
        }

        private void basicSetup()
        {
            rememberMe_CheckBox.Checked = sharedPreferences.GetBoolean("rememberMe", false);
            isCredentialStored(rememberMe_CheckBox.Checked);
        }
    }
}