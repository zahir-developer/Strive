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
	[Register ("HomeView")]
	partial class HomeView
	{
		[Outlet]
		UIKit.UIButton AgreeBtn { get; set; }

		[Outlet]
		UIKit.UIView TermsDocuments { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MapKit.MKMapView WashTimeWebView { get; set; }

		[Action ("AgreeBtnclicked:")]
		partial void AgreeBtnclicked (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AgreeBtn != null) {
				AgreeBtn.Dispose ();
				AgreeBtn = null;
			}

			if (TermsDocuments != null) {
				TermsDocuments.Dispose ();
				TermsDocuments = null;
			}

			if (WashTimeWebView != null) {
				WashTimeWebView.Dispose ();
				WashTimeWebView = null;
			}
		}
	}
}
