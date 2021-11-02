
// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveCustomer.iOS.Views.Schedule
{
	[Register ("Schedule_SelectService_Cell")]
	partial class Schedule_SelectService_Cell
	{
		[Outlet]
		UIKit.NSLayoutConstraint MoreValue_Const { get; set; }

		[Outlet]
		UIKit.UIButton SelectService_Btn { get; set; }

		[Outlet]
		UIKit.UIView SelectService_CellView { get; set; }

		[Outlet]
		UIKit.UILabel SelectService_CostLbl { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint serviceCell_ViewHeight { get; set; }

		[Outlet]
		UIKit.UILabel ServiceName_Lbl { get; set; }

		[Outlet]
		UIKit.UIButton ViewMore_Btn { get; set; }

		[Outlet]
		UIKit.UILabel ViewMore_ValueLbl { get; set; }

		[Action ("SelectService_BtnTouch:")]
		partial void SelectService_BtnTouch (UIKit.UIButton sender);

		[Action ("ViewMore_BtnTouch:")]
		partial void ViewMore_BtnTouch (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (MoreValue_Const != null) {
				MoreValue_Const.Dispose ();
				MoreValue_Const = null;
			}

			if (SelectService_Btn != null) {
				SelectService_Btn.Dispose ();
				SelectService_Btn = null;
			}

			if (SelectService_CellView != null) {
				SelectService_CellView.Dispose ();
				SelectService_CellView = null;
			}

			if (SelectService_CostLbl != null) {
				SelectService_CostLbl.Dispose ();
				SelectService_CostLbl = null;
			}

			if (serviceCell_ViewHeight != null) {
				serviceCell_ViewHeight.Dispose ();
				serviceCell_ViewHeight = null;
			}

			if (ServiceName_Lbl != null) {
				ServiceName_Lbl.Dispose ();
				ServiceName_Lbl = null;
			}

			if (ViewMore_Btn != null) {
				ViewMore_Btn.Dispose ();
				ViewMore_Btn = null;
			}

			if (ViewMore_ValueLbl != null) {
				ViewMore_ValueLbl.Dispose ();
				ViewMore_ValueLbl = null;
			}
		}
	}
}
