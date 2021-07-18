using System;
using Foundation;
using UIKit;

namespace StriveOwner.iOS.Views.Messenger.Chat
{
    public class ChatDataSource : UITableViewSource
    {
        public ChatDataSource()
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            throw new NotImplementedException();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 5;
        }
    }
}
