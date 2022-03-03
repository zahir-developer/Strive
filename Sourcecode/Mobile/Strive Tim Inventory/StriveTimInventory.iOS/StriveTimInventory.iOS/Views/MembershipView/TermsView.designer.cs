// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveTimInventory.iOS.Views.MembershipView
{
	[Register ("TermsView")]
	partial class TermsView
	{
		[Outlet]
		UIKit.UILabel AdditionalServicesTotal { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton AgreeButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		UIKit.UIView ContractView { get; set; }

		[Outlet]
		UIKit.UILabel CustomerName { get; set; }

		[Outlet]
		UIKit.UILabel Date { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton DisagreeButton { get; set; }

		[Outlet]
		UIKit.UITextView DisplaySelectedAdditionals { get; set; }

		[Outlet]
		UIKit.UILabel EndingDate { get; set; }

		[Outlet]
		UIKit.UILabel Membership_name { get; set; }

		[Outlet]
		UIKit.UILabel MonthlyTotal { get; set; }

		[Outlet]
		UIKit.UILabel StartingDate { get; set; }

		[Outlet]
		UIKit.UILabel SwitchMembershipFee { get; set; }

		[Outlet]
		UIKit.UILabel Total { get; set; }

		[Outlet]
		UIKit.UILabel Upchargeslbl { get; set; }

		[Outlet]
		UIKit.UILabel Vehicle { get; set; }

		[Outlet]
		UIKit.UILabel YearlyTotal { get; set; }

		[Action ("AgreeButtonTouch:")]
		partial void AgreeButtonTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AdditionalServicesTotal != null) {
				AdditionalServicesTotal.Dispose ();
				AdditionalServicesTotal = null;
			}

			if (AgreeButton != null) {
				AgreeButton.Dispose ();
				AgreeButton = null;
			}

			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (ContractView != null) {
				ContractView.Dispose ();
				ContractView = null;
			}

			if (Date != null) {
				Date.Dispose ();
				Date = null;
			}

			if (DisagreeButton != null) {
				DisagreeButton.Dispose ();
				DisagreeButton = null;
			}

			if (DisplaySelectedAdditionals != null) {
				DisplaySelectedAdditionals.Dispose ();
				DisplaySelectedAdditionals = null;
			}

			if (EndingDate != null) {
				EndingDate.Dispose ();
				EndingDate = null;
			}

			if (CustomerName != null) {
				CustomerName.Dispose ();
				CustomerName = null;
			}

			if (Membership_name != null) {
				Membership_name.Dispose ();
				Membership_name = null;
			}

			if (MonthlyTotal != null) {
				MonthlyTotal.Dispose ();
				MonthlyTotal = null;
			}

			if (StartingDate != null) {
				StartingDate.Dispose ();
				StartingDate = null;
			}

			if (SwitchMembershipFee != null) {
				SwitchMembershipFee.Dispose ();
				SwitchMembershipFee = null;
			}

			if (Total != null) {
				Total.Dispose ();
				Total = null;
			}

			if (Upchargeslbl != null) {
				Upchargeslbl.Dispose ();
				Upchargeslbl = null;
			}

			if (Vehicle != null) {
				Vehicle.Dispose ();
				Vehicle = null;
			}

			if (YearlyTotal != null) {
				YearlyTotal.Dispose ();
				YearlyTotal = null;
			}
		}
	}
}
