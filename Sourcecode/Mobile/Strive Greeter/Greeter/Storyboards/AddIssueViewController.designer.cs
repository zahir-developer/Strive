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
	[Register ("AddIssueViewController")]
	partial class AddIssueViewController
	{
		[Outlet]
		UIKit.UIButton btnCancel { get; set; }

		[Outlet]
		UIKit.UIButton btnSave { get; set; }

		[Outlet]
		UIKit.UICollectionView cvImages { get; set; }

		[Outlet]
		UIKit.UILabel lblAddPhotos { get; set; }

		[Outlet]
		UIKit.UILabel lblIssueDetailHint { get; set; }

		[Outlet]
		UIKit.UITextField tfIssueDetail { get; set; }

		[Outlet]
		UIKit.UITextView tvIssueDetail { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCancel != null) {
				btnCancel.Dispose ();
				btnCancel = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (cvImages != null) {
				cvImages.Dispose ();
				cvImages = null;
			}

			if (lblAddPhotos != null) {
				lblAddPhotos.Dispose ();
				lblAddPhotos = null;
			}

			if (tfIssueDetail != null) {
				tfIssueDetail.Dispose ();
				tfIssueDetail = null;
			}

			if (tvIssueDetail != null) {
				tvIssueDetail.Dispose ();
				tvIssueDetail = null;
			}

			if (lblIssueDetailHint != null) {
				lblIssueDetailHint.Dispose ();
				lblIssueDetailHint = null;
			}
		}
	}
}
