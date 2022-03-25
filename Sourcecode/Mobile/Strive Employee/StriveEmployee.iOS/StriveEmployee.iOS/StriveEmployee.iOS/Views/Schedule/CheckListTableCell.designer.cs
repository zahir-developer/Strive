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
	[Register ("CheckListTableCell")]
	partial class CheckListTableCell
	{
		[Outlet]
		UIKit.UILabel Task { get; set; }

		[Outlet]
		UIKit.UIImageView TaskCheck { get; set; }

		[Outlet]
		UIKit.UIView TaskContainer { get; set; }

		[Outlet]
		UIKit.UILabel Time { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Task != null) {
				Task.Dispose ();
				Task = null;
			}

			if (TaskCheck != null) {
				TaskCheck.Dispose ();
				TaskCheck = null;
			}

			if (TaskContainer != null) {
				TaskContainer.Dispose ();
				TaskContainer = null;
			}

			if (Time != null) {
				Time.Dispose ();
				Time = null;
			}
		}
	}
}
