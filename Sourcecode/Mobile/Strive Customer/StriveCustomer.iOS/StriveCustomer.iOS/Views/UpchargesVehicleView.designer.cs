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
	[Register ("UpchargesVehicleView")]
	partial class UpchargesVehicleView
	{
		[Outlet]
		UIKit.UITableView UpchargesVehicle_TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (UpchargesVehicle_TableView != null) {
				UpchargesVehicle_TableView.Dispose ();
				UpchargesVehicle_TableView = null;
			}
		}
	}
}
