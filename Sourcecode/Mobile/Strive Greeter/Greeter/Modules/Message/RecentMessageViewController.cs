using System;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using UIKit;

namespace Greeter.Modules.Message
{
    public partial class RecentMessageViewController: UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        UITableView recentMessageTableView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupNavigationItem();
            RegisterCell();

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

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle(ImageNames.ADD_CIRCLE), UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {

            });
        }

        void RegisterCell()
        {
            recentMessageTableView.RegisterClassForCellReuse(typeof(RecentMessageCell), RecentMessageCell.Key);
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return recentMessageHistory.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(RecentMessageCell.Key) as RecentMessageCell;
            cell.SetupData();
            return cell;
        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            NavigationController.PushViewController(new ChatViewController(), animated: true);

        }
    }
}
