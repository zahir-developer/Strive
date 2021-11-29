using System;
using Foundation;
using Strive.Core.ViewModels.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger.Chat
{
    public class ChatDataSource : UITableViewSource
    {
        MessengerPersonalChatViewModel ViewModel;
        public ChatDataSource(MessengerPersonalChatViewModel view)
        {
            this.ViewModel = view;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if(ViewModel.chatMessages.ChatMessage.ChatMessageDetail[indexPath.Row].ReceipientId != 0)
            {
                var cell = tableView.DequeueReusableCell("MessageIncomingCell", indexPath) as MessageIncomingCell;
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.SetData(ViewModel.chatMessages.ChatMessage.ChatMessageDetail[indexPath.Row]);
                return cell;
            }
            else
            {
                var cell = tableView.DequeueReusableCell("MessageOutgoingCell", indexPath) as MessageOutgoingCell;
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.SetData(ViewModel.chatMessages.ChatMessage.ChatMessageDetail[indexPath.Row]);
                return cell;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ViewModel.chatMessages.ChatMessage.ChatMessageDetail.Count;
        }
    }
}
