using System;

using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_Time_Cell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("Schedule_Time_Cell");
        public static readonly UINib Nib;
        AvailableScheduleSlots timeSlots = new AvailableScheduleSlots();
        static Schedule_Time_Cell()
        {
            Nib = UINib.FromName("Schedule_Time_Cell", NSBundle.MainBundle);
        }

        protected Schedule_Time_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(AvailableScheduleSlots slots, NSIndexPath indexPath)
        {
            this.timeSlots = slots;
            Time_CellView.Layer.CornerRadius = 5;
            string timeIn = slots.GetTimeInDetails[indexPath.Row].TimeIn;
            TimeSlot_Btn.SetTitle(timeIn, UIControlState.Normal);
        }        
    }
}
 