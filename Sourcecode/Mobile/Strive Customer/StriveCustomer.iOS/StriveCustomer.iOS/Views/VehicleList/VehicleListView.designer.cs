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

namespace StriveCustomer.iOS.Views
{
    [Register ("VehicleListView")]
    partial class VehicleListView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Firstitem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SecondItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ThirdItem { get; set; }

        [Action ("VehicleTapped:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void VehicleTapped (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (Firstitem != null) {
                Firstitem.Dispose ();
                Firstitem = null;
            }

            if (SecondItem != null) {
                SecondItem.Dispose ();
                SecondItem = null;
            }

            if (ThirdItem != null) {
                ThirdItem.Dispose ();
                ThirdItem = null;
            }
        }
    }
}