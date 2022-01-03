using System;
using Acr.UserDialogs;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Utils.TimInventory;
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
            set.Bind(LoginButton).To(vm => vm.Commands["Login"]);
            set.Bind(PasswordTxtField).For(p => p.SecureTextEntry).To(vm => vm.isPasswordSecure);
            set.Bind(UserIdTxtField).To(vm => vm.UserId);
            set.Bind(PasswordTxtField).To(vm => vm.Password);
            set.Apply();
            
            SetPasswordToggleImage();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            UserIdTxtField.BecomeFirstResponder();

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
            UserIdHintLabel.Hidden = true;
            PasswordHintLabel.Hidden = true;
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
                PasswordToggleButton.SetImage(UIImage.FromBundle(ImageUtils.ICON_EYE_FILLED), UIControlState.Normal);
                return;
            }
            PasswordToggleButton.SetImage(UIImage.FromBundle(ImageUtils.ICON_EYE_FILLED_SLASH), UIControlState.Normal);
        }

        partial void TextFieldBeginEdit(UITextField sender)
        {

            if (sender.Tag == 1)
            {
                UserIdHintLabel.Hidden = false;
                UserIdTxtField.Placeholder = string.Empty;
                return;
            }
            PasswordHintLabel.Hidden = false;
            PasswordTxtField.Placeholder = string.Empty;
        }

        partial void TextFieldChange(UITextField sender)
        {
            
            if (sender.Tag == 1)
            {
                if (string.IsNullOrEmpty(sender.Text))
                {
                    UserIdHintLabel.Hidden = true;
                    UserIdTxtField.Placeholder = ViewModel.UserIdText;
                }
                else
                {
                    UserIdHintLabel.Hidden = false;
                    UserIdTxtField.Placeholder = string.Empty;
                }
                return;
            }
            if (string.IsNullOrEmpty(sender.Text))
            {
                PasswordHintLabel.Hidden = true;
                PasswordTxtField.Placeholder = ViewModel.PasswordText;
            }
            else
            {
                PasswordHintLabel.Hidden = false;
                PasswordTxtField.Placeholder = string.Empty;
            }
        }

        partial void TextFieldEnd(UITextField sender)
        {
            
            if (sender.Tag == 1)
            {
                if(string.IsNullOrEmpty(UserIdTxtField.Text))
                {
                    UserIdHintLabel.Hidden = true;
                }
                UserIdTxtField.Placeholder = ViewModel.UserIdText;
                return;
            }
            if (string.IsNullOrEmpty(PasswordTxtField.Text))
            {
                PasswordHintLabel.Hidden = true;
            }
            PasswordTxtField.Placeholder = ViewModel.PasswordText;
        }

       
    }
}

