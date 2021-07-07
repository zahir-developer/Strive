using System;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using StriveOwner.iOS.UIUtils;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace StriveOwner.iOS.Views.Login
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
            set.Bind(LoginBtn).To(vm => vm.Commands["DoLogin"]);
            set.Apply();
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
            LoginBtn.Layer.CornerRadius = 5;
        }

        partial void CheckBoxClicked(UIButton sender)
        {
            ViewModel.RememberMeButtonCommand();
            SetRememberMe();
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

        void ClearCredentials()
        {
            var dictionary = Persistance.ToDictionary();
            foreach (var dict in dictionary)
            {
                Persistance.RemoveObject(dict.Key.ToString());
            }
        }

        void StoreCredentials()
        {
            Persistance.SetString(ViewModel.loginEmailPhone, UsernameKey);
            Persistance.SetString(ViewModel.loginPassword, PasswordKey);
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

