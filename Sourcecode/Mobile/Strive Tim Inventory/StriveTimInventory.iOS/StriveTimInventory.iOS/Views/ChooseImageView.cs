using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross;
using System.Threading.Tasks;

namespace StriveTimInventory.iOS.Views
{
    [MvxModalPresentation(
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
        Animated = true,
        ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]
    public partial class ChooseImageView : MvxViewController<ChooseImageViewModel>
    {
        public ChooseImageView() : base("ChooseImageView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            var set = this.CreateBindingSet<ChooseImageView, ChooseImageViewModel>();
            set.Bind(NotNowButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(CameraButton).To(vm => vm.Commands["NavigateCamera"]);
            set.Bind(BrowseButton).To(vm => vm.Commands["NavigatePhotoLibrary"]);
            set.Apply();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        void DoInitialSetup()
        {
            CameraView.Layer.BorderWidth = BrowseView.Layer.BorderWidth = IconView.Layer.BorderWidth = 1;
            CameraView.Layer.BorderColor = BrowseView.Layer.BorderColor = IconView.Layer.BorderColor = UIColor.Gray.CGColor;
            IconButton.Layer.CornerRadius = 5;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

