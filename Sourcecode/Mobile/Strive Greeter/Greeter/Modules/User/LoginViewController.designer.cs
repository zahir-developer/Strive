// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Greeter
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		UIKit.UIButton btnEye { get; set; }

		[Outlet]
		UIKit.UIButton btnLogin { get; set; }

		[Outlet]
		UIKit.UITextField tfPswd { get; set; }

		[Outlet]
		UIKit.UITextField tfUserId { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}

			if (tfPswd != null) {
				tfPswd.Dispose ();
				tfPswd = null;
			}

			if (tfUserId != null) {
				tfUserId.Dispose ();
				tfUserId = null;
			}

			if (btnEye != null) {
				btnEye.Dispose ();
				btnEye = null;
			}
		}
	}
}
