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
using System.Threading.Tasks;

namespace StriveTimInventory.iOS.Views
{
    public partial class InventoryEditView : MvxViewController<InventoryEditViewModel>
    {
        public static IMvxMessenger _mvxMessenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        private MvxSubscriptionToken _messageToken;
        public bool isCaptured;
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

            var pickerView1 = new UIPickerView();
            var pickerViewModel1 = new InventoryNewPicker(pickerView1, ViewModel, true);
            pickerView1.Model = pickerViewModel1;
            pickerView1.ShowSelectionIndicator = true;
            ItemLocation.InputView = pickerView1;

            var pickerView2 = new UIPickerView();
            var pickerViewModel2 = new InventoryNewPicker(pickerView2, ViewModel, false);
            pickerView2.Model = pickerViewModel2;
            pickerView2.ShowSelectionIndicator = true;
            ItemType.InputView = pickerView2;

            if(ViewModel.Base64String != null)
            {
                var imageBytes = Convert.FromBase64String(ViewModel.Base64String);
                var imageData = NSData.FromArray(imageBytes);
                var uiImage = UIImage.LoadFromData(imageData);
                ItemImage.Image = uiImage;
            }

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
            set.Bind(ItemLocation).To(vm => vm.ItemLocation);
            set.Bind(ItemType).To(vm => vm.ItemType);
            set.Bind(ItemCost).To(vm => vm.ItemCost);
            set.Bind(ItemPrice).To(vm => vm.ItemPrice);
            set.Bind(SupplierName).To(vm => vm.SupplierName);
            set.Bind(SupplierContact).To(vm => vm.SupplierContact);
            set.Bind(SupplierFax).To(vm => vm.SupplierFax);
            set.Bind(SupplierAddress).To(vm => vm.SupplierAddress);
            set.Bind(SupplierEmail).To(vm => vm.SupplierEmail);
            set.Apply();

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false; 
            View.AddGestureRecognizer(Tap);

            ItemQuantity.KeyboardType = UIKeyboardType.NumberPad;
            ItemCost.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            ItemPrice.KeyboardType = UIKeyboardType.NumberPad;

            //ChangeOrientation();
        }

        public override async void ViewDidAppear(bool animated)
        {
            var locationList = await GetLocationList();
        }

        private async Task<bool> GetLocationList()
        {
            await ViewModel.GetAllLocNameCommand();
            await ViewModel.GetProductTypeCommand();
            return true;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.Portrait;
        }

        //public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        //{
        //    ChangeOrientation();
        //}

        void ChangeOrientation()
        {
            var height = UIScreen.MainScreen.Bounds;
            var width = UIScreen.MainScreen.Bounds;
            if (width.Width > height.Height)
            {
                portconstone.Active = portconsttwo.Active =
                    portconstthree.Active = portconstfour.Active =
                    portconsfive.Active = portconstsix.Active = portconstseven.Active = false;
                landxonstone.Active = landconsttwo.Active =
                     landconstthree.Active = landconstfour.Active =
                     landconstfive.Active = landconstsix.Active = landconstseven.Active = true;

            }
            else
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
            isCaptured = true;
            PickImage();
        }

        private void CaptureFromPhotoLibrary()
        {
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            PickImage();
        }

        private void SetImage(string url)
        {
            ViewModel.Filename = url + ".png";
            ItemImage.Image = UIImage.FromBundle(url);
            ConvertToBase64(UIImage.FromBundle(url));
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

            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerImageURL")] as NSUrl;
            if (referenceURL != null)
            {
                Console.WriteLine("Url:" + referenceURL.ToString());
                string[] list = referenceURL.ToString().Split("/");
                foreach(var item in list)
                {
                    if(item.EndsWith(".png") || item.EndsWith(".jpeg"))
                    {
                        ViewModel.Filename = item;
                    }
                }
            }
            else
            {
                if (isCaptured)
                {
                    System.Random random = new System.Random();
                         
                    ViewModel.Filename = "Image" + random.Next().ToString() + ".png"; 
                }
            }

            if (isImage)
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    ItemImage.Image = originalImage;
                    ConvertToBase64(originalImage);
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

        void ConvertToBase64(UIImage originalImage)
        {
            NSData imageData = originalImage.AsPNG();
            Byte[] myByteArray = new Byte[imageData.Length];
            System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
            string Base64String = Convert.ToBase64String(myByteArray);
            ViewModel.Base64String = Base64String;
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

