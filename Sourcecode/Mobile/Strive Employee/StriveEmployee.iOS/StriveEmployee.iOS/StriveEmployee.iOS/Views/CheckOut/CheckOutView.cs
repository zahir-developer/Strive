﻿using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Employee.CheckOut;
using StriveEmployee.iOS.UIUtils;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public partial class CheckOutView : MvxViewController<CheckOutViewModel>
    {
        public CheckOutView() : base("CheckOutView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        private void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "CheckOut";

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            CheckOut_TableView.Layer.CornerRadius = 5;
            CheckOut_View.Layer.CornerRadius = 5;

            CheckOut_TableView.RegisterNibForCellReuse(CheckOut_Cell.Nib, CheckOut_Cell.Key);
            CheckOut_TableView.BackgroundColor = UIColor.Clear;
            CheckOut_TableView.ReloadData();

            GetCheckoutDetails();
        }
        private async void GetCheckoutDetails()
        {
            await ViewModel.GetCheckOutDetails();
            if (ViewModel.CheckOutVehicleDetails != null)
            {
                if (ViewModel.CheckOutVehicleDetails != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails != null
                    || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count > 0)
                {                                                 
                    var documentSource = new Checkout_DataSource(ViewModel.CheckOutVehicleDetails);
                    CheckOut_TableView.Source = documentSource;
                    CheckOut_TableView.TableFooterView = new UIView(CGRect.Empty);
                    CheckOut_TableView.DelaysContentTouches = false;
                    CheckOut_TableView.ReloadData();
                }
            }
        }
    }
}
