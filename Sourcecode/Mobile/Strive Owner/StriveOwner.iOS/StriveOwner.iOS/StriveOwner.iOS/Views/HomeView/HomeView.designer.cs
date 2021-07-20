// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveOwner.iOS.Views.HomeView
{
	[Register ("HomeView")]
	partial class HomeView
	{
		[Outlet]
		UIKit.UILabel carWashTimeCount { get; set; }

		[Outlet]
		UIKit.UIImageView carWashTimeImage { get; set; }

		[Outlet]
		UIKit.UILabel carWashtimelbl { get; set; }

		[Outlet]
		UIKit.UILabel current_ForecastedLbl { get; set; }

		[Outlet]
		UIKit.UIView DashboardInnerView { get; set; }

		[Outlet]
		UIKit.UIView DashboardParentView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl dashboardService_Seg { get; set; }

		[Outlet]
		UIKit.UILabel detailCount { get; set; }

		[Outlet]
		UIKit.UIImageView detailImage { get; set; }

		[Outlet]
		UIKit.UILabel DetailLbl { get; set; }

		[Outlet]
		UIKit.UILabel employeeCount { get; set; }

		[Outlet]
		UIKit.UILabel forecastedCount { get; set; }

		[Outlet]
		UIKit.UIImageView forecastedImage { get; set; }

		[Outlet]
		UIKit.UITableView ScheduleTableView { get; set; }

		[Outlet]
		UIKit.UILabel scoreCount { get; set; }

		[Outlet]
		UIKit.UIImageView scoreImage { get; set; }

		[Outlet]
		UIKit.UILabel scoreLbl { get; set; }

		[Outlet]
		UIKit.UILabel washCount { get; set; }

		[Outlet]
		UIKit.UIImageView washEmployeeImage { get; set; }

		[Outlet]
		UIKit.UILabel washEmployeeLbl { get; set; }

		[Outlet]
		UIKit.UIImageView washImage { get; set; }

		[Outlet]
		UIKit.UILabel WashLbl { get; set; }

		[Action ("DashboardServices_Touch:")]
		partial void DashboardServices_Touch (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (carWashTimeCount != null) {
				carWashTimeCount.Dispose ();
				carWashTimeCount = null;
			}

			if (carWashTimeImage != null) {
				carWashTimeImage.Dispose ();
				carWashTimeImage = null;
			}

			if (carWashtimelbl != null) {
				carWashtimelbl.Dispose ();
				carWashtimelbl = null;
			}

			if (current_ForecastedLbl != null) {
				current_ForecastedLbl.Dispose ();
				current_ForecastedLbl = null;
			}

			if (DashboardInnerView != null) {
				DashboardInnerView.Dispose ();
				DashboardInnerView = null;
			}

			if (DashboardParentView != null) {
				DashboardParentView.Dispose ();
				DashboardParentView = null;
			}

			if (dashboardService_Seg != null) {
				dashboardService_Seg.Dispose ();
				dashboardService_Seg = null;
			}

			if (detailCount != null) {
				detailCount.Dispose ();
				detailCount = null;
			}

			if (detailImage != null) {
				detailImage.Dispose ();
				detailImage = null;
			}

			if (DetailLbl != null) {
				DetailLbl.Dispose ();
				DetailLbl = null;
			}

			if (employeeCount != null) {
				employeeCount.Dispose ();
				employeeCount = null;
			}

			if (forecastedCount != null) {
				forecastedCount.Dispose ();
				forecastedCount = null;
			}

			if (forecastedImage != null) {
				forecastedImage.Dispose ();
				forecastedImage = null;
			}

			if (scoreCount != null) {
				scoreCount.Dispose ();
				scoreCount = null;
			}

			if (scoreImage != null) {
				scoreImage.Dispose ();
				scoreImage = null;
			}

			if (scoreLbl != null) {
				scoreLbl.Dispose ();
				scoreLbl = null;
			}

			if (washCount != null) {
				washCount.Dispose ();
				washCount = null;
			}

			if (washEmployeeImage != null) {
				washEmployeeImage.Dispose ();
				washEmployeeImage = null;
			}

			if (washEmployeeLbl != null) {
				washEmployeeLbl.Dispose ();
				washEmployeeLbl = null;
			}

			if (washImage != null) {
				washImage.Dispose ();
				washImage = null;
			}

			if (WashLbl != null) {
				WashLbl.Dispose ();
				WashLbl = null;
			}

			if (ScheduleTableView != null) {
				ScheduleTableView.Dispose ();
				ScheduleTableView = null;
			}
		}
	}
}
