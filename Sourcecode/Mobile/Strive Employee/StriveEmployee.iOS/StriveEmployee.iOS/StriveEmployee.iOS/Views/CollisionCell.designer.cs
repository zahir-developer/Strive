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
	[Register ("CollisionCell")]
	partial class CollisionCell
	{
		[Outlet]
		UIKit.UILabel Collision_CellAmount { get; set; }

		[Outlet]
		UIKit.UILabel Collision_CellDate { get; set; }

		[Outlet]
		UIKit.UIImageView Collision_CellImage { get; set; }

		[Outlet]
		UIKit.UILabel Collision_CellText { get; set; }

		[Outlet]
		UIKit.UIView Collision_CellView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Collision_CellAmount != null) {
				Collision_CellAmount.Dispose ();
				Collision_CellAmount = null;
			}

			if (Collision_CellDate != null) {
				Collision_CellDate.Dispose ();
				Collision_CellDate = null;
			}

			if (Collision_CellImage != null) {
				Collision_CellImage.Dispose ();
				Collision_CellImage = null;
			}

			if (Collision_CellText != null) {
				Collision_CellText.Dispose ();
				Collision_CellText = null;
			}

			if (Collision_CellView != null) {
				Collision_CellView.Dispose ();
				Collision_CellView = null;
			}
		}
	}
}
