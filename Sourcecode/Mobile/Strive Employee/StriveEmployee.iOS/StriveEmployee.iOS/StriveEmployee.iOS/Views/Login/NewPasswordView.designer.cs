// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveEmployee.iOS.Views.Login
{
	[Register ("NewPasswordView")]
	partial class NewPasswordView
	{
		[Outlet]
		UIKit.UITextField ConfirmPasswordTextField { get; set; }

		[Outlet]
		UIKit.UITextField NewPasswordTextField { get; set; }

		[Outlet]
		UIKit.UIButton submitPassword { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NewPasswordTextField != null) {
				NewPasswordTextField.Dispose ();
				NewPasswordTextField = null;
			}

			if (ConfirmPasswordTextField != null) {
				ConfirmPasswordTextField.Dispose ();
				ConfirmPasswordTextField = null;
			}

			if (submitPassword != null) {
				submitPassword.Dispose ();
				submitPassword = null;
			}
		}
	}
}
