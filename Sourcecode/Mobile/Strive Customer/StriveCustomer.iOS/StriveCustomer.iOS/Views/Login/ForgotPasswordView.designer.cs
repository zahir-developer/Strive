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
	[Register ("ForgotPasswordView")]
	partial class ForgotPasswordView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton GetOtpButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
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
