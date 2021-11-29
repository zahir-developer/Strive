﻿using System;

using Foundation;
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
    }
}
