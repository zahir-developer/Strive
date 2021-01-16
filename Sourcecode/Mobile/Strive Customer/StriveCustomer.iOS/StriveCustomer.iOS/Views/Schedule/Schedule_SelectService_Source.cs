using System;
using Foundation;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public class Schedule_SelectService_Source : UITableViewSource
    {
        public Schedule_SelectService_Source()
        {
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 90;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 10;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Schedule_SelectService_Cell", indexPath) as Schedule_SelectService_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(indexPath);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            
        }

    }
}
