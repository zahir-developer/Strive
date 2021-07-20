using System;
using Foundation;
using Strive.Core.Models.Customer;
using UIKit;

namespace StriveOwner.iOS.Views.HomeView
{
    public class DBSchedule_DataSource : UITableViewSource
    {
        ScheduleModel list;
        public static bool isexpanded = false;
        public DBSchedule_DataSource(ScheduleModel scheduleList)
        {
            list = scheduleList;
        }        

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return list.DetailsGrid.BayJobDetailViewModel.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return isexpanded ? DBSchedule_Cell.ExpandedHeight:DBSchedule_Cell.NormalHeight; 
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DBSchedule_Cell", indexPath) as DBSchedule_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetupView(list.DetailsGrid.BayJobDetailViewModel[indexPath.Row], isexpanded);
             cell.SetupCell(() =>
               tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.None));
            return cell;
        }
    }
}
