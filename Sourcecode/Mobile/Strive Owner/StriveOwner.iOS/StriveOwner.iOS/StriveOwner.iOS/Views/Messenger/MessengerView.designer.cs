// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveOwner.iOS.Views.Messenger
{
	[Register ("MessengerView")]
	partial class MessengerView
	{
		[Outlet]
		UIKit.UIView Messenger_HomeView { get; set; }

		[Outlet]
		UIKit.UISearchBar Messenger_SearchBar { get; set; }

		[Outlet]
		UIKit.UISegmentedControl Messenger_SegCtrl { get; set; }

		[Outlet]
		UIKit.UITableView Messenger_TableView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint SearchBar_HeightConst { get; set; }

		[Action ("MenuBtnTouch:")]
		partial void MenuBtnTouch (UIKit.UIButton sender);

		[Action ("Messenger_SegmentTouch:")]
		partial void Messenger_SegmentTouch (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Messenger_HomeView != null) {
				Messenger_HomeView.Dispose ();
				Messenger_HomeView = null;
			}

			if (Messenger_SearchBar != null) {
				Messenger_SearchBar.Dispose ();
				Messenger_SearchBar = null;
			}

			if (Messenger_SegCtrl != null) {
				Messenger_SegCtrl.Dispose ();
				Messenger_SegCtrl = null;
			}

			if (Messenger_TableView != null) {
				Messenger_TableView.Dispose ();
				Messenger_TableView = null;
			}

			if (SearchBar_HeightConst != null) {
				SearchBar_HeightConst.Dispose ();
				SearchBar_HeightConst = null;
			}
		}
	}
}
