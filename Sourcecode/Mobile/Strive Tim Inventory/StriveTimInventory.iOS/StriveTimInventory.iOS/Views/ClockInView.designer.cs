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
    [Register ("ClockInView")]
    partial class ClockInView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView RolesCollectionView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RolesCollectionView != null) {
                RolesCollectionView.Dispose ();
                RolesCollectionView = null;
            }
        }
    }
}