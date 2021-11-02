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

namespace StriveOwner.iOS.Views.Login
{
    [Register ("OtpView")]
    partial class OtpView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField FirstDigit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField FourthDigit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ResendOtpButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SecondDigit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ThirdDigit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton VerifyButton { get; set; }

        [Action ("TextFieldDidChange:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TextFieldDidChange (UIKit.UITextField sender);

        [Action ("VerifyClicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void VerifyClicked (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (BackButton != null) {
                BackButton.Dispose ();
                BackButton = null;
            }

            if (FirstDigit != null) {
                FirstDigit.Dispose ();
                FirstDigit = null;
            }

            if (FourthDigit != null) {
                FourthDigit.Dispose ();
                FourthDigit = null;
            }

            if (ResendOtpButton != null) {
                ResendOtpButton.Dispose ();
                ResendOtpButton = null;
            }

            if (SecondDigit != null) {
                SecondDigit.Dispose ();
                SecondDigit = null;
            }

            if (ThirdDigit != null) {
                ThirdDigit.Dispose ();
                ThirdDigit = null;
            }

            if (VerifyButton != null) {
                VerifyButton.Dispose ();
                VerifyButton = null;
            }
        }
    }
}