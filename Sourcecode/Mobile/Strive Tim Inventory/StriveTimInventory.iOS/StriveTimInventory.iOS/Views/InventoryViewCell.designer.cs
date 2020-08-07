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
    [Register ("InventoryViewCell")]
    partial class InventoryViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DecrementButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton IncrementButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ItemCountLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ItemCountOuterView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ItemCountView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ItemDeleteButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ItemDescritption { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ItemId { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ItemIdBackground { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ItemImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ItemTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ViewMoreButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DecrementButton != null) {
                DecrementButton.Dispose ();
                DecrementButton = null;
            }

            if (IncrementButton != null) {
                IncrementButton.Dispose ();
                IncrementButton = null;
            }

            if (ItemCountLabel != null) {
                ItemCountLabel.Dispose ();
                ItemCountLabel = null;
            }

            if (ItemCountOuterView != null) {
                ItemCountOuterView.Dispose ();
                ItemCountOuterView = null;
            }

            if (ItemCountView != null) {
                ItemCountView.Dispose ();
                ItemCountView = null;
            }

            if (ItemDeleteButton != null) {
                ItemDeleteButton.Dispose ();
                ItemDeleteButton = null;
            }

            if (ItemDescritption != null) {
                ItemDescritption.Dispose ();
                ItemDescritption = null;
            }

            if (ItemId != null) {
                ItemId.Dispose ();
                ItemId = null;
            }

            if (ItemIdBackground != null) {
                ItemIdBackground.Dispose ();
                ItemIdBackground = null;
            }

            if (ItemImage != null) {
                ItemImage.Dispose ();
                ItemImage = null;
            }

            if (ItemTitle != null) {
                ItemTitle.Dispose ();
                ItemTitle = null;
            }

            if (ViewMoreButton != null) {
                ViewMoreButton.Dispose ();
                ViewMoreButton = null;
            }
        }
    }
}