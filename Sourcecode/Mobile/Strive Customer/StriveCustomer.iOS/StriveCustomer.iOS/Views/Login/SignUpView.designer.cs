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
		UIKit.UIScrollView Credentials_Container { get; set; }

		[Outlet]
		UIKit.UIView SignUp_Credentials_View { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Credentials_Container != null) {
				Credentials_Container.Dispose ();
				Credentials_Container = null;
			}

			if (SignUp_Credentials_View != null) {
				SignUp_Credentials_View.Dispose ();
				SignUp_Credentials_View = null;
			}
		}
	}
}
