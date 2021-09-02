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
	[Register ("SampleReadMoreViewController")]
	partial class SampleReadMoreViewController
	{
		[Outlet]
		UIKit.UIButton btnMore { get; set; }

		[Outlet]
		UIKit.UILabel lblText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblText != null) {
				lblText.Dispose ();
				lblText = null;
			}

			if (btnMore != null) {
				btnMore.Dispose ();
				btnMore = null;
			}
		}
	}
}
