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
	[Register ("MembershipVehicle_ViewCell")]
	partial class MembershipVehicle_ViewCell
	{
		[Outlet]
		UIKit.UIImageView Membership_CellBtn { get; set; }

		[Outlet]
		UIKit.UIView Membership_CellView { get; set; }

		[Outlet]
		UIKit.UILabel Membership_Discount { get; set; }

		[Outlet]
		UIKit.UILabel Membership_VehicleLbl { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint MembershipCell_ViewHeight { get; set; }

		[Outlet]
		UIKit.UILabel MonthlyCharge_lbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Membership_CellBtn != null) {
				Membership_CellBtn.Dispose ();
				Membership_CellBtn = null;
			}

			if (Membership_CellView != null) {
				Membership_CellView.Dispose ();
				Membership_CellView = null;
			}

			if (Membership_Discount != null) {
				Membership_Discount.Dispose ();
				Membership_Discount = null;
			}

			if (Membership_VehicleLbl != null) {
				Membership_VehicleLbl.Dispose ();
				Membership_VehicleLbl = null;
			}

			if (MembershipCell_ViewHeight != null) {
				MembershipCell_ViewHeight.Dispose ();
				MembershipCell_ViewHeight = null;
			}

			if (MonthlyCharge_lbl != null) {
				MonthlyCharge_lbl.Dispose ();
				MonthlyCharge_lbl = null;
			}
		}
	}
}
