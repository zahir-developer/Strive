using System;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using Greeter.DTOs;
using UIKit;

namespace Greeter.Modules.Message
{
    public partial class RecentMessageViewController : BaseViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        UITableView recentMessageTableView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupNavigationItem();
            RegisterCell();
            RegisterObserver();
            _ = GetRecentChatsAsync();

            //Setup Delegate and DataSource
            recentMessageTableView.WeakDelegate = this;
            recentMessageTableView.WeakDataSource = this;
        }

        void SetupView()
        {
            recentMessageTableView = new UITableView(CGRect.Empty);
            recentMessageTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            recentMessageTableView.RowHeight = 70;
            recentMessageTableView.SeparatorInsetReference = UITableViewSeparatorInsetReference.CellEdges;
            recentMessageTableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
            recentMessageTableView.TableFooterView = new UIView();
            View.Add(recentMessageTableView);

            recentMessageTableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            recentMessageTableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            recentMessageTableView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            recentMessageTableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        void SetupNavigationItem()
        {
            Title = "Recent Chats";
        }

        void RegisterCell()
        {
            recentMessageTableView.RegisterClassForCellReuse(typeof(RecentMessageCell), RecentMessageCell.Key);
        }

        void RegisterObserver()
        {
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("com.strive.greeter.update_recent"), notify: async (notification) => { await GetRecentChatsAsync(); });
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return recentMessageHistory.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(RecentMessageCell.Key) as RecentMessageCell;
            cell.SetupData(recentMessageHistory[indexPath.Row]);
            return cell;
        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var recentChat = recentMessageHistory[indexPath.Row];
            var chatType = recentChat.IsGroup ? ChatType.Group : ChatType.Indivisual;
            var chatInfo = new ChatInfo
            {
                Title = $"{recentChat.FirstName} {recentChat.LastName}",
                GroupId = recentChat.ID,
                SenderId = AppSettings.UserID,
                RecipientId = recentChat.ID,
                CommunicationId = recentChat.CommunicationID
            };

            if (chatType == ChatType.Group)
            {
                chatInfo.SenderId = chatInfo.RecipientId = 0;
            }

            NavigationController.PushViewController(new ChatViewController(chatType, chatInfo), animated: true);
        }

        void RefreshRecentChat()
        {
            if (IsViewLoaded)
                recentMessageTableView.ReloadData();
        }

        ~RecentMessageViewController() => NSNotificationCenter.DefaultCenter.RemoveObserver(this);
    }
}