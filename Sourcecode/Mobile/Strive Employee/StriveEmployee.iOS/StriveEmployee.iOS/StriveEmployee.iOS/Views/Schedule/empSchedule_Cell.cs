using System;

using Foundation;
using Strive.Core.Models.Owner;
using UIKit;

namespace StriveEmployee.iOS.Views.Schedule
{
    public partial class empSchedule_Cell : UITableViewCell
    {
        public ScheduleDetailViewModel scheduleItem;
        public static readonly NSString Key = new NSString("empSchedule_Cell");
        public static readonly UINib Nib;

        static empSchedule_Cell()
        {
            Nib = UINib.FromName("empSchedule_Cell", NSBundle.MainBundle);
        }

        protected empSchedule_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void updateData(ScheduleDetailViewModel item)
        {
            this.scheduleItem = item;

            var startTime = item.StartTime.Substring(11, 5);
            var endTime = item.EndTime.Substring(11, 5);
            EmpScheduleLbl.Text = scheduleItem.LocationName;
            EmpScheduleTimeLbl.Text = startTime + " - " + endTime;
        }
    }
}
