using System;
using System.Runtime.Remoting.Channels;
using CoreGraphics;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectService : UIViewController
    {
        ScheduleServicesViewModel ViewModel = new ScheduleServicesViewModel();
        public Schedule_SelectService() : base("Schedule_SelectService", null)
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
                if (this.ViewModel.checkSelectedService())
                {
                    ViewModel.NavToSelect_Loc();
                }                
            };

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Select Service";           

            SelectService_View.Layer.CornerRadius = 5;
            SelectService_TableView.Layer.CornerRadius = 5;
            Cancel_SelectServiceBtn.Layer.CornerRadius = 5;

            SelectService_TableView.RegisterNibForCellReuse(Schedule_SelectService_Cell.Nib, Schedule_SelectService_Cell.Key);
            SelectService_TableView.ReloadData();

            getSelectServices();
        }

        private async void getSelectServices()
        {
            await ViewModel.GetScheduledServices();

            if(this.ViewModel.scheduleServices != null && this.ViewModel.scheduleServices.AllServiceDetail.Count > 0)
            {
                var selectServiceSource = new Schedule_SelectService_Source(this.ViewModel);
                SelectService_TableView.Source = selectServiceSource;
                SelectService_TableView.TableFooterView = new UIView(CGRect.Empty);
                SelectService_TableView.DelaysContentTouches = false;
                SelectService_TableView.ReloadData();
            }           
        }

        partial void CancelBtn_Service (UIButton sender)
        {
            ViewModel.NavToSchedule();
        }
    }
}

