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
    [Register ("VehicleMembershipDetailView")]
    partial class VehicleMembershipDetailView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ActivatedDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CancelledDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ChangeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MembershipName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Status { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivatedDate != null) {
                ActivatedDate.Dispose ();
                ActivatedDate = null;
            }

            if (BackButton != null) {
                BackButton.Dispose ();
                BackButton = null;
            }

            if (CancelButton != null) {
                CancelButton.Dispose ();
                CancelButton = null;
            }

            if (CancelledDate != null) {
                CancelledDate.Dispose ();
                CancelledDate = null;
            }

            if (ChangeButton != null) {
                ChangeButton.Dispose ();
                ChangeButton = null;
            }

            if (MembershipName != null) {
                MembershipName.Dispose ();
                MembershipName = null;
            }

            if (Status != null) {
                Status.Dispose ();
                Status = null;
            }
        }
    }
}