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
	[Register ("EmailPopupViewController")]
	partial class EmailPopupViewController
	{
		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnSend { get; set; }

		[Outlet]
		UIKit.UITextField tfEmail { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnSend != null) {
				btnSend.Dispose ();
				btnSend = null;
			}

			if (tfEmail != null) {
				tfEmail.Dispose ();
				tfEmail = null;
			}
		}
	}
}
