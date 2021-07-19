using System;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Modules.Pay
{
    public partial class CheckoutViewController : BaseViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        UITableView checkoutTableView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            RegisterCells();
            SetupNavigationItem();

            //Setup Delegate and DataSource
            checkoutTableView.WeakDelegate = this;
            checkoutTableView.WeakDataSource = this;
        }

        public override void ViewWillAppear(bool animated)
        {
            var height = NavigationController.NavigationBar.Bounds.Height;
            var oldContentInset = checkoutTableView.ContentInset;
            checkoutTableView.ContentInset = new UIEdgeInsets(oldContentInset.Top + height, oldContentInset.Left, oldContentInset.Bottom, oldContentInset.Right);
            base.ViewWillAppear(animated);
        }

        void SetupView()
        {
            var backgroundImage = new UIImageView(UIImage.FromBundle(ImageNames.SPLASH_BG));
            backgroundImage.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundImage.ContentMode = UIViewContentMode.ScaleAspectFill;
            View.Add(backgroundImage);

            backgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            backgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            backgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            backgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            checkoutTableView = new UITableView(CGRect.Empty);
            checkoutTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            checkoutTableView.BackgroundColor = UIColor.Clear;
            checkoutTableView.AutomaticallyAdjustsScrollIndicatorInsets = true;
            View.Add(checkoutTableView);

            checkoutTableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, constant: 60).Active = true;
            checkoutTableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, constant: -60).Active = true;
            checkoutTableView.TopAnchor.ConstraintEqualTo(View.TopAnchor, constant: 40).Active = true;
            checkoutTableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, constant: -40).Active = true;
        }

        void RegisterCells()
        {
            checkoutTableView.RegisterClassForCellReuse(typeof(CheckoutCell), CheckoutCell.Key);
        }

        void SetupNavigationItem()
        {
            Title = "Checkout";
            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Next", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {
                //TODO navigate to next screen.
            });
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return Checkouts == null ? 0 : Checkouts.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CheckoutCell.Key) as CheckoutCell;

            var row = (int)indexPath.Row;
            var lastPos = Checkouts.Count - 1;
            if (row == lastPos - 1)
            {
                //_ = LoadItems(lastPagePos);
            }

            cell.SetupData(Checkouts[indexPath.Row]);
            return cell;
        }

        [Export("tableView:trailingSwipeActionsConfigurationForRowAtIndexPath:")]
        public UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var action1 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Hold",
                (flagAction, view, success) =>
                {
                    //success(true);
                    tableView.Editing = false;
                    HoldBtnClicked(Checkouts[indexPath.Row]);
                });
            action1.Image = UIImage.FromBundle("tick");
            action1.BackgroundColor = ColorConverters.FromHex("#ff9d00").ToPlatformColor();

            var action2 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Complete",
                (flagAction, view, success) =>
                {
                    //success(true);
                    tableView.Editing = false;
                    CompleteBtnClicked(Checkouts[indexPath.Row]);
                });

            action2.Image = UIImage.FromBundle(ImageNames.TICK);
            //action2.Image.ApplyTintColor(UIColor.White);
            action2.BackgroundColor = ColorConverters.FromHex("#138a32").ToPlatformColor();

            var action3 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Checkout",
                (flagAction, view, success) =>
                {
                    //success(true);
                    tableView.Editing = false;
                    CheckoutBtnClicked(Checkouts[indexPath.Row]);
                });
            action3.Image = UIImage.FromBundle(ImageNames.TICK);
            //action3.Image.ApplyTintColor(UIColor.White);
            action3.BackgroundColor = Colors.APP_BASE_COLOR.ToPlatformColor();

            return UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { action1, action2, action3 });
        }
    }
}