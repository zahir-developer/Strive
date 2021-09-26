using System;

using Foundation;
using UIKit;

namespace Greeter.Cells
{
    public partial class IssueImageCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("IssueImageCollectionViewCell");
        public static readonly UINib Nib;

        static IssueImageCollectionViewCell()
        {
            Nib = UINib.FromName("IssueImageCollectionViewCell", NSBundle.MainBundle);
        }

        protected IssueImageCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
