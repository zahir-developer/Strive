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
	[Register ("LoginView")]
	partial class LoginView
	{
		[Outlet]
		UIKit.UIButton AgreeBtn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton CheckBox { get; set; }

		[Outlet]
		UIKit.UIButton DisagreeBtn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UITextField EmailTextfield { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton ForgotPasswordButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton LoginButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UITextField PasswordTextfield { get; set; }

		[Outlet]
		UIKit.UILabel SignupLbl { get; set; }

		[Outlet]
		UIKit.UIWebView TermsDocuments { get; set; }

		[Action ("AgreeBtnclicked:")]
		partial void AgreeBtnclicked (UIKit.UIButton sender);

		[Action ("CheckBoxClicked:")]
		partial void CheckBoxClicked (UIKit.UIButton sender);

		[Action ("DisagreeBtnclicked:")]
		partial void DisagreeBtnclicked (UIKit.UIButton sender);

		[Action ("LoginButtonClicked:")]
		partial void LoginButtonClicked (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AgreeBtn != null) {
				AgreeBtn.Dispose ();
				AgreeBtn = null;
			}

			if (DisagreeBtn != null) {
				DisagreeBtn.Dispose ();
				DisagreeBtn = null;
			}

			if (CheckBox != null) {
				CheckBox.Dispose ();
				CheckBox = null;
			}

			if (EmailTextfield != null) {
				EmailTextfield.Dispose ();
				EmailTextfield = null;
			}

			if (ForgotPasswordButton != null) {
				ForgotPasswordButton.Dispose ();
				ForgotPasswordButton = null;
			}

			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
			}

			if (PasswordTextfield != null) {
				PasswordTextfield.Dispose ();
				PasswordTextfield = null;
			}

			if (SignupLbl != null) {
				SignupLbl.Dispose ();
				SignupLbl = null;
			}

			if (TermsDocuments != null) {
				TermsDocuments.Dispose ();
				TermsDocuments = null;
			}
		}
	}
}
