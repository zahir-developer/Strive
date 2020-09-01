// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LoginButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PasswordHintLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PasswordToggleButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PasswordTxtField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserIdHintLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UserIdTxtField { get; set; }

        [Action ("PasswordToggle:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void PasswordToggle (UIKit.UIButton sender);

        [Action ("TextFieldBeginEdit:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldBeginEdit (UIKit.UITextField sender);

        [Action ("TextFieldChange:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldChange (UIKit.UITextField sender);

        [Action ("TextFieldEnd:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldEnd (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (LoginButton != null) {
                LoginButton.Dispose ();
                LoginButton = null;
            }

            if (PasswordHintLabel != null) {
                PasswordHintLabel.Dispose ();
                PasswordHintLabel = null;
            }

            if (PasswordToggleButton != null) {
                PasswordToggleButton.Dispose ();
                PasswordToggleButton = null;
            }

            if (PasswordTxtField != null) {
                PasswordTxtField.Dispose ();
                PasswordTxtField = null;
            }

            if (UserIdHintLabel != null) {
                UserIdHintLabel.Dispose ();
                UserIdHintLabel = null;
            }

            if (UserIdTxtField != null) {
                UserIdTxtField.Dispose ();
                UserIdTxtField = null;
            }
        }
    }
}