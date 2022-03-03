using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Newtonsoft.Json;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using StriveCustomer.iOS.Views.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class Dashboard_ScheduleView : MvxViewController<ScheduleViewModel>
    {
        DB_PastHistory_Cell dB;
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

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            Schedule_ParentView.Layer.CornerRadius = 5;
            Schedule_Seg1.Layer.CornerRadius = 5;
            ScheduleVehicle_TableView.Hidden = false;
            SchedulePastHis_TableView.Hidden = true;
            WashHistory_TableView.Hidden = true;    
            ScheduleVehicle_TableView.RegisterNibForCellReuse(DB_VehicleList_Cell.Nib, DB_VehicleList_Cell.Key);
            ScheduleVehicle_TableView.ReloadData();
            ScheduleVehicle_TableView.Layer.CornerRadius = 5;
            getVehicleList();

            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("com.strive.employee.Pay"), notify: (notification) =>
            {
                if (notification.UserInfo is null)
                    return;
                var Price = notification.UserInfo["Price"] as NSString;
                //var ID = JsonConvert.DeserializeObject<string>("Price");
                InvokeOnMainThread(() =>
                {
                    GetActionSheet(float.Parse(Price));
                });
            });
        }

        public override void ViewWillAppear(bool animated)
        {
            CustomerScheduleInformation.ClearScheduleData();
            InitialSetup();
            NavigationItem.HidesBackButton = true;
        }

        partial void Schedule_SegTouch(UISegmentedControl sender)
        {
            var index = Schedule_SegmentView.SelectedSegment;

            if (index == 0)
            {
                ScheduleVehicle_TableView.Hidden = false;
                SchedulePastHis_TableView.Hidden = true;
                WashHistory_TableView.Hidden = true;
                getVehicleList();
            }
            else if (index == 1)
            {
                SchedulePastHis_TableView.Hidden = false;
                WashHistory_TableView.Hidden = true;
                ScheduleVehicle_TableView.Hidden = true;
                SchedulePastHis_TableView.RegisterNibForCellReuse(DB_PastHistory_Cell.Nib, DB_PastHistory_Cell.Key);
                SchedulePastHis_TableView.ReloadData();
                SchedulePastHis_TableView.Layer.CornerRadius = 5;

                getPastServiceDetails();
            }
            else if (index == 2)
            {
                SchedulePastHis_TableView.Hidden = true;
                ScheduleVehicle_TableView.Hidden = true;
                WashHistory_TableView.Hidden = false;

            }
        }

        private async void getVehicleList()
        {
            await this.ViewModel.GetScheduleVehicleList();

            if(!(this.ViewModel.scheduleVehicleList.Status.Count == 0) || !(this.ViewModel.scheduleVehicleList == null))
            {             
                var ScheduleVehicleSource = new ScheduleVehcileListSource(this, this.ViewModel);
                ScheduleVehicle_TableView.Source = ScheduleVehicleSource;
                ScheduleVehicle_TableView.TableFooterView = new UIView(CGRect.Empty);
                ScheduleVehicle_TableView.DelaysContentTouches = false;
                ScheduleVehicle_TableView.ReloadData();
            }
        }

        private async void getPastServiceDetails()
        {
            
            await this.ViewModel.GetPastServiceDetails();

            if(this.ViewModel.pastServiceHistory != null)
            {
                if(this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel != null) {
                    if (this.ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.Count > 0)
                    {
                        var pastHis_Source = new Schedule_PastHis_Source(this.ViewModel);
                        SchedulePastHis_TableView.Source = pastHis_Source;
                        SchedulePastHis_TableView.TableFooterView = new UIView(CGRect.Empty);
                        SchedulePastHis_TableView.DelaysContentTouches = false;
                        SchedulePastHis_TableView.RowHeight = UITableView.AutomaticDimension;
                        SchedulePastHis_TableView.EstimatedRowHeight = 90;
                        SchedulePastHis_TableView.ReloadData();
                    }
                }                
            }            
        }
        public  void GetActionSheet(float price)
        {
            double TenPercent = (10 * price)/100;
            double FifteenPercent = (15 * price) / 100;
            double TwentyPercent = (20 * price) / 100;
            double TwentyFivePercent = (25 * price) / 100;

            var alert = UIAlertController.Create("Add Tip", "", UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("$"+ TenPercent.ToString("0.00"), UIAlertActionStyle.Destructive, (action) => {
                ViewModel.WashTip = TenPercent;
                ViewModel.TipPayment();
            }));
            alert.AddAction(UIAlertAction.Create("$" + FifteenPercent.ToString("0.00"), UIAlertActionStyle.Destructive, (action) => {
                ViewModel.WashTip = FifteenPercent;
                ViewModel.TipPayment();
            }));
            alert.AddAction(UIAlertAction.Create("$" + TwentyPercent.ToString("0.00"), UIAlertActionStyle.Destructive, (action) => {
                ViewModel.WashTip = TwentyPercent;
                ViewModel.TipPayment();
            }));
            alert.AddAction(UIAlertAction.Create("$" + TwentyFivePercent.ToString("0.00"), UIAlertActionStyle.Destructive, (action) => {
                ViewModel.WashTip = TwentyFivePercent;
                ViewModel.TipPayment();  
            }));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
            this.PresentViewController(alert,true,null);
        }
    }
}

