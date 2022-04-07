// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveOwner.iOS.Views.Inventory
{
	[Register ("IconViewCell")]
	partial class IconViewCell
	{
		[Outlet]
		UIKit.UIImageView IconImage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (IconImage != null) {
				IconImage.Dispose ();
				IconImage = null;
			}
		}
	}
}
