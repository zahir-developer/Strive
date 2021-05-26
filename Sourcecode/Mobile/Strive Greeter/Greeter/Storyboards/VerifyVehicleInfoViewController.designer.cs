// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Greeter.Storyboards
{
	[Register ("VerifyVehicleInfoViewController")]
	partial class VerifyVehicleInfoViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnConfirm { get; set; }

		[Outlet]
		UIKit.UIView viewBarcode { get; set; }

		[Outlet]
		UIKit.UIView viewCustName { get; set; }

		[Outlet]
		UIKit.UIView viewType { get; set; }

		[Outlet]
		UIKit.UIView viewVehicle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewCustName != null) {
				viewCustName.Dispose ();
				viewCustName = null;
			}

			if (viewVehicle != null) {
				viewVehicle.Dispose ();
				viewVehicle = null;
			}

			if (viewBarcode != null) {
				viewBarcode.Dispose ();
				viewBarcode = null;
			}

			if (viewType != null) {
				viewType.Dispose ();
				viewType = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnConfirm != null) {
				btnConfirm.Dispose ();
				btnConfirm = null;
			}
		}
	}
}
