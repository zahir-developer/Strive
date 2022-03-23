// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Greeter.Storyboards
{
	[Register ("DisplayVehicleEmail")]
	partial class DisplayVehicleEmail
	{
		[Outlet]
		UIKit.UIButton BtnCancel { get; set; }

		[Outlet]
		UIKit.UIButton BtnCreate { get; set; }

		[Outlet]
		UIKit.UITextField TftEmail { get; set; }

		[Outlet]
		UIKit.UIPickerView VehiclePicker { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnCancel != null) {
				BtnCancel.Dispose ();
				BtnCancel = null;
			}

			if (BtnCreate != null) {
				BtnCreate.Dispose ();
				BtnCreate = null;
			}

			if (TftEmail != null) {
				TftEmail.Dispose ();
				TftEmail = null;
			}

			if (VehiclePicker != null) {
				VehiclePicker.Dispose ();
				VehiclePicker = null;
			}
		}
	}
}
