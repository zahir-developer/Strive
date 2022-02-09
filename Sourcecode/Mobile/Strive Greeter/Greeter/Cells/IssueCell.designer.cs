// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Greeter.Cells
{
	[Register ("IssueCell")]
	partial class IssueCell
	{
		[Outlet]
		UIKit.UIButton btnClose { get; set; }

		[Outlet]
		UIKit.UICollectionView cvImages { get; set; }

		[Outlet]
		UIKit.UILabel lblDate { get; set; }

		[Outlet]
		UIKit.UILabel lblDesc { get; set; }

		[Action ("CloseBtnClicked:")]
		partial void CloseBtnClicked (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnClose != null) {
				btnClose.Dispose ();
				btnClose = null;
			}

			if (cvImages != null) {
				cvImages.Dispose ();
				cvImages = null;
			}

			if (lblDate != null) {
				lblDate.Dispose ();
				lblDate = null;
			}

			if (lblDesc != null) {
				lblDesc.Dispose ();
				lblDesc = null;
			}
		}
	}
}
