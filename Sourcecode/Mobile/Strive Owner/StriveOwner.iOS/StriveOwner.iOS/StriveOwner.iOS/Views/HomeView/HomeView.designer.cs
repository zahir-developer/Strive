// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveOwner.iOS.Views.HomeView
{
	[Register ("HomeView")]
	partial class HomeView
	{
		[Outlet]
		UIKit.UIView DashboardInnerView { get; set; }

		[Outlet]
		UIKit.UIView DashboardParentView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DashboardParentView != null) {
				DashboardParentView.Dispose ();
				DashboardParentView = null;
			}

			if (DashboardInnerView != null) {
				DashboardInnerView.Dispose ();
				DashboardInnerView = null;
			}
		}
	}
}
