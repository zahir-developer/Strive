using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MvvmCross;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{
    public class MessengerRecents_DataSource : UITableViewSource
    {
        private List<ChatEmployeeList> recentContacts = new List<ChatEmployeeList>();
        public IMessengerService MessengerService = Mvx.IoCProvider.Resolve<IMessengerService>();
        public MessengerRecentContactsViewModel recentViewModel;

        public MessengerRecents_DataSource(List<ChatEmployeeList> Contacts, MessengerRecentContactsViewModel viewModel)
        {
            this.recentContacts = Contacts;
            this.recentViewModel = viewModel;
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

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            MessengerTempData.resetChatData();
            if (MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).IsGroup)
            {
                MessengerTempData.GroupID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).Id;
                MessengerTempData.IsGroup = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).IsGroup;
                MessengerTempData.GroupName = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).FirstName;
                MessengerTempData.GroupUniqueID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).CommunicationId;
                MessengerTempData.ConnectionID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).CommunicationId; ;
                MessengerTempData.RecipientID = 0;
            }
            else
            {
                MessengerTempData.GroupID = 0;
                MessengerTempData.IsGroup = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).IsGroup;
                MessengerTempData.RecipientName = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).FirstName + " " + MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).LastName;
                MessengerTempData.GroupUniqueID = null;
                MessengerTempData.RecipientID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).Id;
                MessengerTempData.ConnectionID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(indexPath.Row).CommunicationId;
                getRecentChats();                
                //var selectedData = data.EmployeeList.ChatEmployeeList.Find(x => x.Id == MessengerTempData.RecipientID);
                //MessengerTempData.ConnectionID = selectedData.CommunicationId;
            }
            recentViewModel.navigateToChat();
        }

        public async void getRecentChats()
        {
            var data = await MessengerService.GetRecentContacts(EmployeeTempData.EmployeeID);
        }
    }
}
