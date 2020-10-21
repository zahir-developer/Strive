using System;

using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class PastDetailViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("PastDetailViewCell");
        public static readonly UINib Nib;

        static PastDetailViewCell()
        {
            Nib = UINib.FromName("PastDetailViewCell", NSBundle.MainBundle);
        }

        protected PastDetailViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetCell()
        {
            PastDetailCellView.Layer.CornerRadius = 5;
        }
    }
}
