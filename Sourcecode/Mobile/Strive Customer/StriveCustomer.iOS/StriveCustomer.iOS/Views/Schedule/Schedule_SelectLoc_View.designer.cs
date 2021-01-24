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
	[Register ("Schedule_SelectLoc_View")]
	partial class Schedule_SelectLoc_View
	{
		[Outlet]
		UIKit.UIButton SelectLoc_CancelBtn { get; set; }

		[Outlet]
		UIKit.UITableView SelectLoc_TableView { get; set; }

		[Outlet]
		UIKit.UIView SelectLocation_View { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SelectLocation_View != null) {
				SelectLocation_View.Dispose ();
				SelectLocation_View = null;
			}

			if (SelectLoc_TableView != null) {
				SelectLoc_TableView.Dispose ();
				SelectLoc_TableView = null;
			}

			if (SelectLoc_CancelBtn != null) {
				SelectLoc_CancelBtn.Dispose ();
				SelectLoc_CancelBtn = null;
			}
		}
	}
}
