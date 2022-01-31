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
	[Register ("LoginView")]
	partial class LoginView
	{
		[Outlet]
		UIKit.UIButton AgreeBtn { get; set; }

		[Outlet]
		UIKit.UIButton CheckBox { get; set; }

		[Outlet]
		UIKit.UIButton DisagreeBtn { get; set; }

		[Outlet]
		UIKit.UITextField EmailTextfield { get; set; }

		[Outlet]
		UIKit.UIButton ForgotPasswordBtn { get; set; }

		[Outlet]
		UIKit.UIButton LoginBtn { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextfield { get; set; }

		[Outlet]
		UIKit.UILabel SignUPLbl { get; set; }

		[Outlet]
		UIKit.UIView TermsAndCondtions { get; set; }

		[Action ("AgreeBtnClicked:")]
		partial void AgreeBtnClicked (UIKit.UIButton sender);

		[Action ("CheckBoxClicked:")]
		partial void CheckBoxClicked (UIKit.UIButton sender);

		[Action ("DisagreeBtnClicked:")]
		partial void DisagreeBtnClicked (UIKit.UIButton sender);

		[Action ("LoginBtnClicked:")]
		partial void LoginBtnClicked (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CheckBox != null) {
				CheckBox.Dispose ();
				CheckBox = null;
			}

			if (EmailTextfield != null) {
				EmailTextfield.Dispose ();
				EmailTextfield = null;
			}

			if (ForgotPasswordBtn != null) {
				ForgotPasswordBtn.Dispose ();
				ForgotPasswordBtn = null;
			}

			if (LoginBtn != null) {
				LoginBtn.Dispose ();
				LoginBtn = null;
			}

			if (PasswordTextfield != null) {
				PasswordTextfield.Dispose ();
				PasswordTextfield = null;
			}

			if (SignUPLbl != null) {
				SignUPLbl.Dispose ();
				SignUPLbl = null;
			}

			if (TermsAndCondtions != null) {
				TermsAndCondtions.Dispose ();
				TermsAndCondtions = null;
			}

			if (AgreeBtn != null) {
				AgreeBtn.Dispose ();
				AgreeBtn = null;
			}

			if (DisagreeBtn != null) {
				DisagreeBtn.Dispose ();
				DisagreeBtn = null;
			}
		}
	}
}
