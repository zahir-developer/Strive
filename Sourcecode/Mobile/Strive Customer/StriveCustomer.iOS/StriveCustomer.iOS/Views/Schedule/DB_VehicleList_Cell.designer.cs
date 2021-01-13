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
	[Register ("DB_VehicleList_Cell")]
	partial class DB_VehicleList_Cell
	{
		[Outlet]
		UIKit.UILabel Schedule_VhCarName { get; set; }

		[Outlet]
		UIKit.UILabel Schedule_VhMembership { get; set; }

		[Outlet]
		UIKit.UIButton ScheduleNow_Btn { get; set; }

		[Action ("ScheduleNow_BtnTouch:")]
		partial void ScheduleNow_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Schedule_VhCarName != null) {
				Schedule_VhCarName.Dispose ();
				Schedule_VhCarName = null;
			}

			if (Schedule_VhMembership != null) {
				Schedule_VhMembership.Dispose ();
				Schedule_VhMembership = null;
			}

			if (ScheduleNow_Btn != null) {
				ScheduleNow_Btn.Dispose ();
				ScheduleNow_Btn = null;
			}
		}
	}
}
