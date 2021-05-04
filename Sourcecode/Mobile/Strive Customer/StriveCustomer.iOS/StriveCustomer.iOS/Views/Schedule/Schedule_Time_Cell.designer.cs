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
	[Register ("Schedule_Time_Cell")]
	partial class Schedule_Time_Cell
	{
		[Outlet]
		UIKit.UIView Time_CellView { get; set; }

		[Outlet]
		UIKit.UIButton TimeSlot_Btn { get; set; }

		[Action ("TimeSlot_BtnTouch:")]
		partial void TimeSlot_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Time_CellView != null) {
				Time_CellView.Dispose ();
				Time_CellView = null;
			}

			if (TimeSlot_Btn != null) {
				TimeSlot_Btn.Dispose ();
				TimeSlot_Btn = null;
			}
		}
	}
}
