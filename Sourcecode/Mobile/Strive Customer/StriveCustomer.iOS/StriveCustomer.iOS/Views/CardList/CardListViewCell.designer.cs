// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveCustomer.iOS.Views.CardList
{
	[Register ("CardListViewCell")]
	partial class CardListViewCell
	{
		[Outlet]
		UIKit.UIView CardListView { get; set; }

		[Outlet]
		UIKit.UILabel CardNumber { get; set; }

		[Outlet]
		UIKit.UIButton deleteVehicleBtn { get; set; }

		[Outlet]
		UIKit.UIButton EditVehicleBtn { get; set; }

		[Outlet]
		UIKit.UILabel ExpiryDate { get; set; }

		[Action ("DeleteVehicleList_BtnTouch:")]
		partial void DeleteVehicleList_BtnTouch (UIKit.UIButton sender);

		[Action ("EditVehicleList_BtnTouch:")]
		partial void EditVehicleList_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CardListView != null) {
				CardListView.Dispose ();
				CardListView = null;
			}

			if (CardNumber != null) {
				CardNumber.Dispose ();
				CardNumber = null;
			}

			if (deleteVehicleBtn != null) {
				deleteVehicleBtn.Dispose ();
				deleteVehicleBtn = null;
			}

			if (EditVehicleBtn != null) {
				EditVehicleBtn.Dispose ();
				EditVehicleBtn = null;
			}

			if (ExpiryDate != null) {
				ExpiryDate.Dispose ();
				ExpiryDate = null;
			}
		}
	}
}
