using System;

using Foundation;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectService_Cell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("Schedule_SelectService_Cell");
        public static readonly UINib Nib;

        static Schedule_SelectService_Cell()
        {
            Nib = UINib.FromName("Schedule_SelectService_Cell", NSBundle.MainBundle);
        }

        protected Schedule_SelectService_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(NSIndexPath indexPath)
        {
            SelectService_CellView.Layer.CornerRadius = 5;
        }
    }
}
