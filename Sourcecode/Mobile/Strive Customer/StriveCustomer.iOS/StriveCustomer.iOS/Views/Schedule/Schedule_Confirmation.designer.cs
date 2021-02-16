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
	[Register ("Schedule_Confirmation")]
	partial class Schedule_Confirmation
	{
		[Outlet]
		UIKit.UIView ConfirmSchedule_View { get; set; }

		[Outlet]
		UIKit.UILabel ScheduleDate_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel ScheduleTime_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel ScheduleVehicleName_Lbl { get; set; }

		[Action ("BackDashboard_BtnTouch:")]
		partial void BackDashboard_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ConfirmSchedule_View != null) {
				ConfirmSchedule_View.Dispose ();
				ConfirmSchedule_View = null;
			}

			if (ScheduleDate_Lbl != null) {
				ScheduleDate_Lbl.Dispose ();
				ScheduleDate_Lbl = null;
			}

			if (ScheduleTime_Lbl != null) {
				ScheduleTime_Lbl.Dispose ();
				ScheduleTime_Lbl = null;
			}

			if (ScheduleVehicleName_Lbl != null) {
				ScheduleVehicleName_Lbl.Dispose ();
				ScheduleVehicleName_Lbl = null;
			}
		}
	}
}
