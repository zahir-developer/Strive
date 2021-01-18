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
	[Register ("Schedule_SelectService")]
	partial class Schedule_SelectService
	{
		[Outlet]
		UIKit.UIButton Cancel_SelectServiceBtn { get; set; }

		[Outlet]
		UIKit.UITableView SelectService_TableView { get; set; }

		[Outlet]
		UIKit.UIView SelectService_View { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SelectService_View != null) {
				SelectService_View.Dispose ();
				SelectService_View = null;
			}

			if (SelectService_TableView != null) {
				SelectService_TableView.Dispose ();
				SelectService_TableView = null;
			}

			if (Cancel_SelectServiceBtn != null) {
				Cancel_SelectServiceBtn.Dispose ();
				Cancel_SelectServiceBtn = null;
			}
		}
	}
}
