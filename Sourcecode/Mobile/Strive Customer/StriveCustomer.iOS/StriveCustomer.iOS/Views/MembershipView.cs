using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class MembershipView : MvxViewController<VehicleMembershipViewModel>
    {               
        public MembershipView() : base("MembershipView", null)
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
                if (ViewModel.VehicleMembershipCheck())
                {
                    ViewModel.NavToUpcharges();     
                }
            };

            VehicleMembership_TableView.Layer.CornerRadius = 5;
            VehicleMembership_TableView.RegisterNibForCellReuse(MembershipVehicle_ViewCell.Nib, MembershipVehicle_ViewCell.Key);
            VehicleMembership_TableView.ReloadData();

            getMembershipData();
        }

        private async void getMembershipData()
        {
            await this.ViewModel.getMembershipDetails();

            var membershipSource = new MembershipVehicleDataSource(this.ViewModel.membershipList);
            VehicleMembership_TableView.Source = membershipSource;
            VehicleMembership_TableView.TableFooterView = new UIView(CGRect.Empty);
            VehicleMembership_TableView.DelaysContentTouches = false;
            VehicleMembership_TableView.ReloadData();
        }
        
    }
}