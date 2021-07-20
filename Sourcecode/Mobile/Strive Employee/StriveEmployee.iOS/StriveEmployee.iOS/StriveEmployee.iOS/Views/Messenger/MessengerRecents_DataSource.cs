using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Employee.Messenger;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{
    public class MessengerRecents_DataSource : UITableViewSource
    {
        private List<ChatEmployeeList> recentContacts = new List<ChatEmployeeList>();
        public MessengerRecents_DataSource(List<ChatEmployeeList> Contacts)
        {
            this.recentContacts = Contacts;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Messenger_CellView", indexPath) as Messenger_CellView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetupData(recentContacts[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return recentContacts.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }
    }
}
