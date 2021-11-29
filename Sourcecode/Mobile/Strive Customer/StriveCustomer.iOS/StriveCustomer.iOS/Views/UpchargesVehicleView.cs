using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using UIKit;
using StriveCustomer.iOS.UIUtils;

namespace StriveCustomer.iOS.Views
{
    public partial class UpchargesVehicleView : MvxViewController<VehicleUpchargeViewModel>
    {
        public UpchargesVehicleView() : base("UpchargesVehicleView", null)
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
            var rightBtn = new UIButton(UIButtonType.Custom);
            rightBtn.SetTitle("Next", UIControlState.Normal);
            rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var rightBarBtn = new UIBarButtonItem(rightBtn);
            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
            rightBtn.TouchUpInside += (sender, e) =>
            {
                if (ViewModel.VehicleUpchargeCheck())
                {
                    ViewModel.NavToAdditionalServices();
                }
            };

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Vehicle";

            UpchargesVehicle_TableView.UserInteractionEnabled = false;
            UpchargesVehicle_TableView.Layer.CornerRadius = 5;
            UpchargesVehicle_TableView.RegisterNibForCellReuse(MembershipVehicle_ViewCell.Nib, MembershipVehicle_ViewCell.Key);
            UpchargesVehicle_TableView.ReloadData();

            getUpchargeList();
        }

        private async void getUpchargeList()
        {
            await this.ViewModel.getServiceList(MembershipDetails.selectedMembership);
            await this.ViewModel.getAllServiceList();

            if (MembershipDetails.filteredList != null)
            {
                var source = new UpchargesVehicleDataSource(ViewModel.upchargeFullList);
                UpchargesVehicle_TableView.Source = source;
                UpchargesVehicle_TableView.TableFooterView = new UIView(CGRect.Empty);
                UpchargesVehicle_TableView.DelaysContentTouches = false;
                UpchargesVehicle_TableView.ReloadData();
            }
        }
    }
}

