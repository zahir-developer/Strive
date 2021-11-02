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
	[Register ("TermsView")]
	partial class TermsView
	{
		[Outlet]
		UIKit.UIView _TermsConfirmView { get; set; }

		[Outlet]
		UIKit.UILabel Date { get; set; }

		[Outlet]
		UIKit.UILabel membership_name { get; set; }

		[Outlet]
		UIKit.UILabel termsLabel { get; set; }

		[Outlet]
		UIKit.UIView TermsParentView { get; set; }

		[Action ("AgreeBtn_Touch:")]
		partial void AgreeBtn_Touch (UIKit.UIButton sender);

		[Action ("DisAgreeBtn_Touch:")]
		partial void DisAgreeBtn_Touch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Date != null) {
				Date.Dispose ();
				Date = null;
			}

			if (membership_name != null) {
				membership_name.Dispose ();
				membership_name = null;
			}

			if (termsLabel != null) {
				termsLabel.Dispose ();
				termsLabel = null;
			}

			if (TermsParentView != null) {
				TermsParentView.Dispose ();
				TermsParentView = null;
			}

			if (_TermsConfirmView != null) {
				_TermsConfirmView.Dispose ();
				_TermsConfirmView = null;
			}
		}
	}
}
