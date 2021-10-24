using System;
using System.Collections.Generic;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using Greeter.DTOs;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Modules.Pay
{
    public partial class ServiceListViewController : BaseViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        UITableView checkoutTableView;
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

            checkoutTableView = new UITableView(CGRect.Empty);
            checkoutTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            checkoutTableView.BackgroundColor = UIColor.Clear;
            checkoutTableView.AutomaticallyAdjustsScrollIndicatorInsets = true;
            refreshControl.TintColor = Colors.APP_BASE_COLOR.ToPlatformColor();
            checkoutTableView.RefreshControl = refreshControl;
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
            Title = "Pay";
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
            cell.SetupData(Checkouts[indexPath.Row], true, PayBtnClicked);
            return cell;
        }

        [Export("tableView:trailingSwipeActionsConfigurationForRowAtIndexPath:")]
        public UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var row = (int)indexPath.Row;
            var checkout = Checkouts[row];

            var action1 = UIContextualAction.FromContextualActionStyle(
              UIContextualActionStyle.Normal,
              "Print",
              (flagAction, view, success) =>
              {
                  //success(true);
                  tableView.Editing = false;
                  PrintReceipt(checkout);
              });
            //action1.Image = UIImage.FromBundle("tick");
            action1.BackgroundColor = Colors.APP_BASE_COLOR.ToPlatformColor();

            var contextualActions = new List<UIContextualAction>() { action1 };

            return UISwipeActionsConfiguration.FromActions(contextualActions.ToArray());
        }

        void PrintReceipt(Checkout checkout)
        {
            string printContentHtml = MakeServiceReceipt(checkout);
            Print(printContentHtml);
        }

        string MakeServiceReceipt(Checkout checkout)
        {
            var body = "<p>Ticket Number : </p>" + checkout.ID + "<br /><br />";

            if (!string.IsNullOrEmpty(checkout.CustomerFirstName))
            {
                body += "<p>Customer Details : </p>" + ""
                    + "<p>Customer Name - " + checkout.CustomerFirstName + " " + checkout.CustomerLastName + "</p><br />";
            }

            body += "<p>Vehicle Details : </p>" +
                 "<p>Make - " + checkout.VehicleMake + "</p>" +
                "<p>Model - " + checkout.VehicleModel + "</p>" +
                 "<p>Color - " + checkout.VehicleColor + "</p><br />" +
                 "<p>Services : " + "</p>";

            if (!string.IsNullOrEmpty(checkout.Services))
            {
                body += "<p>" + checkout.Services + "</p>";
            }

            if (!string.IsNullOrEmpty(checkout.AdditionalServices) && !checkout.AdditionalServices.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                body += "<p>" + checkout.AdditionalServices + "</p>";
            }

            body += "<br/ ><p>" + "Total Amount Due: " + "$" + checkout.Cost.ToString() + "</p>";

            body += "<br/ ><p>Note: Please avoid if you already paid.</p>";

            Debug.WriteLine("Email Body :" + body);

            return body;
        }
    }
}
