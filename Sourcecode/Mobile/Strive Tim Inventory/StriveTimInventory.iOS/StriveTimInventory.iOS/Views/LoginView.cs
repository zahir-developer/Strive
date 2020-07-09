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
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;
            loginLabel.Font = DesignUtils.OpenSansBoldTitle();
            LoginButton.Font = DesignUtils.OpenSansBoldButton();
            UserIdTxtField.Font = DesignUtils.OpenSansRegularText();
            PasswordTxtField.Font = DesignUtils.OpenSansRegularText();
            LoginButton.Layer.CornerRadius = 3;
        }
    }
}

