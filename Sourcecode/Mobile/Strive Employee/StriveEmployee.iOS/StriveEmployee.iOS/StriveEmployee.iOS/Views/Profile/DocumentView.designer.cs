// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveEmployee.iOS.Views.Profile
{
	[Register ("DocumentView")]
	partial class DocumentView
	{
		[Outlet]
		UIKit.UIView DocumentWebView { get; set; }

		[Outlet]
		UIKit.UIImageView JpegDocumentView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DocumentWebView != null) {
				DocumentWebView.Dispose ();
				DocumentWebView = null;
			}

			if (JpegDocumentView != null) {
				JpegDocumentView.Dispose ();
				JpegDocumentView = null;
			}
		}
	}
}
