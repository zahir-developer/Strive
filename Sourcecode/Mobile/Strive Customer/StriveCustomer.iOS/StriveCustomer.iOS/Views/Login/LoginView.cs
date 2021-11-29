using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;
using Foundation;
using Strive.Core.Utils;

namespace StriveCustomer.iOS.Views.Login
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
        NSUserDefaults Persistance;
        string UsernameKey = "username";
        string PasswordKey = "password";

        public LoginView() : base("LoginView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(EmailTextfield).To(vm => vm.loginEmailPhone);
            set.Bind(PasswordTextfield).To(vm => vm.loginPassword);
            set.Bind(LoginButton).To(vm => vm.Commands["DoLogin"]);
            set.Bind(ForgotPasswordButton).To(vm => vm.Commands["ForgotPassword"]);
            set.Apply();

            SignupLbl.UserInteractionEnabled = true;

            Action action = () =>
            {
                UIApplication.SharedApplication.OpenUrl(new NSUrl(ApiUtils.URL_CUSTOMER_SIGNUP));
            };

            UITapGestureRecognizer tap = new UITapGestureRecognizer(action);
            SignupLbl.AddGestureRecognizer(tap);
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;
            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
            Persistance = NSUserDefaults.StandardUserDefaults;
            SetCredentials();
        }

        partial void CheckBoxClicked(UIButton sender)
        {
            ViewModel.RememberMeButtonCommand();
            SetRememberMe();
        }

        void StoreCredentials()
        {
            Persistance.SetString(ViewModel.loginEmailPhone, UsernameKey);
            Persistance.SetString(ViewModel.loginPassword, PasswordKey);
        }

        void ClearCredentials()
        {
            var dictionary = Persistance.ToDictionary();
            foreach(var dict in dictionary)
            {
                Persistance.RemoveObject(dict.Key.ToString());
            }
        }

        void SetCredentials()
        {
            var username = Persistance.StringForKey(UsernameKey);
            var password = Persistance.StringForKey(PasswordKey);
            if (username != null && password != null)
            {
                ViewModel.loginEmailPhone = username;
                ViewModel.loginPassword = password;
                ViewModel.rememberMe = true;
                SetRememberMe();
            }
        }

        void SetRememberMe()
        {
            if (ViewModel.rememberMe)
            {
                CheckBox.SetImage(UIImage.FromBundle("icon-checked"), UIControlState.Normal);
                StoreCredentials();
                return;
            }
            ClearCredentials();
            CheckBox.SetImage(UIImage.FromBundle("icon-unchecked"), UIControlState.Normal);
        }

        public override void ViewDidDisappear(bool animated)
        {
            if (ViewModel.rememberMe)
            {
                StoreCredentials();
            }
        }
    }
}

