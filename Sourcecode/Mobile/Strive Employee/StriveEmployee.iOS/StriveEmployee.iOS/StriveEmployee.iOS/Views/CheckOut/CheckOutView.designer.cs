// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveEmployee.iOS.Views
{
	[Register ("CheckOutView")]
	partial class CheckOutView
	{
		[Outlet]
		UIKit.UITableView CheckOut_TableView { get; set; }

		[Outlet]
		UIKit.UIView CheckOut_View { get; set; }

		[Outlet]
		UIKit.UITextField locationTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CheckOut_TableView != null) {
				CheckOut_TableView.Dispose ();
				CheckOut_TableView = null;
			}

			if (CheckOut_View != null) {
				CheckOut_View.Dispose ();
				CheckOut_View = null;
			}

			if (locationTextField != null) {
				locationTextField.Dispose ();
				locationTextField = null;
			}
		}
	}
}
