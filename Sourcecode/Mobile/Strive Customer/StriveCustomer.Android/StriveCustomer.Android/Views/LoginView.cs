using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Login View", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginView : MvxAppCompatActivity<LoginViewModel>
    {
        private Button loginButton;
        private CheckBox rememberMeCheck;
        private EditText emailPhoneInput;
        private EditText passwordInput;
        private TextView signUp;
        private TextView forgotPassword;
        private TextView rememberMe;
        private TextView newAccount;
        private TextView loginTextView;
        private Button agreeButton;
        private Button disagreeButton;
        private LinearLayout termsLayout;
        private LinearLayout loginLayout;
        private bool hasAgreedToTerms;
        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor preferenceEditor;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);
            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            preferenceEditor = sharedPreferences.Edit();
            loginTextView = FindViewById<TextView>(Resource.Id.loginTextView);
            emailPhoneInput = FindViewById<EditText>(Resource.Id.emailPhoneInputs);
            passwordInput = FindViewById<EditText>(Resource.Id.passwordInputs);
            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            signUp = FindViewById<TextView>(Resource.Id.signUpLinkText);
            signUp.PaintFlags = PaintFlags.UnderlineText;
            rememberMe = FindViewById<TextView>(Resource.Id.rememberMeLabel);
            forgotPassword = FindViewById<TextView>(Resource.Id.forgotPasswordLink);
            forgotPassword.PaintFlags = PaintFlags.UnderlineText;
            newAccount = FindViewById<TextView>(Resource.Id.newAccount);
            rememberMeCheck = FindViewById<CheckBox>(Resource.Id.rememberMeCheck);
            agreeButton = this.FindViewById<Button>(Resource.Id.buttonAgree);
            disagreeButton = this.FindViewById<Button>(Resource.Id.buttonDisagree);
            loginLayout = this.FindViewById<LinearLayout>(Resource.Id.loginLayout);
            termsLayout = this.FindViewById<LinearLayout>(Resource.Id.termsLayout);

            var bindingset = this.CreateBindingSet<LoginView, LoginViewModel>();

            bindingset.Bind(emailPhoneInput).To(lvm => lvm.loginEmailPhone);
            bindingset.Bind(passwordInput).To(lvm => lvm.loginPassword);
            bindingset.Bind(loginTextView).To(lvm => lvm.Login);
            bindingset.Bind(loginButton).For(lvm => lvm.Text).To(lvm => lvm.Login);
            //bindingset.Bind(loginButton).To(lvm => lvm.Commands["DoLogin"]);
            bindingset.Bind(rememberMe).To(lvm => lvm.RememberPassword);
            bindingset.Bind(forgotPassword).To(lvm => lvm.ForgotPassword);
            bindingset.Bind(newAccount).To(lvm => lvm.NewAccount);
            bindingset.Bind(signUp).To(lvm => lvm.SignUp);
            bindingset.Apply();
            basicSetup();
            rememberMeCheck.Click += checkStoredCredentials;
            loginButton.Click += Login_Click;
           // signUp.Click += navigateToSignUp;
            forgotPassword.Click += navigateToForgotPassword;
            //#if DEBUG
            //            emailPhoneInput.Text = "ramtesting21@gmail.com";
            //            passwordInput.Text = "pass@123";
            //#endif

            signUp.Click += navigateToSignUp;
            forgotPassword.Click += navigateToForgotPassword;
            agreeButton.Click += AgreeButton_Click;
            disagreeButton.Click += DisagreeButton_Click;


        }

        private void DisagreeButton_Click(object sender, EventArgs e)
        {
            termsLayout.Visibility = ViewStates.Gone;
            loginLayout.Visibility = ViewStates.Visible;            
        }

        private void AgreeButton_Click(object sender, EventArgs e)
        {
            preferenceEditor.PutBoolean("hasAgreedToTerms", true);
            preferenceEditor.Apply();
            termsLayout.Visibility = ViewStates.Gone;
            loginLayout.Visibility = ViewStates.Visible;
            _ = ViewModel.DoLoginCommand();
        }

        private async void Login_Click(object sender, EventArgs e)
        {
            if (rememberMeCheck.Checked == true)
            {
                preferenceEditor.PutBoolean("rememberMe", rememberMeCheck.Checked);
                preferenceEditor.PutString("loginId", emailPhoneInput.Text);
                preferenceEditor.PutString("password", passwordInput.Text);
                preferenceEditor.Apply();
            }
            hasAgreedToTerms = sharedPreferences.GetBoolean("hasAgreedToTerms", false);
            if (hasAgreedToTerms)
            {
                termsLayout.Visibility = ViewStates.Gone;
                loginLayout.Visibility = ViewStates.Visible;
                await this.ViewModel.DoLoginCommand();
            }
            else
            {
                if (ViewModel.validateCommand())
                {
                    
                    loginLayout.Visibility = ViewStates.Gone;
                    termsLayout.Visibility = ViewStates.Visible;                    
                    HideSoftKeyboard(passwordInput);
                }
            }           

        }
        protected void HideSoftKeyboard(EditText input)
        {
            InputMethodManager inputMethod = (InputMethodManager)GetSystemService(Context.InputMethodService);
            inputMethod.HideSoftInputFromWindow(input.WindowToken, 0);
        }
        private void checkStoredCredentials(object o, EventArgs e)
        {
            preferenceEditor.PutBoolean("rememberMe", rememberMeCheck.Checked);
            preferenceEditor.PutString("loginId", emailPhoneInput.Text);
            preferenceEditor.PutString("password", passwordInput.Text);
            preferenceEditor.Apply();
        }

        private void isCredentialStored(bool isRemember)
        {
            if (isRemember)
            {
                var loginId = sharedPreferences.GetString("loginId", null);
                emailPhoneInput.SetText(loginId, null);
                var password = sharedPreferences.GetString("password", null);
                passwordInput.SetText(password, null);
                //if(!String.IsNullOrEmpty(emailPhoneInput.Text) && !String.IsNullOrEmpty(passwordInput.Text))
                //{
                //    await ViewModel.DoLoginCommand();
                //}
            }
            else
            {
                preferenceEditor.PutBoolean("rememberMe", isRemember);
                preferenceEditor.PutString("loginId", emailPhoneInput.Text);
                preferenceEditor.PutString("password", passwordInput.Text);
                preferenceEditor.Apply();
            }
        }

        private void navigateToSignUp(object o, EventArgs e)
        {

            BaseViewModel._navigationService.Navigate<SignUpViewModel>();
            //ViewModel.SignUpCommand();
            //Browser.OpenAsync(ApiUtils.URL_CUSTOMER_SIGNUP, BrowserLaunchMode.SystemPreferred);

        }

        private void navigateToForgotPassword(object o, EventArgs e)
        {
            ViewModel.ForgotPasswordCommand();
        }

        private void basicSetup()
        {
            rememberMeCheck.Checked = sharedPreferences.GetBoolean("rememberMe", false);
            isCredentialStored(rememberMeCheck.Checked);
            CustomerInfo.selectedMilesOption = sharedPreferences.GetString("milesoption", null);
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