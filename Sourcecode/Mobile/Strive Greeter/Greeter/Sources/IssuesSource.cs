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
            UITableViewCell cell = tableView.DequeueReusableCell(IssueCell.Key);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 10;
        }
    }
}
