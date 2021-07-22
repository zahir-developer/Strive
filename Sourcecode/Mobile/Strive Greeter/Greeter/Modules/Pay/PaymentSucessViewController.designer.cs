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
	[Register ("PaymentSucessViewController")]
	partial class PaymentSucessViewController
	{
		[Outlet]
		UIKit.UIButton btnEmail { get; set; }

		[Outlet]
		UIKit.UIButton btnPrint { get; set; }

		[Outlet]
		UIKit.UIButton btnPrintAndEmail { get; set; }

		[Outlet]
		UIKit.UILabel lblAmount { get; set; }

		[Outlet]
		UIKit.UILabel lblPaymentMsg { get; set; }

		[Outlet]
		UIKit.UILabel lblReceipt { get; set; }

		[Outlet]
		UIKit.UILabel lblService { get; set; }

		[Outlet]
		UIKit.UILabel lblTicketId { get; set; }

		[Outlet]
		UIKit.UILabel lblVehicle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnEmail != null) {
				btnEmail.Dispose ();
				btnEmail = null;
			}

			if (btnPrint != null) {
				btnPrint.Dispose ();
				btnPrint = null;
			}

			if (btnPrintAndEmail != null) {
				btnPrintAndEmail.Dispose ();
				btnPrintAndEmail = null;
			}

			if (lblAmount != null) {
				lblAmount.Dispose ();
				lblAmount = null;
			}

			if (lblPaymentMsg != null) {
				lblPaymentMsg.Dispose ();
				lblPaymentMsg = null;
			}

			if (lblReceipt != null) {
				lblReceipt.Dispose ();
				lblReceipt = null;
			}

			if (lblService != null) {
				lblService.Dispose ();
				lblService = null;
			}

			if (lblTicketId != null) {
				lblTicketId.Dispose ();
				lblTicketId = null;
			}

			if (lblVehicle != null) {
				lblVehicle.Dispose ();
				lblVehicle = null;
			}

			if (lblService != null) {
				lblService.Dispose ();
				lblService = null;
			}
		}
	}
}
