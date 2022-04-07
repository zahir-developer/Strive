// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    [Register("IconsView")]
    partial class IconsView
    {
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton CancelButton { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UICollectionView IconCollectionView { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (CancelButton != null)
            {
                CancelButton.Dispose();
                CancelButton = null;
            }

            if (IconCollectionView != null)
            {
                IconCollectionView.Dispose();
                IconCollectionView = null;
            }

            if (SaveButton != null)
            {
                SaveButton.Dispose();
                SaveButton = null;
            }
        }
    }
}
