// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveCustomer.iOS.Views
{
	[Register ("VehicleListViewCell")]
	partial class VehicleListViewCell
	{
		[Outlet]
		UIKit.UIButton deleteVehicleBtn { get; set; }

		[Outlet]
		UIKit.UIButton DownloadTermsBtn { get; set; }

		[Outlet]
		UIKit.UIButton EditVehicleBtn { get; set; }

		[Outlet]
		UIKit.UILabel VehicleList_CarNameLabel { get; set; }

		[Outlet]
		UIKit.UIView VehicleList_CellView { get; set; }

		[Outlet]
		UIKit.UILabel VehicleList_MembershipLabel { get; set; }

		[Outlet]
		UIKit.UILabel VehicleList_RegNoLabel { get; set; }

		[Action ("DeleteVehicleList_BtnTouch:")]
		partial void DeleteVehicleList_BtnTouch (UIKit.UIButton sender);

		[Action ("DownloadVehicleList_BtnTouch:")]
		partial void DownloadVehicleList_BtnTouch (UIKit.UIButton sender);

		[Action ("EditVehicleList_BtnTouch:")]
		partial void EditVehicleList_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (deleteVehicleBtn != null) {
				deleteVehicleBtn.Dispose ();
				deleteVehicleBtn = null;
			}

			if (DownloadTermsBtn != null) {
				DownloadTermsBtn.Dispose ();
				DownloadTermsBtn = null;
			}

			if (EditVehicleBtn != null) {
				EditVehicleBtn.Dispose ();
				EditVehicleBtn = null;
			}

			if (VehicleList_CarNameLabel != null) {
				VehicleList_CarNameLabel.Dispose ();
				VehicleList_CarNameLabel = null;
			}

			if (VehicleList_CellView != null) {
				VehicleList_CellView.Dispose ();
				VehicleList_CellView = null;
			}

			if (VehicleList_MembershipLabel != null) {
				VehicleList_MembershipLabel.Dispose ();
				VehicleList_MembershipLabel = null;
			}

			if (VehicleList_RegNoLabel != null) {
				VehicleList_RegNoLabel.Dispose ();
				VehicleList_RegNoLabel = null;
			}
		}
	}
}
