using System;
using Foundation;
using Greeter.Cells;
using UIKit;

namespace Greeter.Sources
{
    public class IssuesSource : UITableViewSource
    {
        public IssuesSource()
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            IssueCell cell = (IssueCell)tableView.DequeueReusableCell(IssueCell.Key);
            //cell.UpdateData();
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
        }
    }
}
