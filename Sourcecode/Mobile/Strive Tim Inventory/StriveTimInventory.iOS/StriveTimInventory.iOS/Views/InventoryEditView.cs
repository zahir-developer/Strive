using System;
using MvvmCross.Binding.BindingContext;
using UIKit;
using MvvmCross;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Messenger;
using Strive.Core.ViewModels.TIMInventory;
using Strive.Core.Utils;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using System.Collections;
using CoreGraphics;
using CoreImage;

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
            SaveButton.Layer.CornerRadius = CancelButton.Layer.CornerRadius = 5;
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
            var pickerView = new UIPickerView();
            var PickerViewModel = new InventoryPicker(pickerView,ViewModel);
            pickerView.Model = PickerViewModel;
            pickerView.ShowSelectionIndicator = true;
            SupplierName.InputView = pickerView;

            var set = this.CreateBindingSet<InventoryEditView, InventoryEditViewModel>();
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(EditImageButton).To(vm => vm.Commands["NavigateUploadImage"]);
            set.Bind(SaveButton).To(vm => vm.Commands["AddorUpdate"]);
            set.Bind(CancelButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(LogoutButtton).To(vm => vm.Commands["Logout"]);
            set.Bind(RootView).For(v=> v.Alpha).To(vm => vm.ViewAlpha);
            set.Bind(ItemCode).To(vm => vm.ItemCode);
            set.Bind(ItemName).To(vm => vm.ItemName);
            set.Bind(ItemDescription).To(vm => vm.ItemDescription);
            set.Bind(ItemQuantity).To(vm => vm.ItemQuantity);
            set.Bind(SupplierName).To(vm => vm.SupplierName);
            set.Bind(SupplierContact).To(vm => vm.SupplierContact);
            set.Bind(SupplierFax).To(vm => vm.SupplierFax);
            set.Bind(SupplierAddress).To(vm => vm.SupplierAddress);
            set.Bind(SupplierEmail).To(vm => vm.SupplierEmail);
            set.Apply();
        }

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            var height = UIScreen.MainScreen.Bounds;
            var width = UIScreen.MainScreen.Bounds;
            if(width.Width > height.Height)
            {
                portconstone.Active = portconsttwo.Active =
                    portconstthree.Active = portconstfour.Active =
                    portconsfive.Active = portconstsix.Active = portconstseven.Active = false;
                landxonstone.Active = landconsttwo.Active =
                     landconstthree.Active = landconstfour.Active =
                     landconstfive.Active= landconstsix.Active = landconstseven.Active = true;
                
            } else
            {
                landxonstone.Active = landconsttwo.Active =
                    landconstthree.Active = landconstfour.Active =
                    landconstfive.Active = landconstsix.Active = landconstseven.Active = false;
                portconstone.Active = portconsttwo.Active =
                    portconstthree.Active = portconstfour.Active =
                    portconsfive.Active = portconstsix.Active = portconstseven.Active = true;
            }
            View.SetNeedsLayout();
            UIView.Animate(0.3, () => { View.LayoutIfNeeded(); });

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
            else if (message.Valuea == 4)
            {
                SetImage(message.Valueb);
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

        private void SetImage(string url)
        {
            ItemImage.Image = UIImage.FromBundle(url);
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

