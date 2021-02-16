// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveCustomer.iOS.Views.Schedule
{
	[Register ("Schedule_Preview")]
	partial class Schedule_Preview
	{
		[Outlet]
		UIKit.UILabel Appointment_PreviewDate_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel Appointment_PreviewTime_Lbl { get; set; }

		[Outlet]
		UIKit.UIButton BookNow_PreviewBtn { get; set; }

		[Outlet]
		UIKit.UIButton Cancel_BtnPreview { get; set; }

		[Outlet]
		UIKit.UIView PreviewApp_ParentView { get; set; }

		[Outlet]
		UIKit.UIView PreviewDetail_View1 { get; set; }

		[Outlet]
		UIKit.UIView PreviewDetail_View2 { get; set; }

		[Outlet]
		UIKit.UIView PreviewDetail_View3 { get; set; }

		[Outlet]
		UIKit.UILabel PreviewServiceLocation_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel PreviewServiceName_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel PreviewVehicleName_Lbl { get; set; }

		[Action ("BookNow_PreviewTouch:")]
		partial void BookNow_PreviewTouch (UIKit.UIButton sender);

		[Action ("CancelPreview_Touch:")]
		partial void CancelPreview_Touch (UIKit.UIButton sender);

		[Action ("Reschedule_LblTouch:")]
		partial void Reschedule_LblTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PreviewApp_ParentView != null) {
				PreviewApp_ParentView.Dispose ();
				PreviewApp_ParentView = null;
			}

			if (Appointment_PreviewDate_Lbl != null) {
				Appointment_PreviewDate_Lbl.Dispose ();
				Appointment_PreviewDate_Lbl = null;
			}

			if (Appointment_PreviewTime_Lbl != null) {
				Appointment_PreviewTime_Lbl.Dispose ();
				Appointment_PreviewTime_Lbl = null;
			}

			if (PreviewVehicleName_Lbl != null) {
				PreviewVehicleName_Lbl.Dispose ();
				PreviewVehicleName_Lbl = null;
			}

			if (PreviewServiceName_Lbl != null) {
				PreviewServiceName_Lbl.Dispose ();
				PreviewServiceName_Lbl = null;
			}

			if (PreviewServiceLocation_Lbl != null) {
				PreviewServiceLocation_Lbl.Dispose ();
				PreviewServiceLocation_Lbl = null;
			}

			if (PreviewDetail_View1 != null) {
				PreviewDetail_View1.Dispose ();
				PreviewDetail_View1 = null;
			}

			if (PreviewDetail_View2 != null) {
				PreviewDetail_View2.Dispose ();
				PreviewDetail_View2 = null;
			}

			if (PreviewDetail_View3 != null) {
				PreviewDetail_View3.Dispose ();
				PreviewDetail_View3 = null;
			}

			if (Cancel_BtnPreview != null) {
				Cancel_BtnPreview.Dispose ();
				Cancel_BtnPreview = null;
			}

			if (BookNow_PreviewBtn != null) {
				BookNow_PreviewBtn.Dispose ();
				BookNow_PreviewBtn = null;
			}
		}
	}
}
