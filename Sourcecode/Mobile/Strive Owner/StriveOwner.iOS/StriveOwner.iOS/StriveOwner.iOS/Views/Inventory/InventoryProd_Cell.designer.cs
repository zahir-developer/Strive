// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveOwner.iOS.Views.Inventory
{
	[Register ("InventoryProd_Cell")]
	partial class InventoryProd_Cell
	{
		[Outlet]
		UIKit.UIButton DecrementButton { get; set; }

		[Outlet]
		UIKit.UIButton IncrementButton { get; set; }

		[Outlet]
		UIKit.UILabel ItemCountLabel { get; set; }

		[Outlet]
		UIKit.UIView ItemCountOuterView { get; set; }

		[Outlet]
		UIKit.UIView ItemCountView { get; set; }

		[Outlet]
		UIKit.UILabel ItemDescription { get; set; }

		[Outlet]
		UIKit.UIButton ItemEditButton { get; set; }

		[Outlet]
		UIKit.UILabel ItemId { get; set; }

		[Outlet]
		UIKit.UIView ItemIdBackground { get; set; }

		[Outlet]
		UIKit.UIImageView ItemImage { get; set; }

		[Outlet]
		UIKit.UILabel ItemTitle { get; set; }

		[Outlet]
		UIKit.UIButton RequestButton { get; set; }

		[Outlet]
		UIKit.UIView RequestView { get; set; }

		[Outlet]
		UIKit.UILabel SupplierAddress { get; set; }

		[Outlet]
		UIKit.UILabel SupplierContact { get; set; }

		[Outlet]
		UIKit.UILabel SupplierEmail { get; set; }

		[Outlet]
		UIKit.UILabel SupplierFax { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint SupplierHeightConstraint { get; set; }

		[Outlet]
		UIKit.UILabel SupplierName { get; set; }

		[Outlet]
		UIKit.UIButton ViewMoreButton { get; set; }

		[Action ("EditItemTouch:")]
		partial void EditItemTouch (UIKit.UIButton sender);

		[Action ("RequestBtnTouch:")]
		partial void RequestBtnTouch (UIKit.UIButton sender);
		
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

			if (ItemDescription != null) {
				ItemDescription.Dispose ();
				ItemDescription = null;
			}

			if (ItemEditButton != null) {
				ItemEditButton.Dispose ();
				ItemEditButton = null;
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

			if (RequestButton != null) {
				RequestButton.Dispose ();
				RequestButton = null;
			}

			if (RequestView != null) {
				RequestView.Dispose ();
				RequestView = null;
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

			if (SupplierHeightConstraint != null) {
				SupplierHeightConstraint.Dispose ();
				SupplierHeightConstraint = null;
			}

			if (SupplierName != null) {
				SupplierName.Dispose ();
				SupplierName = null;
			}

			if (ViewMoreButton != null) {
				ViewMoreButton.Dispose ();
				ViewMoreButton = null;
			}
		}
	}
}
