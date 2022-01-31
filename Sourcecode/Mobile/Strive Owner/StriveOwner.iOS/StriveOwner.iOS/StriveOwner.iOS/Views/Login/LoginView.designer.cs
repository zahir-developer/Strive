// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveOwner.iOS.Views.Login
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
		UIKit.UIButton ForgotBtn { get; set; }

		[Outlet]
		UIKit.UIButton LoginBtn { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextfield { get; set; }

		[Outlet]
		UIKit.UILabel SignUpLbl { get; set; }

		[Outlet]
		UIKit.UIView TermsAndContions { get; set; }

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

			if (ForgotBtn != null) {
				ForgotBtn.Dispose ();
				ForgotBtn = null;
			}

			if (LoginBtn != null) {
				LoginBtn.Dispose ();
				LoginBtn = null;
			}

			if (AgreeBtn != null) {
				AgreeBtn.Dispose ();
				AgreeBtn = null;
			}

			if (DisagreeBtn != null) {
				DisagreeBtn.Dispose ();
				DisagreeBtn = null;
			}

			if (PasswordTextfield != null) {
				PasswordTextfield.Dispose ();
				PasswordTextfield = null;
			}

			if (SignUpLbl != null) {
				SignUpLbl.Dispose ();
				SignUpLbl = null;
			}

			if (TermsAndContions != null) {
				TermsAndContions.Dispose ();
				TermsAndContions = null;
			}
		}
	}
}
