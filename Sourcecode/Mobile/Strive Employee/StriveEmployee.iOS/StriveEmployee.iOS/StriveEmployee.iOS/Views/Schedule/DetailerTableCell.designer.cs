// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveEmployee.iOS.Views.Schedule
{
	[Register ("DetailerTableCell")]
	partial class DetailerTableCell
	{
		[Outlet]
		UIKit.UIView DetailCellView { get; set; }

		[Outlet]
		UIKit.UILabel lblAdditionalService { get; set; }

		[Outlet]
		UIKit.UILabel lblDetailService { get; set; }

		[Outlet]
		UIKit.UILabel lblEstimateOut { get; set; }

		[Outlet]
		UIKit.UILabel lblTicketNumber { get; set; }

		[Outlet]
		UIKit.UILabel lblTimeIn { get; set; }

		[Outlet]
		UIKit.UILabel lblVehilce { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DetailCellView != null) {
				DetailCellView.Dispose ();
				DetailCellView = null;
			}

			if (lblTicketNumber != null) {
				lblTicketNumber.Dispose ();
				lblTicketNumber = null;
			}

			if (lblVehilce != null) {
				lblVehilce.Dispose ();
				lblVehilce = null;
			}

			if (lblDetailService != null) {
				lblDetailService.Dispose ();
				lblDetailService = null;
			}

			if (lblAdditionalService != null) {
				lblAdditionalService.Dispose ();
				lblAdditionalService = null;
			}

			if (lblTimeIn != null) {
				lblTimeIn.Dispose ();
				lblTimeIn = null;
			}

			if (lblEstimateOut != null) {
				lblEstimateOut.Dispose ();
				lblEstimateOut = null;
			}
		}
	}
}
