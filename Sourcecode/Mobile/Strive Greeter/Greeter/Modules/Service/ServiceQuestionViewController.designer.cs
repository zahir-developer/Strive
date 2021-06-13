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
	[Register ("ServiceQuestionViewController")]
	partial class ServiceQuestionViewController
	{
		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnNext { get; set; }

		[Outlet]
		UIKit.UILabel lblDate { get; set; }

		[Outlet]
		UIKit.UILabel lblTime { get; set; }

		[Outlet]
		UIKit.UITextField tfAdditionalService { get; set; }

		[Outlet]
		UIKit.UITextField tfAirFreshner { get; set; }

		[Outlet]
		UIKit.UITextField tfBarcode { get; set; }

		[Outlet]
		UIKit.UITextField tfColor { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint tfdetailHeight { get; set; }

		[Outlet]
		UIKit.UITextField tfDetailPkg { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint tfdetailTop { get; set; }

		[Outlet]
		UIKit.UITextField tfMake { get; set; }

		[Outlet]
		UIKit.UITextField tfType { get; set; }

		[Outlet]
		UIKit.UITextField tfUpcharge { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint tfwashHeight { get; set; }

		[Outlet]
		UIKit.UITextField tfWashPkg { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint tfwashTop { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnNext != null) {
				btnNext.Dispose ();
				btnNext = null;
			}

			if (lblDate != null) {
				lblDate.Dispose ();
				lblDate = null;
			}

			if (lblTime != null) {
				lblTime.Dispose ();
				lblTime = null;
			}

			if (tfAdditionalService != null) {
				tfAdditionalService.Dispose ();
				tfAdditionalService = null;
			}

			if (tfAirFreshner != null) {
				tfAirFreshner.Dispose ();
				tfAirFreshner = null;
			}

			if (tfBarcode != null) {
				tfBarcode.Dispose ();
				tfBarcode = null;
			}

			if (tfColor != null) {
				tfColor.Dispose ();
				tfColor = null;
			}

			if (tfDetailPkg != null) {
				tfDetailPkg.Dispose ();
				tfDetailPkg = null;
			}

			if (tfMake != null) {
				tfMake.Dispose ();
				tfMake = null;
			}

			if (tfType != null) {
				tfType.Dispose ();
				tfType = null;
			}

			if (tfUpcharge != null) {
				tfUpcharge.Dispose ();
				tfUpcharge = null;
			}

			if (tfWashPkg != null) {
				tfWashPkg.Dispose ();
				tfWashPkg = null;
			}

			if (tfwashHeight != null) {
				tfwashHeight.Dispose ();
				tfwashHeight = null;
			}

			if (tfdetailHeight != null) {
				tfdetailHeight.Dispose ();
				tfdetailHeight = null;
			}

			if (tfdetailTop != null) {
				tfdetailTop.Dispose ();
				tfdetailTop = null;
			}

			if (tfwashTop != null) {
				tfwashTop.Dispose ();
				tfwashTop = null;
			}
		}
	}
}
