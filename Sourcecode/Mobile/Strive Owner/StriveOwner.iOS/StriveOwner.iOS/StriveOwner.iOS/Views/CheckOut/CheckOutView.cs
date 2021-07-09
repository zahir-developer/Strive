using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using StriveOwner.iOS.UIUtils;
using UIKit;

namespace StriveOwner.iOS.Views.CheckOut
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
                    var checkoutSource = new CheckOut_DataSource(ViewModel.CheckOutVehicleDetails);
                    CheckOut_TableView.Source = checkoutSource;
                    CheckOut_TableView.TableFooterView = new UIView(CGRect.Empty);
                    CheckOut_TableView.DelaysContentTouches = false;
                    CheckOut_TableView.ReloadData();
                }
            }
        }
    }
}

