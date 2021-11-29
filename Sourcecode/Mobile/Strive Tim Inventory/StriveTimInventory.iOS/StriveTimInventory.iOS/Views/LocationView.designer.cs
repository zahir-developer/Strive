// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveTimInventory.iOS.Views
{
	[Register ("LocationView")]
	partial class LocationView
	{
		[Outlet]
		UIKit.UITextField locationTextField { get; set; }

		[Outlet]
		UIKit.UIButton locProceedBtn { get; set; }

		[Action ("locProceedTouch:")]
		partial void locProceedTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (locationTextField != null) {
				locationTextField.Dispose ();
				locationTextField = null;
			}

			if (locProceedBtn != null) {
				locProceedBtn.Dispose ();
				locProceedBtn = null;
			}
		}
	}
}
