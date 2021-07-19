using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{
    public class MsgGroup_DataSource : UITableViewSource
    {
        public List<String> groupList = new List<string>();
        public MsgGroup_DataSource()
        {
            groupList.Add("Washers Team");
            groupList.Add("Detailers Team");
            groupList.Add("Old Milton Team");
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Contact_CellView", indexPath) as Contact_CellView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.setGroupCell(groupList[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return groupList.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }
    }
}
