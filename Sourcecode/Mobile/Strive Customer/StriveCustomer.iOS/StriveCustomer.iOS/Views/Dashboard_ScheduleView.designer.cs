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
	[Register ("Dashboard_ScheduleView")]
	partial class Dashboard_ScheduleView
	{
		[Outlet]
		UIKit.UIView Schedule_ParentView { get; set; }

		[Outlet]
		UIKit.UIView Schedule_Seg1 { get; set; }

		[Outlet]
		UIKit.UISegmentedControl Schedule_SegmentView { get; set; }

		[Outlet]
		UIKit.UITableView ScheduleVehicle_TableView { get; set; }

		[Action ("Schedule_SegTouch:")]
		partial void Schedule_SegTouch (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Schedule_ParentView != null) {
				Schedule_ParentView.Dispose ();
				Schedule_ParentView = null;
			}

			if (Schedule_Seg1 != null) {
				Schedule_Seg1.Dispose ();
				Schedule_Seg1 = null;
			}

			if (Schedule_SegmentView != null) {
				Schedule_SegmentView.Dispose ();
				Schedule_SegmentView = null;
			}

			if (ScheduleVehicle_TableView != null) {
				ScheduleVehicle_TableView.Dispose ();
				ScheduleVehicle_TableView = null;
			}
		}
	}
}
