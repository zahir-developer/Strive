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
	[Register ("MembershipDetailView")]
	partial class MembershipDetailView
	{
		[Outlet]
		UIKit.UILabel ActivatedDate_Value { get; set; }

		[Outlet]
		UIKit.UILabel CancelledDate_Value { get; set; }

		[Outlet]
		UIKit.UIButton CancelMembership_Btn { get; set; }

		[Outlet]
		UIKit.UIView MembershipDetail_ParentView { get; set; }

		[Outlet]
		UIKit.UILabel MembershipNameLbl { get; set; }

		[Outlet]
		UIKit.UILabel MembershipStatus_Value { get; set; }

		[Action ("CancelMembership_BtnTouch:")]
		partial void CancelMembership_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (MembershipDetail_ParentView != null) {
				MembershipDetail_ParentView.Dispose ();
				MembershipDetail_ParentView = null;
			}

			if (MembershipNameLbl != null) {
				MembershipNameLbl.Dispose ();
				MembershipNameLbl = null;
			}

			if (ActivatedDate_Value != null) {
				ActivatedDate_Value.Dispose ();
				ActivatedDate_Value = null;
			}

			if (CancelledDate_Value != null) {
				CancelledDate_Value.Dispose ();
				CancelledDate_Value = null;
			}

			if (MembershipStatus_Value != null) {
				MembershipStatus_Value.Dispose ();
				MembershipStatus_Value = null;
			}

			if (CancelMembership_Btn != null) {
				CancelMembership_Btn.Dispose ();
				CancelMembership_Btn = null;
			}
		}
	}
}
