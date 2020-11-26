// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveCustomer.iOS.Views
{
	[Register ("ProfileView")]
	partial class ProfileView
	{
		[Outlet]
		UIKit.UILabel Address_Value { get; set; }

		[Outlet]
		UIKit.UILabel ContactNo_Value { get; set; }

		[Outlet]
		UIKit.UILabel Email_Value { get; set; }

		[Outlet]
		UIKit.UILabel FullName_Value { get; set; }

		[Outlet]
		UIKit.UIView PastDetail_Segment { get; set; }

		[Outlet]
		UIKit.UITableView PastDetailTableView { get; set; }

		[Outlet]
		UIKit.UIButton PersonalEditBtn_View { get; set; }

		[Outlet]
		UIKit.UIView PersonalInfo_Segment { get; set; }

		[Outlet]
		UIKit.UILabel PhoneNo_Value { get; set; }

		[Outlet]
		UIKit.UIView ProfileParent_View { get; set; }

		[Outlet]
		UIKit.UISegmentedControl SegmentControl { get; set; }

		[Outlet]
		UIKit.UILabel ZipCode_Value { get; set; }

		[Action ("EditProfile_Touch:")]
		partial void EditProfile_Touch (UIKit.UIButton sender);
		
		[Action ("ProfileSegment_SelectedTab:")]
		partial void ProfileSegment_SelectedTab (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PastDetail_Segment != null) {
				PastDetail_Segment.Dispose ();
				PastDetail_Segment = null;
			}

			if (PastDetailTableView != null) {
				PastDetailTableView.Dispose ();
				PastDetailTableView = null;
			}

			if (PersonalInfo_Segment != null) {
				PersonalInfo_Segment.Dispose ();
				PersonalInfo_Segment = null;
			}

			if (ProfileParent_View != null) {
				ProfileParent_View.Dispose ();
				ProfileParent_View = null;
			}

			if (SegmentControl != null) {
				SegmentControl.Dispose ();
				SegmentControl = null;
			}

			if (PersonalEditBtn_View != null) {
				PersonalEditBtn_View.Dispose ();
				PersonalEditBtn_View = null;
			}

			if (FullName_Value != null) {
				FullName_Value.Dispose ();
				FullName_Value = null;
			}

			if (ContactNo_Value != null) {
				ContactNo_Value.Dispose ();
				ContactNo_Value = null;
			}

			if (Address_Value != null) {
				Address_Value.Dispose ();
				Address_Value = null;
			}

			if (ZipCode_Value != null) {
				ZipCode_Value.Dispose ();
				ZipCode_Value = null;
			}

			if (PhoneNo_Value != null) {
				PhoneNo_Value.Dispose ();
				PhoneNo_Value = null;
			}

			if (Email_Value != null) {
				Email_Value.Dispose ();
				Email_Value = null;
			}
		}
	}
}
