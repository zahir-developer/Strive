using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.UIUtils;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
        public LoginView() : base("LoginView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(LoginButton).To(vm => vm.Commands["NavigationToClockIn"]);
            set.Bind(PasswordTxtField).For(p => p.SecureTextEntry).To(vm => vm.isPasswordSecure);
            set.Bind(UserIdTxtField).To(vm => vm.UserId);
            set.Bind(PasswordTxtField).To(vm => vm.Password);
            set.Apply();
            SetPasswordToggleImage();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;
            LoginButton.Layer.CornerRadius = 3;
           
        }

        partial void PasswordToggle(UIButton sender)
        {
            ViewModel.PasswordToggleCommand();
            SetPasswordToggleImage();
        }

        void SetPasswordToggleImage()
        {
            if (ViewModel.isPasswordSecure)
            {
                PasswordToggleButton.SetImage(UIImage.FromBundle("eye-filled"), UIControlState.Normal);
                return;
            }
            PasswordToggleButton.SetImage(UIImage.FromBundle("eye-filled-slash"), UIControlState.Normal);
        }
    }
}

