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
        UIKit.UIImageView ImgView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ImgView != null) {
                ImgView.Dispose ();
                ImgView = null;
            }
        }
    }
}