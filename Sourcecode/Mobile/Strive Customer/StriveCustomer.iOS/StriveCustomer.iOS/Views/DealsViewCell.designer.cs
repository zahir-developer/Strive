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
	[Register ("DealsViewCell")]
	partial class DealsViewCell
	{
		[Outlet]
		UIKit.UIView BackgroundView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel ValidityLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (ValidityLabel != null) {
				ValidityLabel.Dispose ();
				ValidityLabel = null;
			}

			if (BackgroundView != null) {
				BackgroundView.Dispose ();
				BackgroundView = null;
			}
		}
	}
}
