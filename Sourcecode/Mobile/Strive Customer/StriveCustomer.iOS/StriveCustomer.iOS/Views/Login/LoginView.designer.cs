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

namespace StriveCustomer.iOS.Views.Login
{
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CheckBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EmailTextfield { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ForgotPasswordButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LoginButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PasswordTextfield { get; set; }

        [Action ("CheckBoxClicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CheckBoxClicked (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (CheckBox != null) {
                CheckBox.Dispose ();
                CheckBox = null;
            }

            if (EmailTextfield != null) {
                EmailTextfield.Dispose ();
                EmailTextfield = null;
            }

            if (ForgotPasswordButton != null) {
                ForgotPasswordButton.Dispose ();
                ForgotPasswordButton = null;
            }

            if (LoginButton != null) {
                LoginButton.Dispose ();
                LoginButton = null;
            }

            if (PasswordTextfield != null) {
                PasswordTextfield.Dispose ();
                PasswordTextfield = null;
            }
        }
    }
}