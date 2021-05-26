// This file has been autogenerated from a class added in the UI designer.

using System;
using Greeter.Common;

namespace Greeter
{
    public partial class LoginViewController : BaseViewController
    {
        bool isEyeOpen = false;

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Initial UI Settings
            AddLeftPadding(tfUserId, UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            AddRightPadding(tfUserId, UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);

            AddLeftPadding(tfPswd, UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            AddRightPadding(tfPswd, UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            btnLogin.TouchUpInside += delegate
            {
                LoginClicked();
            };

            btnEye.TouchUpInside += delegate
            {
                ChaangeEye(isEyeOpen);
            };
        }

        void ChaangeEye(bool isOpen)
        {
            if (isOpen)
            {
                // TODO : Close Eye Image Change and disable text shown in pswd
            }
            else
            {
                // TODO : Open Eye Image Change and show text in pswd
            }
        }

        void LoginClicked()
        {
            // Validate Fields
            if (IsEmpty(tfUserId.Text) && IsEmpty(tfPswd.Text))
            {
                ShowAlertMsg(Common.Messages.USER_ID_AND_PSWD_EMPTY);
                return;
            }

            if (IsEmpty(tfUserId.Text))
            {
                ShowAlertMsg(Common.Messages.USER_ID_EMPTY);
                return;
            }

            if (IsEmpty(tfPswd.Text))
            {
                ShowAlertMsg(Common.Messages.PSWD_EMPTY);
                return;
            }

            // Navigation
            NavigateToLocationScreen();
        }

        void NavigateToLocationScreen()
        {
            var vcLocation = this.Storyboard.InstantiateViewController(nameof(LocationViewController));

            NavigateToWithAnim(vcLocation);
        }
    }
}
