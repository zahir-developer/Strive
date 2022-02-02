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
	[Register ("VehicleMembershipDetailView")]
	partial class VehicleMembershipDetailView
	{
		[Outlet]
		UIKit.UILabel _NoRelatableData { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel ActivatedDate { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel CancelledDate { get; set; }

		[Outlet]
		UIKit.UITableView CardDetailsTable { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton ChangeButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel MembershipName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel Status { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivatedDate != null) {
				ActivatedDate.Dispose ();
				ActivatedDate = null;
			}

			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (CancelledDate != null) {
				CancelledDate.Dispose ();
				CancelledDate = null;
			}

			if (ChangeButton != null) {
				ChangeButton.Dispose ();
				ChangeButton = null;
			}

			if (MembershipName != null) {
				MembershipName.Dispose ();
				MembershipName = null;
			}

			if (Status != null) {
				Status.Dispose ();
				Status = null;
			}

			if (CardDetailsTable != null) {
				CardDetailsTable.Dispose ();
				CardDetailsTable = null;
			}

			if (_NoRelatableData != null) {
				_NoRelatableData.Dispose ();
				_NoRelatableData = null;
			}
		}
	}
}
