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
	[Register ("PastDetailTabView")]
	partial class PastDetailTabView
	{
		[Outlet]
		UIKit.UILabel AdditionalServiceValue { get; set; }

		[Outlet]
		UIKit.UILabel BarcodeValue { get; set; }

		[Outlet]
		UIKit.UILabel CarNameValue { get; set; }

		[Outlet]
		UIKit.UILabel ColorValue { get; set; }

		[Outlet]
		UIKit.UILabel MakeValue { get; set; }

		[Outlet]
		UIKit.UILabel ModelValue { get; set; }

		[Outlet]
		UIKit.UILabel PackageNameValue { get; set; }

		[Outlet]
		UIKit.UIView PastDetailTab_ParentView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl PastDetailTab_SegmentCtrl { get; set; }

		[Outlet]
		UIKit.UILabel PriceValue { get; set; }

		[Outlet]
		UIKit.UILabel VisitDateValue { get; set; }

		[Action ("DateSegment_PD:")]
		partial void DateSegment_PD (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AdditionalServiceValue != null) {
				AdditionalServiceValue.Dispose ();
				AdditionalServiceValue = null;
			}

			if (BarcodeValue != null) {
				BarcodeValue.Dispose ();
				BarcodeValue = null;
			}

			if (CarNameValue != null) {
				CarNameValue.Dispose ();
				CarNameValue = null;
			}

			if (ColorValue != null) {
				ColorValue.Dispose ();
				ColorValue = null;
			}

			if (MakeValue != null) {
				MakeValue.Dispose ();
				MakeValue = null;
			}

			if (ModelValue != null) {
				ModelValue.Dispose ();
				ModelValue = null;
			}

			if (PackageNameValue != null) {
				PackageNameValue.Dispose ();
				PackageNameValue = null;
			}

			if (PastDetailTab_ParentView != null) {
				PastDetailTab_ParentView.Dispose ();
				PastDetailTab_ParentView = null;
			}

			if (PastDetailTab_SegmentCtrl != null) {
				PastDetailTab_SegmentCtrl.Dispose ();
				PastDetailTab_SegmentCtrl = null;
			}

			if (PriceValue != null) {
				PriceValue.Dispose ();
				PriceValue = null;
			}

			if (VisitDateValue != null) {
				VisitDateValue.Dispose ();
				VisitDateValue = null;
			}
		}
	}
}
