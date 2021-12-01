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
	[Register ("ContactUsView")]
	partial class ContactUsView
	{
		[Outlet]
		UIKit.UIView ContactUsChildView { get; set; }

		[Outlet]
		MapKit.MKMapView ContactUsMap { get; set; }

		[Outlet]
		UIKit.UIView ContactUsParentView { get; set; }

		[Outlet]
		UIKit.UILabel LocationNameLbl { get; set; }

		[Outlet]
		UIKit.UILabel locationValue_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel mailValue_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel phoneValue_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel timeValue_Lbl { get; set; }

		[Action ("FacebookRedirect:")]
		partial void FacebookRedirect (UIKit.UIButton sender);

		[Action ("InstagramRedirect:")]
		partial void InstagramRedirect (UIKit.UIButton sender);

		[Action ("TwitterRedirect:")]
		partial void TwitterRedirect (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ContactUsChildView != null) {
				ContactUsChildView.Dispose ();
				ContactUsChildView = null;
			}

			if (ContactUsMap != null) {
				ContactUsMap.Dispose ();
				ContactUsMap = null;
			}

			if (ContactUsParentView != null) {
				ContactUsParentView.Dispose ();
				ContactUsParentView = null;
			}

			if (LocationNameLbl != null) {
				LocationNameLbl.Dispose ();
				LocationNameLbl = null;
			}

			if (locationValue_Lbl != null) {
				locationValue_Lbl.Dispose ();
				locationValue_Lbl = null;
			}

			if (mailValue_Lbl != null) {
				mailValue_Lbl.Dispose ();
				mailValue_Lbl = null;
			}

			if (phoneValue_Lbl != null) {
				phoneValue_Lbl.Dispose ();
				phoneValue_Lbl = null;
			}

			if (timeValue_Lbl != null) {
				timeValue_Lbl.Dispose ();
				timeValue_Lbl = null;
			}
		}
	}
}
