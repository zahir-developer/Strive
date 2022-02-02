// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveTimInventory.iOS.Views.CardList
{
	[Register ("ModifyCardView")]
	partial class ModifyCardView
	{
		[Outlet]
		UIKit.UIView CardDetialsView { get; set; }

		[Outlet]
		UIKit.UITextField CardNumberentry { get; set; }

		[Outlet]
		UIKit.UITextField ExpiryDateentry { get; set; }

		[Outlet]
		UIKit.UIButton SaveCardButton { get; set; }

		[Outlet]
		UIKit.UILabel Title { get; set; }

		[Action ("SaveCard_BtnTouch:")]
		partial void SaveCard_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CardDetialsView != null) {
				CardDetialsView.Dispose ();
				CardDetialsView = null;
			}

			if (CardNumberentry != null) {
				CardNumberentry.Dispose ();
				CardNumberentry = null;
			}

			if (ExpiryDateentry != null) {
				ExpiryDateentry.Dispose ();
				ExpiryDateentry = null;
			}

			if (SaveCardButton != null) {
				SaveCardButton.Dispose ();
				SaveCardButton = null;
			}

			if (Title != null) {
				Title.Dispose ();
				Title = null;
			}
		}
	}
}
