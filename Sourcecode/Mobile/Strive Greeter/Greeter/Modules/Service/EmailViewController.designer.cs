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
	[Register ("EmailViewController")]
	partial class EmailViewController
	{
		[Outlet]
		UIKit.UIButton btnCustomerSend { get; set; }

		[Outlet]
		UIKit.UIButton btnEmpDropdown { get; set; }

		[Outlet]
		UIKit.UIButton btnEmpSent { get; set; }

		[Outlet]
		UIKit.UIButton btnPay { get; set; }

		[Outlet]
		UIKit.UIButton btnPrint { get; set; }

		[Outlet]
		UIKit.UITextField tfCust { get; set; }

		[Outlet]
		UIKit.UITextField tfEmp { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCustomerSend != null) {
				btnCustomerSend.Dispose ();
				btnCustomerSend = null;
			}

			if (btnEmpSent != null) {
				btnEmpSent.Dispose ();
				btnEmpSent = null;
			}

			if (btnPay != null) {
				btnPay.Dispose ();
				btnPay = null;
			}

			if (btnPrint != null) {
				btnPrint.Dispose ();
				btnPrint = null;
			}

			if (tfCust != null) {
				tfCust.Dispose ();
				tfCust = null;
			}

			if (tfEmp != null) {
				tfEmp.Dispose ();
				tfEmp = null;
			}

			if (btnEmpDropdown != null) {
				btnEmpDropdown.Dispose ();
				btnEmpDropdown = null;
			}
		}
	}
}
