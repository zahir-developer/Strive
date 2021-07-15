using System;
using Foundation;
using UIKit;

namespace StriveOwner.iOS.Views.Messenger
{
    public class Messenger_BaseDataSource : UITableViewSource
    {
        public Messenger_BaseDataSource()
        {
        }
               
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Messenger_CellView", indexPath) as Messenger_CellView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetupData();
            return cell;
        }
    }
}
