using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Photos;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;
using Xamarin.Controls;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class SignatureView : MvxViewController<SignatureViewModel>
    {
        public SignatureView() : base("SignatureView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			var set = this.CreateBindingSet<SignatureView, SignatureViewModel>();
			set.Bind(NextButton).To(vm => vm.Commands["Next"]);
			set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
			set.Apply();

			SignPad.StrokeCompleted += (sender, e) => UpdateControls();
			SignPad.Cleared += (sender, e) => UpdateControls();
		}

        private void UpdateControls()
        {
            DoneButton.Enabled = !SignPad.IsBlank;
        }		

		public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async partial void DoneButtonClicked(UIButton sender)
		{
			UIImage image;
			using (var bitmap = await SignPad.GetImageStreamAsync(SignatureImageFormat.Png, UIColor.Black, UIColor.White, 1f))
			using (var data = NSData.FromStream(bitmap))
			{
				image = UIImage.LoadFromData(data);
			}

			var status = await PHPhotoLibrary.RequestAuthorizationAsync();
			if (status == PHAuthorizationStatus.Authorized)
			{
				image.SaveToPhotosAlbum((i, error) =>
				{
					image.Dispose();

					if (error == null)
					{ }

					else { }

				});
			}
			else
			{

			}
		}

        partial void CancelButtonClicked(UIButton sender)
        {
			SignPad.Clear();
        }
    }
}

