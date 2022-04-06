using System;
using CoreGraphics;
using Foundation;
using Greeter.Extensions;
using UIKit;

namespace Greeter.Cells
{
    public partial class ImageCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("ImageCell");
        public static readonly UINib Nib;

        static ImageCell()
        {
            Nib = UINib.FromName("ImageCell", NSBundle.MainBundle);
        }

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();

        }
        protected ImageCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            //imgvClose.MakeRoundedView();

            //Clicks
            imgvClose.AddGestureRecognizer(new UITapGestureRecognizer(DeleteImage));
        }

        void DeleteImage()
        {
            // TODO : remove image and reload list
        }

        internal void UpdateData(UIImage path, bool isCloseOptionNeeded = false)
        {
            //imgvClose.Layer.CornerRadius = 28;
            //imgvClose.Layer.MasksToBounds = true;
            //imgvClose.ClipsToBounds = true;

            imgvClose.Hidden = !isCloseOptionNeeded;
            imgv.Transform = CGAffineTransform.MakeRotation(3.14159f * 90 / 180f);
            if (path is not null)
                imgv.Image = path;
        }
        public void ImageSelected(int row)
        {
            Console.WriteLine("Selected image"+row);
            var dict = new NSDictionary(new NSString("IssueImageId"), new NSString(row.ToString()));
            NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.View_Clicked"), null, dict);
        }
    }
}
