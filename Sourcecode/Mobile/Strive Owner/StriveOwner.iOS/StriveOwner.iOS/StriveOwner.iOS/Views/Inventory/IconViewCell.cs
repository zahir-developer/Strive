using System;

using Foundation;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    public partial class IconViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("IconViewCell");
        public static readonly UINib Nib;

        static IconViewCell()
        {
            Nib = UINib.FromName("IconViewCell", NSBundle.MainBundle);
        }

        protected IconViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetCell(IconViewCell cell, string url)
        {
            cell.IconImage.Image = UIImage.FromBundle(url);
        }
    }
}