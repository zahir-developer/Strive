using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using UIKit;

namespace StriveOwner.iOS.Views.Messenger
{
    public class Contact_DataSource : UITableViewSource
    {
        private List<Employee> contacts = new List<Employee>();
        public Contact_DataSource(List<Employee> contactList)
        {
            this.contacts = contactList;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }        

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return contacts.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Contact_CellView", indexPath) as Contact_CellView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            //cell.SetupData(ContactCellConfigureType.ContactList);
            cell.SetData(indexPath, contacts[indexPath.Row]);
            return cell;
        }
    }
}
