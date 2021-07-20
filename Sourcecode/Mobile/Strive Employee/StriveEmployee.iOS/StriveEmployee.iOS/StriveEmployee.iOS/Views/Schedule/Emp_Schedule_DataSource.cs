using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Owner;
using UIKit;

namespace StriveEmployee.iOS.Views.Schedule
{
    public class Emp_Schedule_DataSource : UITableViewSource
    {
        List<ScheduleDetailViewModel> schedules;
        public Emp_Schedule_DataSource(List<ScheduleDetailViewModel> scheduleList)
        {
            this.schedules = scheduleList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("empSchedule_Cell", indexPath) as empSchedule_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.updateData(schedules[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return schedules.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 70;
        }
    }
}
