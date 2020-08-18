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
    [Register ("IconViewCell")]
    partial class IconViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView IconImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (IconImage != null) {
                IconImage.Dispose ();
                IconImage = null;
            }
        }
    }
}