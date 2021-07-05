using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Network;
using UIKit;

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
            _ = GetData();

            //Setup Delegate and DataSource
            checkoutTableView.WeakDelegate = this;
            checkoutTableView.WeakDataSource = this;
        }

        async Task GetData()
        {
            var req = new CheckoutRequest();
            req.StartDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            req.EndDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            req.LocationID = AppSettings.LocationID;
            req.SortBy = "TicketNumber";
            req.SortOrder = "ASC";
            req.Status = true;

            ShowActivityIndicator();
            var response = await new ApiService(new NetworkService()).GetCheckoutList(req);
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                Checkouts = response.CheckinVehicleDetails.CheckOutList;
                checkoutTableView.ReloadData();
            }
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
            cell.SetupData(Checkouts[indexPath.Row]);
            return cell;
        }
    }
}