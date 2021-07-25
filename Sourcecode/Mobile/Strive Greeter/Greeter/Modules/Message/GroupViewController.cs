using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using Greeter.DTOs;
using UIKit;

namespace Greeter.Modules.Message
{
    public partial class GroupViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate, IUITextFieldDelegate
    {
        UITableView messageGroupsTableView;
        readonly UIRefreshControl refreshControl = new();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupNavigationItem();
            RegisterCell();

            //Setup Delegate and DataSource
            messageGroupsTableView.WeakDelegate = this;
            messageGroupsTableView.WeakDataSource = this;
        }

        void SetupView()
        {
            View.BackgroundColor = UIColor.White;

            var searchContainerView = new UIView(CGRect.Empty);
            searchContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            searchContainerView.Layer.BorderColor = UIColor.LightGray.CGColor;
            searchContainerView.Layer.BorderWidth = 2;
            View.Add(searchContainerView);

            var searchTextField = new UITextField(CGRect.Empty);
            searchTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            searchTextField.Placeholder = "Search Group";
            searchTextField.WeakDelegate = this;
            searchContainerView.Add(searchTextField);

            var searchImageView = new UIImageView(CGRect.Empty);
            searchImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            searchImageView.Image = UIImage.FromBundle(ImageNames.SEARCH);
            searchContainerView.Add(searchImageView);

            refreshControl.AddTarget((sender, e) => { OnRefersh().ConfigureAwait(false); }, UIControlEvent.ValueChanged);

            messageGroupsTableView = new UITableView(CGRect.Empty);
            messageGroupsTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            messageGroupsTableView.RowHeight = 70;
            messageGroupsTableView.SeparatorInsetReference = UITableViewSeparatorInsetReference.CellEdges;
            messageGroupsTableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
            messageGroupsTableView.TableFooterView = new UIView();
            messageGroupsTableView.RefreshControl = refreshControl;
            View.Add(messageGroupsTableView);

            searchContainerView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, constant: 20).Active = true;
            searchContainerView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, constant: -20).Active = true;
            searchContainerView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, constant: 20).Active = true;
            searchContainerView.HeightAnchor.ConstraintEqualTo(50).Active = true;

            searchTextField.LeadingAnchor.ConstraintEqualTo(searchContainerView.LeadingAnchor, constant: 20).Active = true;
            searchTextField.TrailingAnchor.ConstraintEqualTo(searchImageView.LeadingAnchor, constant: -20).Active = true;
            searchTextField.TopAnchor.ConstraintEqualTo(searchContainerView.TopAnchor).Active = true;
            searchTextField.BottomAnchor.ConstraintEqualTo(searchContainerView.BottomAnchor).Active = true;

            searchImageView.TrailingAnchor.ConstraintEqualTo(searchContainerView.TrailingAnchor, constant: -20).Active = true;
            searchImageView.CenterYAnchor.ConstraintEqualTo(searchContainerView.CenterYAnchor).Active = true;
            searchImageView.WidthAnchor.ConstraintEqualTo(25).Active = true;
            searchImageView.HeightAnchor.ConstraintEqualTo(25).Active = true;

            messageGroupsTableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            messageGroupsTableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            messageGroupsTableView.TopAnchor.ConstraintEqualTo(searchContainerView.BottomAnchor, constant: 10).Active = true;
            messageGroupsTableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        void SetupNavigationItem()
        {
            Title = "Groups";

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle(ImageNames.ADD_CIRCLE), UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {
                NavigationController.PushViewController(new GroupParticipantsViewController(true), true);
            });
        }

        void RegisterCell()
        {
            messageGroupsTableView.RegisterClassForCellReuse(typeof(MessageGroupCell), MessageGroupCell.Key);
        }

        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            var oldNSString = new NSString(textField.Text ?? "");
            var replacedString = oldNSString.Replace(range, new NSString(replacementString));

            //SearchGroup(replacedString).ConfigureAwait(false);
            //RefreshGroupsToUI(); // refreshing issue bcoz of not awaiting to store searched contacts

            //Task.Run(async () =>
            //{
            //    await SearchGroup(replacedString);
            //    RefreshGroupsToUI(); // Leads to not access ui elements outside of main thread
            //});

            SearchGroup(replacedString).ConfigureAwait(false);
            return true;
        }

        void RefreshGroupsToUI()
        {
            if (IsViewLoaded)
                messageGroupsTableView.ReloadData();
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return searchedGroups?.Count ?? 0;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(MessageGroupCell.Key) as MessageGroupCell;
            cell.SetupData(searchedGroups[indexPath.Row]);
            return cell;
        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var group = searchedGroups[indexPath.Row];
            var chatInfo = new ChatInfo
            {
                Title = group,
                GroupId = -1,
                SenderId = -1,
                RecipientId = -1
            };
            NavigationController.PushViewController(new ChatViewController(ChatType.Group, chatInfo), animated: true);
        }
    }
}