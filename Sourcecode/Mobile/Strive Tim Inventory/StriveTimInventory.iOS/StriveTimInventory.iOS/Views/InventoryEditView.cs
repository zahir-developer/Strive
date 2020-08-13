using System;
using MvvmCross.Binding.BindingContext;
using UIKit;
using MvvmCross;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Messenger;
using Strive.Core.ViewModels.TIMInventory;
using Strive.Core.Utils;
using Foundation;

namespace StriveTimInventory.iOS.Views
{
    public partial class InventoryEditView : MvxViewController<InventoryEditViewModel>
    {
        public static IMvxMessenger _mvxMessenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        private MvxSubscriptionToken _messageToken;

        UIImagePickerController imagePicker;

        public InventoryEditView() : base("InventoryEditView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);

            var set = this.CreateBindingSet<InventoryEditView, InventoryEditViewModel>();
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(EditImageButton).To(vm => vm.Commands["NavigateUploadImage"]);
            set.Bind(RootView).For(v=> v.Alpha).To(vm => vm.ViewAlpha);
            set.Bind(ItemCode).To(vm => vm.ItemCode);
            set.Bind(ItemName).To(vm => vm.ItemName);
            set.Bind(ItemDescription).To(vm => vm.ItemDescription);
            set.Bind(ItemQuantity).To(vm => vm.ItemQuantity);
            set.Apply();
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            imagePicker = new UIImagePickerController();
            ViewModel.ViewAlpha = 1;
            if (message.Valuea == 2)
            {
                CaptureFromCamera();
            }
            else if (message.Valuea == 3)
            {
                CaptureFromPhotoLibrary();
            }
        }

        private void CaptureFromCamera()
        {
            imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
            PickImage();
        }

        private void CaptureFromPhotoLibrary()
        {
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            PickImage();
        }

        private void PickImage()
        {
            imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
            imagePicker.Canceled += Handle_Canceled;

            PresentViewController(imagePicker, true, () => { });
        }

        private void Handle_Canceled(object sender, EventArgs e)
        {
            imagePicker.DismissModalViewController(true);
        }

        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine("Url:" + referenceURL.ToString());

            if (isImage)
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    ItemImage.Image = originalImage; 
                }

                UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
                if (editedImage != null)
                {
                    Console.WriteLine("got the edited image");
                    ItemImage.Image = editedImage;
                }
            }

            imagePicker.DismissModalViewController(true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        //public override void ViewDidDisappear(bool animated)
        //{
        //    _messageToken.Dispose();
        //}

    }
}

