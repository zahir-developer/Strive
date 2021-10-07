﻿using System;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
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
    }
}
