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
		UIKit.UIView TermsParentView { get; set; }

		[Action ("AgreeBtn_Touch:")]
		partial void AgreeBtn_Touch (UIKit.UIButton sender);

		[Action ("DisAgreeBtn_Touch:")]
		partial void DisAgreeBtn_Touch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (TermsParentView != null) {
				TermsParentView.Dispose ();
				TermsParentView = null;
			}
		}
	}
}
