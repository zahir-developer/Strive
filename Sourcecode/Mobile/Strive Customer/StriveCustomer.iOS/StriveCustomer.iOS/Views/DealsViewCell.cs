using System;

using Foundation;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class DealsViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DealsViewCell");
        public static readonly UINib Nib;

        static DealsViewCell()
        {
            Nib = UINib.FromName("DealsViewCell", NSBundle.MainBundle);
        }

        protected DealsViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
