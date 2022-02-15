// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveTimInventory.iOS.Views
{
	[Register ("InventoryView")]
	partial class InventoryView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton AddProductButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UITableView InventoryListTableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UISearchBar InventorySearch { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton LogoutButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel Title { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddProductButton != null) {
				AddProductButton.Dispose ();
				AddProductButton = null;
			}

			if (InventoryListTableView != null) {
				InventoryListTableView.Dispose ();
				InventoryListTableView = null;
			}

			if (InventorySearch != null) {
				InventorySearch.Dispose ();
				InventorySearch = null;
			}

			if (LogoutButton != null) {
				LogoutButton.Dispose ();
				LogoutButton = null;
			}

			if (Title != null) {
				Title.Dispose ();
				Title = null;
			}
		}
	}
}
