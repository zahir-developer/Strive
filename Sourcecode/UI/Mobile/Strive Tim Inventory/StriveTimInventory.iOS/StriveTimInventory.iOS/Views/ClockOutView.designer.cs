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
    [Register ("ClockOutView")]
    partial class ClockOutView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ClockInTimeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ClockInView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ClockOutTimeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ClockOutViewBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LogoutButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RoleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TotalHoursLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel WelcomeBackLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ClockInTimeLabel != null) {
                ClockInTimeLabel.Dispose ();
                ClockInTimeLabel = null;
            }

            if (ClockInView != null) {
                ClockInView.Dispose ();
                ClockInView = null;
            }

            if (ClockOutTimeLabel != null) {
                ClockOutTimeLabel.Dispose ();
                ClockOutTimeLabel = null;
            }

            if (ClockOutViewBox != null) {
                ClockOutViewBox.Dispose ();
                ClockOutViewBox = null;
            }

            if (DateLabel != null) {
                DateLabel.Dispose ();
                DateLabel = null;
            }

            if (LogoutButton != null) {
                LogoutButton.Dispose ();
                LogoutButton = null;
            }

            if (RoleLabel != null) {
                RoleLabel.Dispose ();
                RoleLabel = null;
            }

            if (TitleLabel != null) {
                TitleLabel.Dispose ();
                TitleLabel = null;
            }

            if (TotalHoursLabel != null) {
                TotalHoursLabel.Dispose ();
                TotalHoursLabel = null;
            }

            if (WelcomeBackLabel != null) {
                WelcomeBackLabel.Dispose ();
                WelcomeBackLabel = null;
            }
        }
    }
}