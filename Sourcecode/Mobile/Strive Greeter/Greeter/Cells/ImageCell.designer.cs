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
	[Register ("ImageCell")]
	partial class ImageCell
	{
		[Outlet]
		UIKit.UIImageView imgv { get; set; }

		[Outlet]
		UIKit.UIImageView imgvClose { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgv != null) {
				imgv.Dispose ();
				imgv = null;
			}

			if (imgvClose != null) {
				imgvClose.Dispose ();
				imgvClose = null;
			}
		}
	}
}
