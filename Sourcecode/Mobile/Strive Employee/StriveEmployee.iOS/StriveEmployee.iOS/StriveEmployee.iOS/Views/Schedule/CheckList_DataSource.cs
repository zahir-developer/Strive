using System;
using Foundation;
using Strive.Core.Models.Employee.CheckList;
using UIKit;
namespace StriveEmployee.iOS.Views.Schedule

{
    public class CheckList_DataSource : UITableViewSource
    {
        string sampletask;
        Checklist checklist;
        public CheckList_DataSource(Checklist _checklist)
        {
            checklist = _checklist;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CheckListTableCell", indexPath) as CheckListTableCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.updateData(checklist,indexPath);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return checklist.ChecklistNotification.Count;
        }
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 110;
        }
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            CheckListTableCell cell = (CheckListTableCell)tableView.CellAt(indexPath);
            cell.updateServices(checklist,indexPath);
        }
    }
}
