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
	[Register ("empSchedule_Cell")]
	partial class empSchedule_Cell
	{
		[Outlet]
		UIKit.UIView EmpSchedule_CellView { get; set; }

		[Outlet]
		UIKit.UILabel EmpScheduleLbl { get; set; }

		[Outlet]
		UIKit.UILabel EmpScheduleTimeLbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (EmpSchedule_CellView != null) {
				EmpSchedule_CellView.Dispose ();
				EmpSchedule_CellView = null;
			}

			if (EmpScheduleLbl != null) {
				EmpScheduleLbl.Dispose ();
				EmpScheduleLbl = null;
			}

			if (EmpScheduleTimeLbl != null) {
				EmpScheduleTimeLbl.Dispose ();
				EmpScheduleTimeLbl = null;
			}
		}
	}
}
