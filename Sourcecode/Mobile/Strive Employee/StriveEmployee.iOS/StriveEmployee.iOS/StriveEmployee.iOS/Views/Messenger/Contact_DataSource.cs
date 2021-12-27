using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{
    public class Contact_DataSource : UITableViewSource
    {
        private List<Employee> contacts = new List<Employee>();
        MessengerContactViewModel ViewModel;
        public Contact_DataSource(List<Employee> contactList, MessengerContactViewModel viewModel)
        {
            this.contacts = contactList;
            ViewModel = viewModel;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Contact_CellView", indexPath) as Contact_CellView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(indexPath, contacts[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return contacts.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            MessengerTempData.resetChatData();
            MessengerTempData.GroupID = 0;

            MessengerTempData.IsGroup = false;

            MessengerTempData.RecipientName = MessengerTempData.employeeList_Contact.EmployeeList.Employee.ElementAt(indexPath.Row).FirstName + " " + MessengerTempData.employeeList_Contact.EmployeeList.Employee.ElementAt(indexPath.Row).LastName;
            MessengerTempData.GroupUniqueID = null;
            MessengerTempData.RecipientID = MessengerTempData.employeeList_Contact.EmployeeList.Employee.ElementAt(indexPath.Row).EmployeeId;

            if (MessengerTempData.RecipientsConnectionID == null)
            {
                MessengerTempData.RecipientsConnectionID = new Dictionary<string, string>();
            }
            if (!MessengerTempData.RecipientsConnectionID.ContainsKey(MessengerTempData.RecipientID.ToString()))
            {
                if (MessengerTempData.ConnectionID != null)
                    MessengerTempData.RecipientsConnectionID.Add(MessengerTempData.RecipientID.ToString(), MessengerTempData.ConnectionID);
            }

            ViewModel.navigateToChat();
        }
    }
}
