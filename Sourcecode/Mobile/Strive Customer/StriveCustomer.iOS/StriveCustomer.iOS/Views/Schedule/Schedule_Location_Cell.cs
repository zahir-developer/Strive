using System;

using Foundation;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_Location_Cell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("Schedule_Location_Cell");
        public static readonly UINib Nib;

        static Schedule_Location_Cell()
        {
            Nib = UINib.FromName("Schedule_Location_Cell", NSBundle.MainBundle);
        }

        protected Schedule_Location_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(ScheduleLocationsViewModel viewModel, NSIndexPath indexPath)
        {
            ScheduleLocation_Lbl.Text = viewModel.Locations.Location[indexPath.Row].Address1;
        }

        public void updateCell(NSIndexPath indexPath)
        {
            scheduleLoc_Btn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);                
        }

        public void deselectRow(NSIndexPath indexPath)
        {
            scheduleLoc_Btn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
        }
    }
}