using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;
using StriveCustomer.iOS.UIUtils;

namespace StriveCustomer.iOS.Views
{
    public partial class AdditionalServicesVehicleView : MvxViewController<VehicleAdditionalServiceViewModel>
    {
        public AdditionalServicesVehicleView() : base("AdditionalServicesVehicleView", null)
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
                ViewModel.NavToSignatureView();
            };

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Vehicle";

            AdditionalServicesTableView.Layer.CornerRadius = 5;
            AdditionalServicesTableView.RegisterNibForCellReuse(MembershipVehicle_ViewCell.Nib, MembershipVehicle_ViewCell.Key);
            AdditionalServicesTableView.ReloadData();

            getAdditionalServices();           
            
        }

        private async void getAdditionalServices()
        {
            await this.ViewModel.AddUpchargesToServiceList();

            var source = new AdditionalServicesDataSource(ViewModel.serviceList);
            AdditionalServicesTableView.Source = source;
            AdditionalServicesTableView.TableFooterView = new UIView(CGRect.Empty);
            AdditionalServicesTableView.DelaysContentTouches = false;
            AdditionalServicesTableView.ReloadData();
        }
    }
}