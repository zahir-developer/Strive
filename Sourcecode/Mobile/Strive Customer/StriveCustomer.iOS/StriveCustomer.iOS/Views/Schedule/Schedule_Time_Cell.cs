using System;

using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Utils;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_Time_Cell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("Schedule_Time_Cell");
        public static readonly UINib Nib;
        public NSIndexPath selectedTime;
        public NSIndexPath oldCell;
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
            this.selectedTime = indexPath; 
            Time_CellView.Layer.CornerRadius = 5;
            Time_CellView.BackgroundColor = UIColor.Gray;
            string timeIn = slots.GetTimeInDetails[indexPath.Row].TimeIn;
            TimeSlot_Btn.SetTitle(timeIn, UIControlState.Normal);
        }

        partial void TimeSlot_BtnTouch(UIButton sender)
        {           
            if(Time_CellView.BackgroundColor == UIColor.Gray)
            {
                Time_CellView.BackgroundColor = UIColor.SystemBlueColor;
                CustomerScheduleInformation.ScheduleServiceTime = timeSlots.GetTimeInDetails[selectedTime.Row].TimeIn;               
                //CustomerScheduleInformation.ScheduleTime = CustomerScheduleInformation.ScheduleFullDate + "T" + CustomerScheduleInformation.ScheduleServiceTime + ":00+05:30";
                CustomerScheduleInformation.ScheduleTime = CustomerScheduleInformation.ScheduleFullDate + "T" + CustomerScheduleInformation.ScheduleServiceTime;
                CustomerScheduleInformation.ScheduledBayId = timeSlots.GetTimeInDetails[selectedTime.Row].BayId;               
            }
            else
            {
                Time_CellView.BackgroundColor = UIColor.Gray;                
            }                   
        }       
    }
}
 