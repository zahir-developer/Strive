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

namespace StriveTimInventory.iOS.Views.MembershipView
{
    [Register ("SignatureView")]
    partial class SignatureView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DoneButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Xamarin.Controls.SignaturePadView SignPad { get; set; }

        [Action ("CancelButtonClicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CancelButtonClicked (UIKit.UIButton sender);

        [Action ("DoneButtonClicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DoneButtonClicked (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (BackButton != null) {
                BackButton.Dispose ();
                BackButton = null;
            }

            if (CancelButton != null) {
                CancelButton.Dispose ();
                CancelButton = null;
            }

            if (DoneButton != null) {
                DoneButton.Dispose ();
                DoneButton = null;
            }

            if (SignPad != null) {
                SignPad.Dispose ();
                SignPad = null;
            }
        }
    }
}