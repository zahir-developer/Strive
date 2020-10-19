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
	[Register ("PastDetailViewCell")]
	partial class PastDetailViewCell
	{
		[Outlet]
		UIKit.UIView PastDetailCellView { get; set; }

		[Outlet]
		UIKit.UILabel PastDetailLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PastDetailCellView != null) {
				PastDetailCellView.Dispose ();
				PastDetailCellView = null;
			}

			if (PastDetailLabel != null) {
				PastDetailLabel.Dispose ();
				PastDetailLabel = null;
			}
		}
	}
}
