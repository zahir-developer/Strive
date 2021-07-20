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
	[Register ("ScheduleView")]
	partial class ScheduleView
	{
		[Outlet]
		UIKit.UITableView empSchedule_TableView { get; set; }

		[Outlet]
		UIKit.UIDatePicker ScheduleDateView { get; set; }

		[Outlet]
		UIKit.UIView ScheduleParentView { get; set; }

		[Action ("scheduleDate_Touch:")]
		partial void scheduleDate_Touch (UIKit.UIDatePicker sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (empSchedule_TableView != null) {
				empSchedule_TableView.Dispose ();
				empSchedule_TableView = null;
			}

			if (ScheduleDateView != null) {
				ScheduleDateView.Dispose ();
				ScheduleDateView = null;
			}

			if (ScheduleParentView != null) {
				ScheduleParentView.Dispose ();
				ScheduleParentView = null;
			}
		}
	}
}
