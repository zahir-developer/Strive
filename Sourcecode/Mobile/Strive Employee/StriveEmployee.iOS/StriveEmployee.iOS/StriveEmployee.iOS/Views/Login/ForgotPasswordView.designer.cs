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
	[Register ("ForgotPasswordView")]
	partial class ForgotPasswordView
	{
		[Outlet]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		UIKit.UIButton GetOtpButton { get; set; }

		[Outlet]
		UIKit.UITextField MobileTextfield { get; set; }

		[Action ("BackBtn_Touch:")]
		partial void BackBtn_Touch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (GetOtpButton != null) {
				GetOtpButton.Dispose ();
				GetOtpButton = null;
			}

			if (MobileTextfield != null) {
				MobileTextfield.Dispose ();
				MobileTextfield = null;
			}
		}
	}
}
