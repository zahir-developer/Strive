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
    [Register ("ExtraServiceView")]
    partial class ExtraServiceView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ExtraServiceTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton NextButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BackButton != null) {
                BackButton.Dispose ();
                BackButton = null;
            }

            if (ExtraServiceTable != null) {
                ExtraServiceTable.Dispose ();
                ExtraServiceTable = null;
            }

            if (NextButton != null) {
                NextButton.Dispose ();
                NextButton = null;
            }
        }
    }
}