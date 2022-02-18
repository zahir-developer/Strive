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
		UIKit.UIButton _FinishBtn { get; set; }

		[Outlet]
		UIKit.UITableView Checklist_TableView { get; set; }

		[Outlet]
		UIKit.UIView CheckListView { get; set; }

		[Outlet]
		UIKit.UIDatePicker DetailDateView { get; set; }

		[Outlet]
		UIKit.UITableView detailer_TableView { get; set; }

		[Outlet]
		UIKit.UIView DetailerView { get; set; }

		[Outlet]
		UIKit.UITableView empSchedule_TableView { get; set; }

		[Outlet]
		UIKit.UIView ParentView { get; set; }

		[Outlet]
		UIKit.UIDatePicker ScheduleDateView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl Scheduledetailer_Seg_Ctrl { get; set; }

		[Outlet]
		UIKit.UIView ScheduleParentView { get; set; }

		[Action ("DetailDate_Touch:")]
		partial void DetailDate_Touch (UIKit.UIDatePicker sender);

		[Action ("Schedule_Segment_Touch:")]
		partial void Schedule_Segment_Touch (UIKit.UISegmentedControl sender);

		[Action ("scheduleDate_Touch:")]
		partial void scheduleDate_Touch (UIKit.UIDatePicker sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Checklist_TableView != null) {
				Checklist_TableView.Dispose ();
				Checklist_TableView = null;
			}

			if (_FinishBtn != null) {
				_FinishBtn.Dispose ();
				_FinishBtn = null;
			}

			if (CheckListView != null) {
				CheckListView.Dispose ();
				CheckListView = null;
			}

			if (DetailDateView != null) {
				DetailDateView.Dispose ();
				DetailDateView = null;
			}

			if (detailer_TableView != null) {
				detailer_TableView.Dispose ();
				detailer_TableView = null;
			}

			if (DetailerView != null) {
				DetailerView.Dispose ();
				DetailerView = null;
			}

			if (empSchedule_TableView != null) {
				empSchedule_TableView.Dispose ();
				empSchedule_TableView = null;
			}

			if (ParentView != null) {
				ParentView.Dispose ();
				ParentView = null;
			}

			if (ScheduleDateView != null) {
				ScheduleDateView.Dispose ();
				ScheduleDateView = null;
			}

			if (Scheduledetailer_Seg_Ctrl != null) {
				Scheduledetailer_Seg_Ctrl.Dispose ();
				Scheduledetailer_Seg_Ctrl = null;
			}

			if (ScheduleParentView != null) {
				ScheduleParentView.Dispose ();
				ScheduleParentView = null;
			}
		}
	}
}
