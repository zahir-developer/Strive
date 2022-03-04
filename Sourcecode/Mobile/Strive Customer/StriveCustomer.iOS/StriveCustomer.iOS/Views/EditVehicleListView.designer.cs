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
	[Register ("EditVehicleListView")]
	partial class EditVehicleListView
	{
		[Outlet]
		UIKit.UIButton Cancel_Membership { get; set; }

		[Outlet]
		UIKit.UITableView CardDetails_TableView { get; set; }

		[Outlet]
		UIKit.UILabel EditBarCode_Value { get; set; }

		[Outlet]
		UIKit.UIView EditVehicle_ParentView { get; set; }

		[Outlet]
		UIKit.UILabel EditVehicleColor_Value { get; set; }

		[Outlet]
		UIKit.UILabel EditVehicleMake_Value { get; set; }

		[Outlet]
		UIKit.UILabel EditVehicleMembership_Value { get; set; }

		[Outlet]
		UIKit.UILabel EditVehicleModel_Value { get; set; }

		[Outlet]
		UIKit.UILabel EditVehicleName { get; set; }

		[Outlet]
		UIKit.UILabel NoData { get; set; }

		[Action ("EditVehicleList_BtnTouch:")]
		partial void EditVehicleList_BtnTouch (UIKit.UIButton sender);

		[Action ("Touch_Cancel_Membership:")]
		partial void Touch_Cancel_Membership (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Cancel_Membership != null) {
				Cancel_Membership.Dispose ();
				Cancel_Membership = null;
			}

			if (CardDetails_TableView != null) {
				CardDetails_TableView.Dispose ();
				CardDetails_TableView = null;
			}

			if (EditBarCode_Value != null) {
				EditBarCode_Value.Dispose ();
				EditBarCode_Value = null;
			}

			if (EditVehicle_ParentView != null) {
				EditVehicle_ParentView.Dispose ();
				EditVehicle_ParentView = null;
			}

			if (EditVehicleColor_Value != null) {
				EditVehicleColor_Value.Dispose ();
				EditVehicleColor_Value = null;
			}

			if (EditVehicleMake_Value != null) {
				EditVehicleMake_Value.Dispose ();
				EditVehicleMake_Value = null;
			}

			if (EditVehicleMembership_Value != null) {
				EditVehicleMembership_Value.Dispose ();
				EditVehicleMembership_Value = null;
			}

			if (EditVehicleModel_Value != null) {
				EditVehicleModel_Value.Dispose ();
				EditVehicleModel_Value = null;
			}

			if (EditVehicleName != null) {
				EditVehicleName.Dispose ();
				EditVehicleName = null;
			}

			if (NoData != null) {
				NoData.Dispose ();
				NoData = null;
			}
		}
	}
}
