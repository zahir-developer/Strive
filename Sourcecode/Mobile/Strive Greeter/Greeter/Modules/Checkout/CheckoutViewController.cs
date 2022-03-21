using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Modules.Pay
{
    public partial class CheckoutViewController : BaseViewController, IUITableViewDataSource, IUITableViewDelegate, IUISearchBarDelegate
    {
        UITableView checkoutTableView;
        UISearchBar searchBar;
        readonly UIRefreshControl refreshControl = new();
        bool isAlreadyLoaded;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            RegisterCells();
            SetupNavigationItem();

            //Setup Delegate and DataSource
            checkoutTableView.WeakDelegate = this;
            checkoutTableView.WeakDataSource = this;

            refreshControl.ValueChanged += async (sender, e) =>
            {
                Checkouts = await GetCheckoutListFromApiAsync();
                if (!string.IsNullOrWhiteSpace(searchBar.Text))
                {
                    dsa(searchBar.Text);
                }
                else
                {
                    FilteredCheckouts = Checkouts;
                    checkoutTableView.ReloadData();
                }
                refreshControl.EndRefreshing();
            };
        }

        public override void ViewWillAppear(bool animated)
        {
            if (!isAlreadyLoaded)
            {
                var height = NavigationController.NavigationBar.Bounds.Height;
                var oldContentInset = checkoutTableView.ContentInset;
                checkoutTableView.ContentInset = new UIEdgeInsets(oldContentInset.Top + height, oldContentInset.Left, oldContentInset.Bottom, oldContentInset.Right);
                isAlreadyLoaded = true;
            }
            base.ViewWillAppear(animated);

            GetCheckoutListAsync().ConfigureAwait(false);
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

            searchBar = new UISearchBar(CGRect.Empty);
            searchBar.TranslatesAutoresizingMaskIntoConstraints = false;
            searchBar.SearchBarStyle = UISearchBarStyle.Default;
            searchBar.BackgroundColor = UIColor.Clear;
            //searchBar.SearchTextField.Subviews.FirstOrDefault().BackgroundColor = UIColor.Clear;

            //searchBar.TintColor = UIColor.Clear;
            searchBar.BackgroundImage = new UIImage();
            searchBar.Placeholder = " Search Ticket ID";
            searchBar.SizeToFit();
            searchBar.Translucent = false;
            searchBar.Delegate = this;
            searchBar.SearchTextField.KeyboardType = UIKeyboardType.Default;
            //searchBar.CancelButtonClicked += SearchBar_CancelButtonClicked;
            //NavigationItem.TitleView = searchBar;
            View.Add(searchBar);

            checkoutTableView = new UITableView(CGRect.Empty);
            checkoutTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            checkoutTableView.BackgroundColor = UIColor.Clear;
            checkoutTableView.AutomaticallyAdjustsScrollIndicatorInsets = true;
            refreshControl.TintColor = ColorConverters.FromHex(Common.Colors.APP_BASE_COLOR).ToPlatformColor();
            checkoutTableView.RefreshControl = refreshControl;
            View.Add(checkoutTableView);

            searchBar.LeadingAnchor.ConstraintEqualTo(checkoutTableView.LeadingAnchor, constant: 0).Active = true;
            searchBar.TrailingAnchor.ConstraintEqualTo(checkoutTableView.TrailingAnchor, constant: 0).Active = true;
            searchBar.TopAnchor.ConstraintEqualTo(View.TopAnchor, constant: 40).Active = true;
            searchBar.HeightAnchor.ConstraintEqualTo(60).Active = true;

            checkoutTableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, constant: 60).Active = true;
            checkoutTableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, constant: -60).Active = true;
            checkoutTableView.TopAnchor.ConstraintEqualTo(searchBar.TopAnchor, constant: 40).Active = true;
            checkoutTableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, constant: -40).Active = true;
        }

        //private void SearchBar_CancelButtonClicked(object sender, EventArgs e)
        //{
        //    searchBar.Text = string.Empty;
        //    FilteredCheckouts = Checkouts;
        //    checkoutTableView.ReloadData();
        //}

        void dsa(string text)
        {
            if (string.IsNullOrWhiteSpace(text.Trim()))
            {
                FilteredCheckouts = Checkouts;
                checkoutTableView.ReloadData();
                return;
            }

            FilteredCheckouts = FilterCheckout(text, Checkouts);
            if (!FilteredCheckouts.IsNullOrEmpty())
            {
                checkoutTableView.ReloadData();
            }
        }

        List<Checkout> FilterCheckout(string ticketId, List<Checkout> checkouts)
        {
            List<Checkout> filteredCheckouts = null;
            filteredCheckouts = checkouts.Where(x => x.TicketNumber.ToString().Contains(ticketId)).ToList();
            return filteredCheckouts;
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
            return FilteredCheckouts == null ? 0 : FilteredCheckouts.Count;
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

            cell.SetupData(FilteredCheckouts[indexPath.Row]);
            return cell;
        }

        [Export("tableView:trailingSwipeActionsConfigurationForRowAtIndexPath:")]
        public UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var row = (int)indexPath.Row;
            var checkout = FilteredCheckouts[row];

            var actionPrint = UIContextualAction.FromContextualActionStyle(
             UIContextualActionStyle.Normal,
             "Print",
             (flagAction, view, success) =>
             {
                 //success(true);
                 tableView.Editing = false;
                 PrintReceipt(checkout);
             });

            actionPrint.Image = UIImage.FromBundle("tick");
            actionPrint.BackgroundColor = ColorConverters.FromHex(Common.Colors.PRINT_COLOR).ToPlatformColor();

            var action1 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Hold",
                (flagAction, view, success) =>
                {
                    //success(true);
                    tableView.Editing = false;
                    HoldBtnClicked(FilteredCheckouts[indexPath.Row]);
                });
            action1.Image = UIImage.FromBundle("tick");
            action1.BackgroundColor = ColorConverters.FromHex("#ff9d00").ToPlatformColor();

            var contextualActions = new List<UIContextualAction>() { actionPrint, action1 };

            if (!checkout.JobStatus.Equals("Completed"))
            {
                var action2 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Complete",
                (flagAction, view, success) =>
                {
                    //success(true);
                    tableView.Editing = false;
                    CompleteBtnClicked(FilteredCheckouts[indexPath.Row]);
                });

                action2.Image = UIImage.FromBundle(ImageNames.TICK);
                //action2.Image.ApplyTintColor(UIColor.White);
                action2.BackgroundColor = ColorConverters.FromHex("#138a32").ToPlatformColor();
                contextualActions.Add(action2);
            }

            var action3 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Checkout",
                (flagAction, view, success) =>
                {
                    //success(true);
                    tableView.Editing = false;
                    CheckoutBtnClicked(FilteredCheckouts[indexPath.Row]);
                });
            action3.Image = UIImage.FromBundle(ImageNames.TICK);
            //action3.Image.ApplyTintColor(UIColor.White);
            action3.BackgroundColor = ColorConverters.FromHex(Common.Colors.APP_BASE_COLOR).ToPlatformColor();
            contextualActions.Add(action3);

            return UISwipeActionsConfiguration.FromActions(contextualActions.ToArray());
        }

        [Export("searchBar:textDidChange:")]
        public void TextChanged(UISearchBar searchBar, string searchText)
        {
            dsa(searchText);
        }

        [Export("searchBarCancelButtonClicked:")]
        public void CancelButtonClicked(UISearchBar searchBar)
        {
            searchBar.Text = string.Empty;
            FilteredCheckouts = Checkouts;
            checkoutTableView.ReloadData();
        }
    }
}