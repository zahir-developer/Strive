// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Foundation;
using Greeter.Cells;
using Greeter.Sources;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class AddIssueViewController : UIViewController, IUITextViewDelegate
    {
        UIImagePickerController imagePickerController;
        List<string> imagePaths;

        private const string PUBLIC_IMAGE = "public.image";
        private const string PUBLIC_VIDEO = "public.video";

        public AddIssueViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            tvIssueDetail.Delegate = this;

            Initialise();

            //   tvIssueDetail.EndEditing += delegate {
            //       return true;
            //};

            //tvIssueDetail.ShouldChangeText += delegate
            //{
            //    Debug.WriteLine("Chnged Text : " + tvIssueDetail.Text);
            //    return true;
            //};

            //tvIssueDetail.ShouldChangeTextInRange = (range, replacementText) =>
            //{
            //    return true;
            //};

            //mobNoTxtField.ShouldChangeCharacters = (textField, range, replacementString) =>
            //{
            //    var newLength = textField.Text.Length + replacementString.Length - range.Length;
            //    return newLength <= 14;
            //};

            tvIssueDetail.Layer.BorderWidth = 1;
            tvIssueDetail.Layer.BorderColor = UIColor.LightGray.CGColor;
            tvIssueDetail.Layer.CornerRadius = 5;

            //Clicks
            lblAddPhotos.AddGestureRecognizer(new UITapGestureRecognizer(OpenCamera));
            btnCancel.TouchUpInside += (s, e) => DismissViewController(true, null);
            btnSave.TouchUpInside += BtnSave_TouchUpInside;
        }

        void Initialise()
        {
            ShowOrHideHint(tvIssueDetail.Text.Length, lblIssueDetailHint);

            cvImages.RegisterNibForCell(ImageCell.Nib, ImageCell.Key);
            cvImages.Source = new ImagesSource(imagePaths);

            CreateCamera();
        }

        void ShowOrHideHint(int len, UILabel lbl)
        {
            lbl.Hidden = len == 0 ? false : true;
        }

        private void BtnSave_TouchUpInside(object sender, EventArgs e)
        {

        }

        [Export("textView:shouldChangeTextInRange:replacementText:")]
        public bool ShouldChangeText(UITextView textView, NSRange range, string text)
        {
            int len = textView.Text.Length + text.Length - (int)range.Length;
            ShowOrHideHint(len, lblIssueDetailHint);
            return true;
        }

        // CHOOSE CAMERA CANCELLED ACTION
        void Canceled(object sender, EventArgs e)
        {
            CloseCamera();
        }

        void CreateCamera()
        {
            imagePickerController = new UIImagePickerController();
            imagePickerController.SourceType = UIImagePickerControllerSourceType.Camera;
            imagePickerController.FinishedPickingMedia += Finished;
            imagePickerController.Canceled += Canceled;
        }

        void OpenCamera()
        {
            PresentViewController(imagePickerController, true, null);
        }

        // IMAGE CHOOSED ACTION
        public void Finished(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            CloseCamera();

            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case PUBLIC_IMAGE:
                    isImage = true;
                    break;
                case PUBLIC_VIDEO:
                    break;
            }

            if (!isImage) return;

            UIImage image = e.Info[UIImagePickerController.OriginalImage] as UIImage;


            NSUrl referenceURL = e.Info[UIImagePickerController.ReferenceUrl] as NSUrl;

            if (referenceURL != null)
            {
                Debug.WriteLine("Image Path : " + referenceURL.ToString());
            }
            else return;

            if (imagePaths is null)
            {
                imagePaths = new List<string>();
            }

            var imagePath = "";
            imagePaths.Add(imagePath);

            cvImages.Source = new ImagesSource(imagePaths);
            cvImages.ReloadData();
        }

        void CloseCamera()
        {
            imagePickerController.DismissViewController(true, null);
        }
    }
}
