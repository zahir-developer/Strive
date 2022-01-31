// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace StriveTimInventory.iOS.Views.MembershipView
{
	[Register ("SignatureView")]
	partial class SignatureView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		UIKit.UIImageView CustomerSignatureIMG { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton DoneButton { get; set; }

		[Outlet]
		UIKit.UIView FinalContract { get; set; }

		[Outlet]
		UIKit.UIImageView FinalContractIMG { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Xamarin.Controls.SignaturePadView SignPad { get; set; }

		[Action ("CancelButtonClicked:")]
		partial void CancelButtonClicked (UIKit.UIButton sender);

		[Action ("DoneButtonClicked:")]
		partial void DoneButtonClicked (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
			}

			if (SignPad != null) {
				SignPad.Dispose ();
				SignPad = null;
			}

			if (FinalContract != null) {
				FinalContract.Dispose ();
				FinalContract = null;
			}

			if (CustomerSignatureIMG != null) {
				CustomerSignatureIMG.Dispose ();
				CustomerSignatureIMG = null;
			}

			if (FinalContractIMG != null) {
				FinalContractIMG.Dispose ();
				FinalContractIMG = null;
			}
		}
	}
}
