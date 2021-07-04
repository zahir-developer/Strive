using System;

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

        internal void UpdateData(string path, bool isCloseOptionNeeded = false)
        {
            //imgvClose.Layer.CornerRadius = 28;
            //imgvClose.Layer.MasksToBounds = true;
            //imgvClose.ClipsToBounds = true;

            imgvClose.Hidden = !isCloseOptionNeeded;

            if (path is not null)
                imgv.Image = UIImage.FromFile(path);
        }
    }
}
