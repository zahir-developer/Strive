// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveEmployee.iOS.Views
{
	[Register ("DocumentsCell")]
	partial class DocumentsCell
	{
		[Outlet]
		UIKit.UILabel DocumentDate { get; set; }

		[Outlet]
		UIKit.UILabel DocumentName { get; set; }

		[Action ("DocView_BtnTouch:")]
		partial void DocView_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (DocumentName != null) {
				DocumentName.Dispose ();
				DocumentName = null;
			}

			if (DocumentDate != null) {
				DocumentDate.Dispose ();
				DocumentDate = null;
			}
		}
	}
}
