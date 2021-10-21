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
	[Register ("Schedule_SelectDate_View")]
	partial class Schedule_SelectDate_View
	{
		[Outlet]
		UIKit.UIButton Cancel_DateSchedule { get; set; }

		[Outlet]
		UIKit.UICollectionView Date_CollectionView { get; set; }

		[Outlet]
		UIKit.UIView Schedule_Date_ChildView { get; set; }

		[Outlet]
		UIKit.UIDatePicker Schedule_datePicker { get; set; }

		[Outlet]
		UIKit.UIView SelectDate_ParentView { get; set; }

		[Outlet]
		UIKit.UILabel TimeSlot_Label { get; set; }

		[Action ("CancelDate_BtnTouch:")]
		partial void CancelDate_BtnTouch (UIKit.UIButton sender);

		[Action ("dateChange:")]
		partial void dateChange (UIKit.UIDatePicker sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Cancel_DateSchedule != null) {
				Cancel_DateSchedule.Dispose ();
				Cancel_DateSchedule = null;
			}

			if (Date_CollectionView != null) {
				Date_CollectionView.Dispose ();
				Date_CollectionView = null;
			}

			if (Schedule_Date_ChildView != null) {
				Schedule_Date_ChildView.Dispose ();
				Schedule_Date_ChildView = null;
			}

			if (Schedule_datePicker != null) {
				Schedule_datePicker.Dispose ();
				Schedule_datePicker = null;
			}

			if (SelectDate_ParentView != null) {
				SelectDate_ParentView.Dispose ();
				SelectDate_ParentView = null;
			}

			if (TimeSlot_Label != null) {
				TimeSlot_Label.Dispose ();
				TimeSlot_Label = null;
			}
		}
	}
}
