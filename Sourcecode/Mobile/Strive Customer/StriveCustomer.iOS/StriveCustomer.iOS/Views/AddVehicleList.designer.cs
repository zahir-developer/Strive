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
	[Register ("AddVehicleList")]
	partial class AddVehicleList
	{
		[Outlet]
		UIKit.UIButton SaveAddVehicle_Btn { get; set; }

		[Outlet]
		UIKit.UIButton SelectMembership_Text { get; set; }

		[Outlet]
		UIKit.UIImageView VehicleColor_Arrow { get; set; }

		[Outlet]
		UIKit.UITextField VehicleColor_TextField { get; set; }

		[Outlet]
		UIKit.UIImageView VehicleMake_Arrow { get; set; }

		[Outlet]
		UIKit.UITextField VehicleMake_TextField { get; set; }

		[Outlet]
		UIKit.UITextField VehicleModel_TextField { get; set; }

		[Action ("Make_TouchInside:")]
		partial void Make_TouchInside (UIKit.UITextField sender);

		[Action ("Make_TouchOutside:")]
		partial void Make_TouchOutside (UIKit.UITextField sender);

		[Action ("Model_TouchInside:")]
		partial void Model_TouchInside (UIKit.UITextField sender);

		[Action ("SaveAddVehicle_BtnTouch:")]
		partial void SaveAddVehicle_BtnTouch (UIKit.UIButton sender);

		[Action ("SelectMembership_Touch:")]
		partial void SelectMembership_Touch (UIKit.UIButton sender);

		[Action ("VehicleColor_SpinnerTouchBegin:")]
		partial void VehicleColor_SpinnerTouchBegin (UIKit.UITextField sender);

		[Action ("VehicleColor_SpinnerTouchEnd:")]
		partial void VehicleColor_SpinnerTouchEnd (UIKit.UITextField sender);

		[Action ("VehicleMake_SpinnerTouchBegin:")]
		partial void VehicleMake_SpinnerTouchBegin (UIKit.UITextField sender);

		[Action ("VehicleMake_SpinnerTouchEnd:")]
		partial void VehicleMake_SpinnerTouchEnd (UIKit.UITextField sender);

		[Action ("VehicleModel_SpinnerTouchBegin:")]
		partial void VehicleModel_SpinnerTouchBegin (UIKit.UITextField sender);

		[Action ("VehicleModel_SpinnerTouchEnd:")]
		partial void VehicleModel_SpinnerTouchEnd (UIKit.UITextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (VehicleColor_Arrow != null) {
				VehicleColor_Arrow.Dispose ();
				VehicleColor_Arrow = null;
			}

			if (VehicleColor_TextField != null) {
				VehicleColor_TextField.Dispose ();
				VehicleColor_TextField = null;
			}

			if (VehicleMake_Arrow != null) {
				VehicleMake_Arrow.Dispose ();
				VehicleMake_Arrow = null;
			}

			if (VehicleMake_TextField != null) {
				VehicleMake_TextField.Dispose ();
				VehicleMake_TextField = null;
			}

			if (VehicleModel_TextField != null) {
				VehicleModel_TextField.Dispose ();
				VehicleModel_TextField = null;
			}

			if (SelectMembership_Text != null) {
				SelectMembership_Text.Dispose ();
				SelectMembership_Text = null;
			}

			if (SaveAddVehicle_Btn != null) {
				SaveAddVehicle_Btn.Dispose ();
				SaveAddVehicle_Btn = null;
			}
		}
	}
}
