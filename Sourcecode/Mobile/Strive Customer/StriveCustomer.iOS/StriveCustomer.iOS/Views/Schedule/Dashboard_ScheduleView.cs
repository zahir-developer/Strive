using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using StriveCustomer.iOS.Views.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class Dashboard_ScheduleView : MvxViewController<ScheduleViewModel>
    {
        public Dashboard_ScheduleView() : base("Dashboard_ScheduleView", null)
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
            NavigationItem.Title = "Schedule";

            Schedule_ParentView.Layer.CornerRadius = 5;
            Schedule_Seg1.Layer.CornerRadius = 5;
            ScheduleVehicle_TableView.RegisterNibForCellReuse(DB_VehicleList_Cell.Nib, DB_VehicleList_Cell.Key);
            ScheduleVehicle_TableView.ReloadData();
            ScheduleVehicle_TableView.Layer.CornerRadius = 5;
            getVehicleList();
        }

        partial void Schedule_SegTouch(UISegmentedControl sender)
        {
            var index = Schedule_SegmentView.SelectedSegment;

            if(index == 0)
            {
                getVehicleList();
            }
            else if(index == 1)
            {
                ScheduleVehicle_TableView.RegisterNibForCellReuse(DB_PastHistory_Cell.Nib, DB_PastHistory_Cell.Key);
                ScheduleVehicle_TableView.ReloadData();
                ScheduleVehicle_TableView.Layer.CornerRadius = 5;
            }
        }

        private async void getVehicleList()
        {
            await this.ViewModel.GetScheduleVehicleList();

            if(!(this.ViewModel.scheduleVehicleList.Status.Count == 0) || !(this.ViewModel.scheduleVehicleList == null))
            {
                var ScheduleVehicleSource = new ScheduleVehcileListSource(this.ViewModel);
                ScheduleVehicle_TableView.Source = ScheduleVehicleSource;
                ScheduleVehicle_TableView.TableFooterView = new UIView(CGRect.Empty);
                ScheduleVehicle_TableView.DelaysContentTouches = false;
                ScheduleVehicle_TableView.ReloadData();
            }
        }
    }
}

