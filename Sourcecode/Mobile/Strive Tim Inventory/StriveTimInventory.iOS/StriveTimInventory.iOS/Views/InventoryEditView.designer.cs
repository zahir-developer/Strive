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
    [Register ("InventoryEditView")]
    partial class InventoryEditView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ItemCode { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ItemDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ItemName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ItemQuantity { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LogoutButtton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SupplierAddress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SupplierContact { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SupplierEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SupplierFax { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SupplierName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Title { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BackButton != null) {
                BackButton.Dispose ();
                BackButton = null;
            }

            if (CancelButton != null) {
                CancelButton.Dispose ();
                CancelButton = null;
            }

            if (ItemCode != null) {
                ItemCode.Dispose ();
                ItemCode = null;
            }

            if (ItemDescription != null) {
                ItemDescription.Dispose ();
                ItemDescription = null;
            }

            if (ItemName != null) {
                ItemName.Dispose ();
                ItemName = null;
            }

            if (ItemQuantity != null) {
                ItemQuantity.Dispose ();
                ItemQuantity = null;
            }

            if (LogoutButtton != null) {
                LogoutButtton.Dispose ();
                LogoutButtton = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }

            if (SupplierAddress != null) {
                SupplierAddress.Dispose ();
                SupplierAddress = null;
            }

            if (SupplierContact != null) {
                SupplierContact.Dispose ();
                SupplierContact = null;
            }

            if (SupplierEmail != null) {
                SupplierEmail.Dispose ();
                SupplierEmail = null;
            }

            if (SupplierFax != null) {
                SupplierFax.Dispose ();
                SupplierFax = null;
            }

            if (SupplierName != null) {
                SupplierName.Dispose ();
                SupplierName = null;
            }

            if (Title != null) {
                Title.Dispose ();
                Title = null;
            }
        }
    }
}