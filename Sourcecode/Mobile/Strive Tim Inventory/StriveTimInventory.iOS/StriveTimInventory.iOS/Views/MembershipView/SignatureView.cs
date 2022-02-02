using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CoreGraphics;
using Foundation;
using MvvmCross;
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
        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        public SignatureView() : base("SignatureView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            FinalContract.Hidden = true;
			var set = this.CreateBindingSet<SignatureView, SignatureViewModel>();
			set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
			set.Bind(CancelButton).To(vm => vm.Commands["NavigateBack"]);
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
            
            var bitmap = await SignPad.GetImageStreamAsync(SignatureImageFormat.Png, UIColor.Black, UIColor.White, 1f);
            if(bitmap == null)
            {
                await _userDialog.AlertAsync("Please sign to complete the membership.");
                return;
            }
			using (var data = NSData.FromStream(bitmap))
			{
				image = UIImage.LoadFromData(data);
			}
            
            CustomerSignatureIMG.Image = image;
            FinalContractIMG.Image = TermsView.TermsViewImg;
            FinalContract.Hidden = false;
            UIImage Contract = UIViewExtensions.AsImage(FinalContract);
            SignatureViewModel.Base64ContractString = Contract.AsJPEG(0.15f).GetBase64EncodedString(NSDataBase64EncodingOptions.None);

            await Task.Delay(3000);
                ViewModel.NextCommand();
        }

        partial void CancelButtonClicked(UIButton sender)
        {
			SignPad.Clear();
        }
    }
}

