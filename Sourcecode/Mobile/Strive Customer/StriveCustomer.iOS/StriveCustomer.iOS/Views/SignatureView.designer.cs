// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveCustomer.iOS.Views
{
	[Register ("SignatureView")]
	partial class SignatureView
	{
		[Outlet]
		UIKit.UIButton CancelBtn_Sign { get; set; }

		[Outlet]
		UIKit.UIButton DoneBtn_Sign { get; set; }

		[Outlet]
		UIKit.UIView SignatureParentView { get; set; }

		[Outlet]
		Xamarin.Controls.SignaturePadView signPadView { get; set; }

		[Action ("CancelBtn_SignTouch:")]
		partial void CancelBtn_SignTouch (UIKit.UIButton sender);

		[Action ("DoneBtn_SignTouch:")]
		partial void DoneBtn_SignTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (signPadView != null) {
				signPadView.Dispose ();
				signPadView = null;
			}

			if (CancelBtn_Sign != null) {
				CancelBtn_Sign.Dispose ();
				CancelBtn_Sign = null;
			}

			if (DoneBtn_Sign != null) {
				DoneBtn_Sign.Dispose ();
				DoneBtn_Sign = null;
			}

			if (SignatureParentView != null) {
				SignatureParentView.Dispose ();
				SignatureParentView = null;
			}
		}
	}
}
