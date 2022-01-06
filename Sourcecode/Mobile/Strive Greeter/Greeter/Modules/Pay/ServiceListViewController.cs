using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Modules.Pay
{
    public partial class ServiceListViewController : BaseViewController, IUITableViewDataSource, IUITableViewDelegate, IUISearchBarDelegate
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
                await GetCheckoutListFromApiAsync();
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
            searchBar.SearchTextField.KeyboardType = UIKeyboardType.NumberPad;
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

        void RegisterCells()
        {
            checkoutTableView.RegisterClassForCellReuse(typeof(CheckoutCell), CheckoutCell.Key);
        }

        void SetupNavigationItem()
        {
            Title = "Pay";
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
            cell.SetupData(FilteredCheckouts[indexPath.Row], true, PayBtnClicked);
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

            var contextualActions = new List<UIContextualAction>() { actionPrint };

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
