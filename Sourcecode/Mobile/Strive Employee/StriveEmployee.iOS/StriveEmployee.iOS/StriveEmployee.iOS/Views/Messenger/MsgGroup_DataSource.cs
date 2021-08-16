using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{
    public class MsgGroup_DataSource : UITableViewSource
    {
        MessengerGroupContactViewModel groupViewModel;
        List<ChatEmployeeList> groupChats;
        public MsgGroup_DataSource(List<ChatEmployeeList> list, MessengerGroupContactViewModel viewModel)
        {
            groupViewModel = viewModel;
            groupChats = list;   
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Contact_CellView", indexPath) as Contact_CellView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.setGroupCell(groupChats[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return groupChats.Count;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            MessengerTempData.resetChatData();
            MessengerTempData.GroupID = MessengerTempData.GroupLists.ChatEmployeeList[indexPath.Row].Id;
            MessengerTempData.IsGroup = MessengerTempData.GroupLists.ChatEmployeeList.ElementAt(indexPath.Row).IsGroup;
            MessengerTempData.GroupName = MessengerTempData.GroupLists.ChatEmployeeList.ElementAt(indexPath.Row).FirstName;
            var data = MessengerTempData.GroupLists.ChatEmployeeList.Find(x => x.Id == MessengerTempData.GroupID);
            MessengerTempData.GroupUniqueID = data.CommunicationId;
            MessengerTempData.ConnectionID = data.CommunicationId;
            MessengerTempData.RecipientID = 0;

            groupViewModel.navigateToChat();
        }
    }
}
