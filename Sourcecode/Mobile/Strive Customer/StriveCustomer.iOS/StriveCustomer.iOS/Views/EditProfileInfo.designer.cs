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
	[Register ("EditProfileInfo")]
	partial class EditProfileInfo
	{
		[Outlet]
		UIKit.UITextField Address_EditField { get; set; }

		[Outlet]
		UIKit.UITextField ContactNo_EditField { get; set; }

		[Outlet]
		UIKit.UILabel EditAddress_Hint { get; set; }

		[Outlet]
		UIKit.UILabel EditContactNo_Hint { get; set; }

		[Outlet]
		UIKit.UILabel EditEmail_Hint { get; set; }

		[Outlet]
		UIKit.UILabel EditFullName_Hint { get; set; }

		[Outlet]
		UIKit.UIScrollView EditProfile_scrollView { get; set; }

		[Outlet]
		UIKit.UIView EditProfile_View { get; set; }

		[Outlet]
		UIKit.UILabel EditSecPh_Hint { get; set; }

		[Outlet]
		UIKit.UILabel EditZipCode_Hint { get; set; }

		[Outlet]
		UIKit.UITextField Email_EditField { get; set; }

		[Outlet]
		UIKit.UITextField FullName_EditField { get; set; }

		[Outlet]
		UIKit.UIButton SaveEditProfile_Btn { get; set; }

		[Outlet]
		UIKit.UITextField SecPhone_EditField { get; set; }

		[Outlet]
		UIKit.UITextField ZipCode_EditField { get; set; }

		[Action ("Address_TouchBegin:")]
		partial void Address_TouchBegin (UIKit.UITextField sender);

		[Action ("Address_TouchEnd:")]
		partial void Address_TouchEnd (UIKit.UITextField sender);

		[Action ("ContactNo_TouchBegin:")]
		partial void ContactNo_TouchBegin (UIKit.UITextField sender);

		[Action ("ContactNo_TouchEnd:")]
		partial void ContactNo_TouchEnd (UIKit.UITextField sender);

		[Action ("EditEmail_TouchBegin:")]
		partial void EditEmail_TouchBegin (UIKit.UITextField sender);

		[Action ("EditEmail_TouchEnd:")]
		partial void EditEmail_TouchEnd (UIKit.UITextField sender);

		[Action ("FullName_TouchBegin:")]
		partial void FullName_TouchBegin (UIKit.UITextField sender);

		[Action ("FullName_TouchEnd:")]
		partial void FullName_TouchEnd (UIKit.UITextField sender);

		[Action ("SaveProfile_BtnTouch:")]
		partial void SaveProfile_BtnTouch (UIKit.UIButton sender);

		[Action ("SecPhNo_TouchBegin:")]
		partial void SecPhNo_TouchBegin (UIKit.UITextField sender);

		[Action ("SecPhNo_TouchEnd:")]
		partial void SecPhNo_TouchEnd (UIKit.UITextField sender);

		[Action ("ZipCode_TouchBegin:")]
		partial void ZipCode_TouchBegin (UIKit.UITextField sender);

		[Action ("ZipCode_TouchEnd:")]
		partial void ZipCode_TouchEnd (UIKit.UITextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (EditProfile_View != null) {
				EditProfile_View.Dispose ();
				EditProfile_View = null;
			}

			if (EditFullName_Hint != null) {
				EditFullName_Hint.Dispose ();
				EditFullName_Hint = null;
			}

			if (EditContactNo_Hint != null) {
				EditContactNo_Hint.Dispose ();
				EditContactNo_Hint = null;
			}

			if (EditAddress_Hint != null) {
				EditAddress_Hint.Dispose ();
				EditAddress_Hint = null;
			}

			if (EditZipCode_Hint != null) {
				EditZipCode_Hint.Dispose ();
				EditZipCode_Hint = null;
			}

			if (EditSecPh_Hint != null) {
				EditSecPh_Hint.Dispose ();
				EditSecPh_Hint = null;
			}

			if (EditEmail_Hint != null) {
				EditEmail_Hint.Dispose ();
				EditEmail_Hint = null;
			}

			if (SaveEditProfile_Btn != null) {
				SaveEditProfile_Btn.Dispose ();
				SaveEditProfile_Btn = null;
			}

			if (EditProfile_scrollView != null) {
				EditProfile_scrollView.Dispose ();
				EditProfile_scrollView = null;
			}

			if (FullName_EditField != null) {
				FullName_EditField.Dispose ();
				FullName_EditField = null;
			}

			if (ContactNo_EditField != null) {
				ContactNo_EditField.Dispose ();
				ContactNo_EditField = null;
			}

			if (Address_EditField != null) {
				Address_EditField.Dispose ();
				Address_EditField = null;
			}

			if (ZipCode_EditField != null) {
				ZipCode_EditField.Dispose ();
				ZipCode_EditField = null;
			}

			if (SecPhone_EditField != null) {
				SecPhone_EditField.Dispose ();
				SecPhone_EditField = null;
			}

			if (Email_EditField != null) {
				Email_EditField.Dispose ();
				Email_EditField = null;
			}
		}
	}
}
