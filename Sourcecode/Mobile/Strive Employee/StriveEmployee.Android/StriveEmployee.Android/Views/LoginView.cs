using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.Utils;
using Strive.Core.ViewModels;
using Strive.Core.ViewModels.Employee;
using Xamarin.Essentials;

namespace StriveEmployee.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Login View", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        private Button login_Button;
        private CheckBox rememberMe_CheckBox;
        private EditText emailPhone_EditText;
        private EditText password_EditText;
        private TextView loginHeading_TextView;
        private TextView rememberMe_TextView;
        private TextView newAccount;
        private TextView signUp;
        private TextView forgotPassword;
        private Button agreeButton;
        private Button disagreeButton;
        private LinearLayout termsLayout;
        private LinearLayout loginLayout;
        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor preferenceEditor;
        private bool hasAgreedToTerms;

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
            newAccount = FindViewById<TextView>(Resource.Id.newAccount);
            signUp = FindViewById<TextView>(Resource.Id.signUpLinkText);
            signUp.PaintFlags = PaintFlags.UnderlineText;
            forgotPassword = this.FindViewById<TextView>(Resource.Id.forgotPasswordLink);
            agreeButton = this.FindViewById<Button>(Resource.Id.btnAgree);
            disagreeButton = this.FindViewById<Button>(Resource.Id.btnDisagree);
            loginLayout = this.FindViewById<LinearLayout>(Resource.Id.loginLayout);
            termsLayout = this.FindViewById<LinearLayout>(Resource.Id.termsLayout);
            forgotPassword.PaintFlags = PaintFlags.UnderlineText;
            rememberMe_CheckBox.Click += RememberMe_CheckBox_Click;
            login_Button.Click += Login_Click;
            agreeButton.Click += AgreeButton_Click;
            disagreeButton.Click += DisagreeButton_Click;
            var bindingset = this.CreateBindingSet<LoginView, LoginViewModel>();

            bindingset.Bind(emailPhone_EditText).To(lvm => lvm.loginEmailPhone);
            bindingset.Bind(password_EditText).To(lvm => lvm.loginPassword);
            bindingset.Bind(loginHeading_TextView).To(lvm => lvm.Login);
            bindingset.Bind(login_Button).For(lvm => lvm.Text).To(lvm => lvm.Login);
            //bindingset.Bind(login_Button).To(lvm => lvm.Commands["DoLogin"]);
            bindingset.Bind(rememberMe_TextView).To(lvm => lvm.RememberPassword);
            bindingset.Bind(newAccount).To(lvm => lvm.NewAccount);
            bindingset.Bind(forgotPassword).To(lvm => lvm.ForgotPassword);
            bindingset.Bind(signUp).To(lvm => lvm.SignUp);
            bindingset.Apply();
            basicSetup();
            forgotPassword.Click += navigateToForgotPassword;           
            signUp.Click += navigateToSignUp;
        }

        private void DisagreeButton_Click(object sender, EventArgs e)
        {
            
        }

        private void AgreeButton_Click(object sender, EventArgs e)
        {
            preferenceEditor.PutBoolean("hasAgreedToTerms", true);
            preferenceEditor.Apply();
            termsLayout.Visibility = ViewStates.Gone;
            _ = ViewModel.DoLoginCommand();

        }

        private void Login_Click(object sender, EventArgs e)
        {
            if(rememberMe_CheckBox.Checked == true)
            {
                preferenceEditor.PutBoolean("rememberMe", rememberMe_CheckBox.Checked);
                preferenceEditor.PutString("loginId", emailPhone_EditText.Text);
                preferenceEditor.PutString("password", password_EditText.Text);
                preferenceEditor.Apply();
            }          
            hasAgreedToTerms = sharedPreferences.GetBoolean("hasAgreedToTerms",false);
            if (hasAgreedToTerms)
            {
                termsLayout.Visibility = ViewStates.Gone;
                _ = ViewModel.DoLoginCommand();
            }
            else 
            {
                loginLayout.Visibility = ViewStates.Gone;
                termsLayout.Visibility = ViewStates.Visible;
            }
            
        }
        private void navigateToSignUp(object sender, EventArgs e)
        {
            Browser.OpenAsync(ApiUtils.URL_CUSTOMER_SIGNUP, BrowserLaunchMode.SystemPreferred);
        }

        private void navigateToForgotPassword(object sender, EventArgs e)
        {
            ViewModel.ForgotPasswordCommand();
        }

        private void RememberMe_CheckBox_Click(object sender, EventArgs e)
        {
            preferenceEditor.PutBoolean("rememberMe", rememberMe_CheckBox.Checked);
            preferenceEditor.PutString("loginId", emailPhone_EditText.Text);
            preferenceEditor.PutString("password", password_EditText.Text);
            preferenceEditor.Apply();
        }

        private  void isCredentialStored(bool isRemember)
        {
            if (isRemember)
            {
                var loginId = sharedPreferences.GetString("loginId", null);
                emailPhone_EditText.SetText(loginId, null);
                var password = sharedPreferences.GetString("password", null);
                password_EditText.SetText(password, null);
                //if (!String.IsNullOrEmpty(emailPhone_EditText.Text) && !String.IsNullOrEmpty(password_EditText.Text))
                //{
                //    await ViewModel.DoLoginCommand();
                //}
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
        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                FinishAffinity();
            }

            return true;
        }
    }
}