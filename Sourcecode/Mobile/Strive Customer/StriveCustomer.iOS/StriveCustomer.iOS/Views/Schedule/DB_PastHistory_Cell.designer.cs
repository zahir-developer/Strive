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
	[Register ("DB_PastHistory_Cell")]
	partial class DB_PastHistory_Cell
	{
		[Outlet]
		UIKit.UIView PastHis_ShortView { get; set; }

		[Outlet]
		UIKit.UILabel PH_Cost_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel PH_Date_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel PH_TicNo_Lbl { get; set; }

		[Outlet]
		UIKit.UILabel PH_VehicleName_Lbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PH_TicNo_Lbl != null) {
				PH_TicNo_Lbl.Dispose ();
				PH_TicNo_Lbl = null;
			}

			if (PH_Date_Lbl != null) {
				PH_Date_Lbl.Dispose ();
				PH_Date_Lbl = null;
			}

			if (PH_VehicleName_Lbl != null) {
				PH_VehicleName_Lbl.Dispose ();
				PH_VehicleName_Lbl = null;
			}

			if (PH_Cost_Lbl != null) {
				PH_Cost_Lbl.Dispose ();
				PH_Cost_Lbl = null;
			}

			if (PastHis_ShortView != null) {
				PastHis_ShortView.Dispose ();
				PastHis_ShortView = null;
			}
		}
	}
}
