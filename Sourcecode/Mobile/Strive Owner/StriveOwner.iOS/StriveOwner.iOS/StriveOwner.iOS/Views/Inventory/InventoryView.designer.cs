// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveOwner.iOS.Views.Inventory
{
	[Register ("InventoryView")]
	partial class InventoryView
	{
		[Outlet]
		UIKit.UIButton AddProd_Btn { get; set; }

		[Outlet]
		UIKit.UITableView Inventory_TableView { get; set; }

		[Outlet]
		UIKit.UISearchBar InventorySearch { get; set; }

		[Outlet]
		UIKit.UILabel InventoryTitle { get; set; }

		[Outlet]
		UIKit.UIButton LogoutButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddProd_Btn != null) {
				AddProd_Btn.Dispose ();
				AddProd_Btn = null;
			}

			if (Inventory_TableView != null) {
				Inventory_TableView.Dispose ();
				Inventory_TableView = null;
			}

			if (InventorySearch != null) {
				InventorySearch.Dispose ();
				InventorySearch = null;
			}

			if (LogoutButton != null) {
				LogoutButton.Dispose ();
				LogoutButton = null;
			}

			if (InventoryTitle != null) {
				InventoryTitle.Dispose ();
				InventoryTitle = null;
			}
		}
	}
}
