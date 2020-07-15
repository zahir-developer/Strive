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
    [Register ("ClockedInView")]
    partial class ClockedInView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ClockinView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ClockoutView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ClockinView != null) {
                ClockinView.Dispose ();
                ClockinView = null;
            }

            if (ClockoutView != null) {
                ClockoutView.Dispose ();
                ClockoutView = null;
            }
        }
    }
}