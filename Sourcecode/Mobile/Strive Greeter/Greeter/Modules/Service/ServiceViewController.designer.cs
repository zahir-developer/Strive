// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Greeter
{
	[Register ("ServiceViewController")]
	partial class ServiceViewController
	{
		[Outlet]
		UIKit.UIButton btnBack { get; set; }

		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnCloseBarcode { get; set; }

		[Outlet]
		UIKit.UIButton btnDetail { get; set; }

		[Outlet]
		UIKit.UIButton btnDriveUp { get; set; }

		[Outlet]
		UIKit.UIButton btnSelect { get; set; }

		[Outlet]
		UIKit.UIButton btnWash { get; set; }

		[Outlet]
		UIKit.UILabel lblLastService { get; set; }

		[Outlet]
		UIKit.UILabel lblViewIssue { get; set; }

		[Outlet]
		UIKit.UILabel lblWashTime { get; set; }

		[Outlet]
		UIKit.UITextField txtFieldBarcode { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}

			if (btnWash != null) {
				btnWash.Dispose ();
				btnWash = null;
			}

			if (btnDetail != null) {
				btnDetail.Dispose ();
				btnDetail = null;
			}

			if (txtFieldBarcode != null) {
				txtFieldBarcode.Dispose ();
				txtFieldBarcode = null;
			}

			if (btnCloseBarcode != null) {
				btnCloseBarcode.Dispose ();
				btnCloseBarcode = null;
			}

			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnDriveUp != null) {
				btnDriveUp.Dispose ();
				btnDriveUp = null;
			}

			if (btnSelect != null) {
				btnSelect.Dispose ();
				btnSelect = null;
			}

			if (lblWashTime != null) {
				lblWashTime.Dispose ();
				lblWashTime = null;
			}

			if (lblLastService != null) {
				lblLastService.Dispose ();
				lblLastService = null;
			}

			if (lblViewIssue != null) {
				lblViewIssue.Dispose ();
				lblViewIssue = null;
			}
		}
	}
}
