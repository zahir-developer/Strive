using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectLoc_View : MvxViewController<ScheduleLocationsViewModel>
    {
        public MvxViewController view;
        public Schedule_SelectLoc_View() : base("Schedule_SelectLoc_View", null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetUp();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void InitialSetUp()
        {
            var rightBtn = new UIButton(UIButtonType.Custom);
            rightBtn.SetTitle("Next", UIControlState.Normal);
            rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var rightBarBtn = new UIBarButtonItem(rightBtn);
            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
            rightBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.NavToSelect_Service();
            };

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Select Location";

            SelectLocation_View.Layer.CornerRadius = 5;
            SelectLoc_TableView.Layer.CornerRadius = 5;
            SelectLoc_CancelBtn.Layer.CornerRadius = 5;

            SelectLoc_TableView.RegisterNibForCellReuse(Schedule_Location_Cell.Nib, Schedule_Location_Cell.Key);
            SelectLoc_TableView.ReloadData();
            getLocations();
        }

        public async void getLocations()
        {
            await this.ViewModel.GetAllLocationsCommand();

            var locationSource = new ScheduleLocation_Source(this.ViewModel);
            SelectLoc_TableView.Source = locationSource;
            SelectLoc_TableView.TableFooterView = new UIView(CGRect.Empty);
            SelectLoc_TableView.DelaysContentTouches = false;
            SelectLoc_TableView.ReloadData();
        }

        partial void CancelLoc_BtnTouch(UIButton sender)
        {
            ViewModel.NavToSchedule();
        }
    }
}

