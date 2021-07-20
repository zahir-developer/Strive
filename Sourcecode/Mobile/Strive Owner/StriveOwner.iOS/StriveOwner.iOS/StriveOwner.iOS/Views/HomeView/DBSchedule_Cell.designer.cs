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
	[Register ("DBSchedule_Cell")]
	partial class DBSchedule_Cell
	{
		[Outlet]
		UIKit.UIButton BayButton { get; set; }

		[Outlet]
		UIKit.UILabel BayNameLbl { get; set; }

		[Outlet]
		UIKit.UIView BayView { get; set; }

		[Outlet]
		UIKit.UILabel ClientValue { get; set; }

		[Outlet]
		UIKit.UILabel MakeValue { get; set; }

		[Outlet]
		UIKit.UILabel PhoneValue { get; set; }

		[Outlet]
		UIKit.UILabel ServiceValue { get; set; }

		[Outlet]
		UIKit.UILabel TicketNo_Lbl { get; set; }

		[Outlet]
		UIKit.UIView TicketView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ticketView_HeightConst { get; set; }

		[Outlet]
		UIKit.UILabel TimeInValue { get; set; }

		[Outlet]
		UIKit.UILabel TimeOutValue { get; set; }

		[Outlet]
		UIKit.UILabel UpchargeValue { get; set; }

		[Action ("BayBtn_Touch:")]
		partial void BayBtn_Touch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BayView != null) {
				BayView.Dispose ();
				BayView = null;
			}

			if (TicketView != null) {
				TicketView.Dispose ();
				TicketView = null;
			}

			if (ticketView_HeightConst != null) {
				ticketView_HeightConst.Dispose ();
				ticketView_HeightConst = null;
			}

			if (BayButton != null) {
				BayButton.Dispose ();
				BayButton = null;
			}

			if (BayNameLbl != null) {
				BayNameLbl.Dispose ();
				BayNameLbl = null;
			}

			if (TicketNo_Lbl != null) {
				TicketNo_Lbl.Dispose ();
				TicketNo_Lbl = null;
			}

			if (TimeInValue != null) {
				TimeInValue.Dispose ();
				TimeInValue = null;
			}

			if (ClientValue != null) {
				ClientValue.Dispose ();
				ClientValue = null;
			}

			if (PhoneValue != null) {
				PhoneValue.Dispose ();
				PhoneValue = null;
			}

			if (TimeOutValue != null) {
				TimeOutValue.Dispose ();
				TimeOutValue = null;
			}

			if (UpchargeValue != null) {
				UpchargeValue.Dispose ();
				UpchargeValue = null;
			}

			if (MakeValue != null) {
				MakeValue.Dispose ();
				MakeValue = null;
			}

			if (ServiceValue != null) {
				ServiceValue.Dispose ();
				ServiceValue = null;
			}
		}
	}
}
