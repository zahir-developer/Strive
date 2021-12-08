// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveEmployee.iOS.Views.Messenger
{
	[Register ("CreateGroupView")]
	partial class CreateGroupView
	{
		[Outlet]
		UIKit.UIView CreateGroup_ParentView { get; set; }

		[Outlet]
		UIKit.UITableView CreateGroup_TableView { get; set; }

		[Outlet]
		UIKit.UISearchBar SearchContactBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CreateGroup_ParentView != null) {
				CreateGroup_ParentView.Dispose ();
				CreateGroup_ParentView = null;
			}

			if (CreateGroup_TableView != null) {
				CreateGroup_TableView.Dispose ();
				CreateGroup_TableView = null;
			}

			if (SearchContactBar != null) {
				SearchContactBar.Dispose ();
				SearchContactBar = null;
			}
		}
	}
}
