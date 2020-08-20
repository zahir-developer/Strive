// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    [Register ("ClientTableViewCell")]
    partial class ClientTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ClientName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView MemebrshipImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ClientName != null) {
                ClientName.Dispose ();
                ClientName = null;
            }

            if (MemebrshipImage != null) {
                MemebrshipImage.Dispose ();
                MemebrshipImage = null;
            }
        }
    }
}