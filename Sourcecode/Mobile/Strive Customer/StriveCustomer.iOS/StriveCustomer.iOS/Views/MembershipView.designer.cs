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
	[Register ("MembershipView")]
	partial class MembershipView
	{
		[Outlet]
		UIKit.UITableView VehicleMembership_TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (VehicleMembership_TableView != null) {
				VehicleMembership_TableView.Dispose ();
				VehicleMembership_TableView = null;
			}
		}
	}
}
