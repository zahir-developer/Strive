using System;
using Foundation;
using MvvmCross.ViewModels;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Utils.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger.Chat
{
    public class ChatDataSource : UITableViewSource
    {
        public MvxObservableCollection<ChatMessageDetail> ChatMessages;
        public ChatDataSource(MvxObservableCollection<ChatMessageDetail> list)
        {
            ChatMessages = list;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if(ChatMessages[indexPath.Row].SenderId != EmployeeTempData.EmployeeID)
            {
                var cell = tableView.DequeueReusableCell("MessageIncomingCell", indexPath) as MessageIncomingCell;
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.SetData(ChatMessages[indexPath.Row]);
                return cell;
            }
            else
            {
                var cell = tableView.DequeueReusableCell("MessageOutgoingCell", indexPath) as MessageOutgoingCell;
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.SetData(ChatMessages[indexPath.Row]);
                return cell;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ChatMessages.Count;
        }
    }
}
