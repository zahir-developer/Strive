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
    [Register ("ChooseImageView")]
    partial class ChooseImageView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BrowseButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView BrowseView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CameraButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView CameraView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton IconButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView IconView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton NotNowButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BrowseButton != null) {
                BrowseButton.Dispose ();
                BrowseButton = null;
            }

            if (BrowseView != null) {
                BrowseView.Dispose ();
                BrowseView = null;
            }

            if (CameraButton != null) {
                CameraButton.Dispose ();
                CameraButton = null;
            }

            if (CameraView != null) {
                CameraView.Dispose ();
                CameraView = null;
            }

            if (IconButton != null) {
                IconButton.Dispose ();
                IconButton = null;
            }

            if (IconView != null) {
                IconView.Dispose ();
                IconView = null;
            }

            if (NotNowButton != null) {
                NotNowButton.Dispose ();
                NotNowButton = null;
            }
        }
    }
}