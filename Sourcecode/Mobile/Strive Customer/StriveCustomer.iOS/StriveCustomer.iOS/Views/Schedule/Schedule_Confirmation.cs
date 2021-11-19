using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_Confirmation : MvxViewController<ScheduleConfirmationViewModel>
    {
        public Schedule_Confirmation() : base("Schedule_Confirmation", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            this.NavigationItem.SetHidesBackButton(true, false);

            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        public void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Confirmation";

            ConfirmSchedule_View.Layer.CornerRadius = 5;
            ScheduleDate_Lbl.Text = CustomerScheduleInformation.ScheduleDate + " " + CustomerScheduleInformation.ScheduleMonth + " "
                + CustomerScheduleInformation.ScheduleYear + " | ";
            ScheduleTime_Lbl.Text = CustomerScheduleInformation.ScheduleServiceTime;
            ScheduleVehicleName_Lbl.Text = CustomerScheduleInformation.ScheduledVehicleName;
        }

        partial void BackDashboard_BtnTouch(UIButton sender)
        {
            this.ViewModel.ClearScheduleData();
            NavToSchedule();
        }

        public void NavToSchedule()
        {
            ViewModel.NavtoSchedule();
        }
    }
}

