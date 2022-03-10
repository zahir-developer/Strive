// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveCustomer.iOS.Views.Login
{
	[Register ("SignUpView")]
	partial class SignUpView
	{
		[Outlet]
		UIKit.UITextField ConfirmPasswordFld { get; set; }

		[Outlet]
		UIKit.UIButton CreateAccountButton { get; set; }

		[Outlet]
		UIKit.UIScrollView Credentials_Container { get; set; }

		[Outlet]
		UIKit.UITextField EmailIdFld { get; set; }

		[Outlet]
		UIKit.UITextField FirstNameFld { get; set; }

		[Outlet]
		UIKit.UITextField LastNameFld { get; set; }

		[Outlet]
		UIKit.UITextField PasswordFld { get; set; }

		[Outlet]
		UIKit.UITextField PhoneNumberFld { get; set; }

		[Outlet]
		UIKit.UIView SignUp_Credentials_View { get; set; }

		[Outlet]
		UIKit.UITextField VehicleColorFld { get; set; }

		[Outlet]
		UIKit.UITextField VehicleMakeFld { get; set; }

		[Outlet]
		UIKit.UITextField VehicleModelFld { get; set; }

		[Action ("CreateBtn_Touch:")]
		partial void CreateBtn_Touch (UIKit.UIButton sender);

		[Action ("VehicleMake_SpinnerTouchEnd:")]
		partial void VehicleMake_SpinnerTouchEnd (UIKit.UITextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ConfirmPasswordFld != null) {
				ConfirmPasswordFld.Dispose ();
				ConfirmPasswordFld = null;
			}

			if (CreateAccountButton != null) {
				CreateAccountButton.Dispose ();
				CreateAccountButton = null;
			}

			if (Credentials_Container != null) {
				Credentials_Container.Dispose ();
				Credentials_Container = null;
			}

			if (EmailIdFld != null) {
				EmailIdFld.Dispose ();
				EmailIdFld = null;
			}

			if (FirstNameFld != null) {
				FirstNameFld.Dispose ();
				FirstNameFld = null;
			}

			if (LastNameFld != null) {
				LastNameFld.Dispose ();
				LastNameFld = null;
			}

			if (PasswordFld != null) {
				PasswordFld.Dispose ();
				PasswordFld = null;
			}

			if (PhoneNumberFld != null) {
				PhoneNumberFld.Dispose ();
				PhoneNumberFld = null;
			}

			if (SignUp_Credentials_View != null) {
				SignUp_Credentials_View.Dispose ();
				SignUp_Credentials_View = null;
			}

			if (VehicleColorFld != null) {
				VehicleColorFld.Dispose ();
				VehicleColorFld = null;
			}

			if (VehicleMakeFld != null) {
				VehicleMakeFld.Dispose ();
				VehicleMakeFld = null;
			}

			if (VehicleModelFld != null) {
				VehicleModelFld.Dispose ();
				VehicleModelFld = null;
			}
		}
	}
}
