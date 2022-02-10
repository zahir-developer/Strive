using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Employee.Detailer;
using UIKit;

namespace StriveEmployee.iOS.Views.Schedule
{
    public class Detailer_DataSource : UITableViewSource
    {
        status DetailerData;
        public Detailer_DataSource(status detailerData)
        {
            DetailerData = detailerData;

        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DetailerTableCell", indexPath) as DetailerTableCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.updateData(DetailerData, indexPath);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return DetailerData.Status.Count;
        }
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 220;
        }
    }
}
