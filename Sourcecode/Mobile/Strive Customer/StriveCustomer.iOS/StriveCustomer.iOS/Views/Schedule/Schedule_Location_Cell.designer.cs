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
	[Register ("Schedule_Location_Cell")]
	partial class Schedule_Location_Cell
	{
		[Outlet]
		UIKit.UIImageView scheduleLoc_Image { get; set; }

		[Outlet]
		UIKit.UILabel ScheduleLocation_Lbl { get; set; }

		[Action ("scheduleLoc_BtnTouched:")]
		partial void scheduleLoc_BtnTouched (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (scheduleLoc_Image != null) {
				scheduleLoc_Image.Dispose ();
				scheduleLoc_Image = null;
			}

			if (ScheduleLocation_Lbl != null) {
				ScheduleLocation_Lbl.Dispose ();
				ScheduleLocation_Lbl = null;
			}
		}
	}
}
