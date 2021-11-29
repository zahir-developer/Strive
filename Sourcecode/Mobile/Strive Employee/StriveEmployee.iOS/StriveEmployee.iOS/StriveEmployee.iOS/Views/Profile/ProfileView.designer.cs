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
	[Register ("ProfileView")]
	partial class ProfileView
	{
		[Outlet]
		UIKit.UITableView Collision_TableView { get; set; }

		[Outlet]
		UIKit.UITableView Documents_TableView { get; set; }

		[Outlet]
		UIKit.UILabel Emp_Address { get; set; }

		[Outlet]
		UIKit.UILabel Emp_ContactNo { get; set; }

		[Outlet]
		UIKit.UILabel Emp_Firstname { get; set; }

		[Outlet]
		UIKit.UILabel Emp_Gender { get; set; }

		[Outlet]
		UIKit.UILabel Emp_HireDate { get; set; }

		[Outlet]
		UIKit.UILabel Emp_Imm_Status { get; set; }

		[Outlet]
		UIKit.UILabel Emp_Lastname { get; set; }

		[Outlet]
		UIKit.UILabel Emp_LoginId { get; set; }

		[Outlet]
		UIKit.UILabel Emp_SSN { get; set; }

		[Outlet]
		UIKit.UILabel Emp_Status { get; set; }

		[Outlet]
		UIKit.UISegmentedControl EmpAccount_Seg_Ctrl { get; set; }

		[Outlet]
		UIKit.UIView EmployeeCollision_View { get; set; }

		[Outlet]
		UIKit.UIView EmployeeDocument_View { get; set; }

		[Outlet]
		UIKit.UIView EmployeeInfo_Segment { get; set; }

		[Outlet]
		UIKit.UIView EmployeeProfile_View { get; set; }

		[Action ("Emp_Segment_Touch:")]
		partial void Emp_Segment_Touch (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Collision_TableView != null) {
				Collision_TableView.Dispose ();
				Collision_TableView = null;
			}

			if (Emp_Address != null) {
				Emp_Address.Dispose ();
				Emp_Address = null;
			}

			if (Emp_ContactNo != null) {
				Emp_ContactNo.Dispose ();
				Emp_ContactNo = null;
			}

			if (Emp_Firstname != null) {
				Emp_Firstname.Dispose ();
				Emp_Firstname = null;
			}

			if (Emp_Gender != null) {
				Emp_Gender.Dispose ();
				Emp_Gender = null;
			}

			if (Emp_HireDate != null) {
				Emp_HireDate.Dispose ();
				Emp_HireDate = null;
			}

			if (Emp_Imm_Status != null) {
				Emp_Imm_Status.Dispose ();
				Emp_Imm_Status = null;
			}

			if (Emp_Lastname != null) {
				Emp_Lastname.Dispose ();
				Emp_Lastname = null;
			}

			if (Emp_LoginId != null) {
				Emp_LoginId.Dispose ();
				Emp_LoginId = null;
			}

			if (Emp_SSN != null) {
				Emp_SSN.Dispose ();
				Emp_SSN = null;
			}

			if (Emp_Status != null) {
				Emp_Status.Dispose ();
				Emp_Status = null;
			}

			if (EmpAccount_Seg_Ctrl != null) {
				EmpAccount_Seg_Ctrl.Dispose ();
				EmpAccount_Seg_Ctrl = null;
			}

			if (EmployeeCollision_View != null) {
				EmployeeCollision_View.Dispose ();
				EmployeeCollision_View = null;
			}

			if (EmployeeDocument_View != null) {
				EmployeeDocument_View.Dispose ();
				EmployeeDocument_View = null;
			}

			if (EmployeeInfo_Segment != null) {
				EmployeeInfo_Segment.Dispose ();
				EmployeeInfo_Segment = null;
			}

			if (EmployeeProfile_View != null) {
				EmployeeProfile_View.Dispose ();
				EmployeeProfile_View = null;
			}

			if (Documents_TableView != null) {
				Documents_TableView.Dispose ();
				Documents_TableView = null;
			}
		}
	}
}
