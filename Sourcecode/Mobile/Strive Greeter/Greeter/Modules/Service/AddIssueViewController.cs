// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Foundation;
using Greeter.Cells;
using Greeter.Sources;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Storyboards
{
    public partial class AddIssueViewController : UIViewController, IUITextViewDelegate
    {
        readonly List<string> imagePaths = new();

        ImagesSource imagesSource;

        public AddIssueViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            tvIssueDetail.Delegate = this;
            Initialise();

            tvIssueDetail.Layer.BorderWidth = 1;
            tvIssueDetail.Layer.BorderColor = UIColor.LightGray.CGColor;
            tvIssueDetail.Layer.CornerRadius = 5;

            //Clicks
            lblAddPhotos.AddGestureRecognizer(new UITapGestureRecognizer(TakePictureFromCamera));
            btnCancel.TouchUpInside += (s, e) => DismissViewController(true, null);
            btnSave.TouchUpInside += BtnSave_TouchUpInside;
        }

        void Initialise()
        {
            ShowOrHideHint(tvIssueDetail.Text.Length, lblIssueDetailHint);
            imagesSource = new ImagesSource(imagePaths);

            cvImages.RegisterNibForCell(ImageCell.Nib, ImageCell.Key);
            cvImages.WeakDataSource = imagesSource;
            cvImages.WeakDelegate = imagesSource;
        }

        void ShowOrHideHint(int len, UILabel lbl)
        {
            lbl.Hidden = len != 0;
        }

        private void BtnSave_TouchUpInside(object sender, EventArgs e)
        {

        }

        [Export("textView:shouldChangeTextInRange:replacementText:")]
        public bool ShouldChangeText(UITextView textView, NSRange range, string replacementString)
        {
            //int len = textView.Text.Length + text.Length - (int)range.Length;

            var oldNSString = new NSString(textView.Text ?? "");
            var replacedString = oldNSString.Replace(range, new NSString(replacementString));

            ShowOrHideHint((int)replacedString.Length, lblIssueDetailHint);

            return true;
        }

        async void TakePictureFromCamera()
        {
            try
            {
                var cameraResponse = await MediaPicker.CapturePhotoAsync();
                var stream = await cameraResponse.OpenReadAsync();

                var tempFilePath = string.Empty;

                if (stream != null && stream != Stream.Null)
                {
                    tempFilePath = Path.Combine(FileSystem.CacheDirectory, cameraResponse.FileName);
                    using (var fileStream = new FileStream(tempFilePath, FileMode.CreateNew))
                    {
                        await stream.CopyToAsync(fileStream);
                        await stream.FlushAsync();
                    }

                    await stream.DisposeAsync();
                }

                imagePaths.Add(tempFilePath);
                cvImages.ReloadData();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}