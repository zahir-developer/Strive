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
	[Register ("ClockOutView")]
	partial class ClockOutView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel ClockInTimeLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIView ClockInView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel ClockOutTimeLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIView ClockOutViewBox { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton LogoutButton { get; set; }


		
		[GeneratedCode("iOS Designer", "1.0")]
		[Outlet]
		UIKit.UIButton Return { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel RoleLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel TotalHoursLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel WelcomeBackLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ClockInTimeLabel != null) {
				ClockInTimeLabel.Dispose ();
				ClockInTimeLabel = null;
			}

			if (ClockInView != null) {
				ClockInView.Dispose ();
				ClockInView = null;
			}

			if (ClockOutTimeLabel != null) {
				ClockOutTimeLabel.Dispose ();
				ClockOutTimeLabel = null;
			}

			if (ClockOutViewBox != null) {
				ClockOutViewBox.Dispose ();
				ClockOutViewBox = null;
			}

			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (LogoutButton != null) {
				LogoutButton.Dispose ();
				LogoutButton = null;
			}

			if (RoleLabel != null) {
				RoleLabel.Dispose ();
				RoleLabel = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (TotalHoursLabel != null) {
				TotalHoursLabel.Dispose ();
				TotalHoursLabel = null;
			}

			if (WelcomeBackLabel != null) {
				WelcomeBackLabel.Dispose ();
				WelcomeBackLabel = null;
			}

			if (Return != null) {
				Return.Dispose ();
				Return = null;
			}
		}
	}
}
