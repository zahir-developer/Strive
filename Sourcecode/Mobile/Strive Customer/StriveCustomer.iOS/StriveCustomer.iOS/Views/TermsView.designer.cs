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
	[Register ("TermsView")]
	partial class TermsView
	{
		[Outlet]
		UIKit.UIView _TermsConfirmView { get; set; }

		[Outlet]
		UIKit.UILabel AdditionalServicesTotal { get; set; }

		[Outlet]
		UIKit.UILabel Date { get; set; }

		[Outlet]
		UIKit.UILabel DisplaySelectedAddtionals { get; set; }

		[Outlet]
		UIKit.UILabel EndingDate { get; set; }

		[Outlet]
		UIKit.UILabel membership_name { get; set; }

		[Outlet]
		UIKit.UILabel MonthlyTotal { get; set; }

		[Outlet]
		UIKit.UILabel StartingDate { get; set; }

		[Outlet]
		UIKit.UILabel SwitchMembershipFee { get; set; }

		[Outlet]
		UIKit.UILabel termsLabel { get; set; }

		[Outlet]
		UIKit.UIView TermsParentView { get; set; }

		[Outlet]
		UIKit.UILabel total { get; set; }

		[Outlet]
		UIKit.UILabel Yearlytotal { get; set; }

		[Action ("AgreeBtn_Touch:")]
		partial void AgreeBtn_Touch (UIKit.UIButton sender);

		[Action ("DisAgreeBtn_Touch:")]
		partial void DisAgreeBtn_Touch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (_TermsConfirmView != null) {
				_TermsConfirmView.Dispose ();
				_TermsConfirmView = null;
			}

			if (AdditionalServicesTotal != null) {
				AdditionalServicesTotal.Dispose ();
				AdditionalServicesTotal = null;
			}

			if (Date != null) {
				Date.Dispose ();
				Date = null;
			}

			if (DisplaySelectedAddtionals != null) {
				DisplaySelectedAddtionals.Dispose ();
				DisplaySelectedAddtionals = null;
			}

			if (EndingDate != null) {
				EndingDate.Dispose ();
				EndingDate = null;
			}

			if (membership_name != null) {
				membership_name.Dispose ();
				membership_name = null;
			}

			if (MonthlyTotal != null) {
				MonthlyTotal.Dispose ();
				MonthlyTotal = null;
			}

			if (StartingDate != null) {
				StartingDate.Dispose ();
				StartingDate = null;
			}

			if (termsLabel != null) {
				termsLabel.Dispose ();
				termsLabel = null;
			}

			if (TermsParentView != null) {
				TermsParentView.Dispose ();
				TermsParentView = null;
			}

			if (total != null) {
				total.Dispose ();
				total = null;
			}

			if (SwitchMembershipFee != null) {
				SwitchMembershipFee.Dispose ();
				SwitchMembershipFee = null;
			}

			if (Yearlytotal != null) {
				Yearlytotal.Dispose ();
				Yearlytotal = null;
			}
		}
	}
}
