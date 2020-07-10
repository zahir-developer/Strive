// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace StriveTimInventory.iOS.Views
{
    [Register ("EmployeeRolesCell")]
    partial class EmployeeRolesCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView RoleBackgroundImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView RoleIconImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RoleTitileLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RoleBackgroundImage != null) {
                RoleBackgroundImage.Dispose ();
                RoleBackgroundImage = null;
            }

            if (RoleIconImage != null) {
                RoleIconImage.Dispose ();
                RoleIconImage = null;
            }

            if (RoleTitileLabel != null) {
                RoleTitileLabel.Dispose ();
                RoleTitileLabel = null;
            }
        }
    }
}