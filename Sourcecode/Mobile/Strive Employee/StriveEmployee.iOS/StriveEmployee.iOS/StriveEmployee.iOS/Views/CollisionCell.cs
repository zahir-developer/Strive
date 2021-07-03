using System;

using Foundation;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public partial class CollisionCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CollisionCell");
        public static readonly UINib Nib;

        static CollisionCell()
        {
            Nib = UINib.FromName("CollisionCell", NSBundle.MainBundle);
        }

        protected CollisionCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(NSIndexPath indexpath)
        {

        }
    }
}
