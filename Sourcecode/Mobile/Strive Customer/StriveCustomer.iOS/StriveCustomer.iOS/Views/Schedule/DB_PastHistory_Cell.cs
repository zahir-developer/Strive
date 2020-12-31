using System;

using Foundation;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class DB_PastHistory_Cell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DB_PastHistory_Cell");
        public static readonly UINib Nib;

        static DB_PastHistory_Cell()
        {
            Nib = UINib.FromName("DB_PastHistory_Cell", NSBundle.MainBundle);
        }

        protected DB_PastHistory_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
