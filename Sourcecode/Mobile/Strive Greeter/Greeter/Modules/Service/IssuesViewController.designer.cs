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
	[Register ("IssuesViewController")]
	partial class IssuesViewController
	{
		[Outlet]
		UIKit.UIButton btnAddIssue { get; set; }

		[Outlet]
		UIKit.UIButton CloseButton { get; set; }

		[Outlet]
		UIKit.UIView ImageContainer { get; set; }

		[Outlet]
		UIKit.UIImageView IssueImageView { get; set; }

		[Outlet]
		UIKit.UILabel lblBarcode { get; set; }

		[Outlet]
		UIKit.UILabel lblDate { get; set; }

		[Outlet]
		UIKit.UILabel lblMake { get; set; }

		[Outlet]
		UIKit.UILabel lblModel { get; set; }

		[Outlet]
		UIKit.UITableView tvIssues { get; set; }

		[Outlet]
		UIKit.UIView viewHeader { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddIssue != null) {
				btnAddIssue.Dispose ();
				btnAddIssue = null;
			}

			if (lblBarcode != null) {
				lblBarcode.Dispose ();
				lblBarcode = null;
			}

			if (lblDate != null) {
				lblDate.Dispose ();
				lblDate = null;
			}

			if (lblMake != null) {
				lblMake.Dispose ();
				lblMake = null;
			}

			if (lblModel != null) {
				lblModel.Dispose ();
				lblModel = null;
			}

			if (tvIssues != null) {
				tvIssues.Dispose ();
				tvIssues = null;
			}

			if (viewHeader != null) {
				viewHeader.Dispose ();
				viewHeader = null;
			}

			if (ImageContainer != null) {
				ImageContainer.Dispose ();
				ImageContainer = null;
			}

			if (IssueImageView != null) {
				IssueImageView.Dispose ();
				IssueImageView = null;
			}

			if (CloseButton != null) {
				CloseButton.Dispose ();
				CloseButton = null;
			}
		}
	}
}
