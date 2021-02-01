using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_Preview : MvxViewController<SchedulePreviewDetailsViewModel>
    {
        public Schedule_Preview() : base("Schedule_Preview", null)
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

        public void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Preview Details";

            PreviewApp_ParentView.Layer.CornerRadius = 5;
            PreviewDetail_View1.Layer.CornerRadius = 5;
            PreviewDetail_View2.Layer.CornerRadius = 5;
            PreviewDetail_View3.Layer.CornerRadius = 5;
            Cancel_BtnPreview.Layer.CornerRadius = 5;
            BookNow_PreviewBtn.Layer.CornerRadius = 5;

            PreviewServiceLocation_Lbl.Text = CustomerScheduleInformation.ScheduleLocationAddress;
            PreviewServiceName_Lbl.Text = CustomerScheduleInformation.ScheduleServiceName;
            PreviewVehicleName_Lbl.Text = CustomerScheduleInformation.ScheduledVehicleName;
            Appointment_PreviewTime_Lbl.Text = CustomerScheduleInformation.ScheduleServiceTime;
            Appointment_PreviewDate_Lbl.Text = CustomerScheduleInformation.ScheduleDate + " " +
                CustomerScheduleInformation.ScheduleMonth + " " + CustomerScheduleInformation.ScheduleYear + " | ";
        }

        partial void BookNow_PreviewTouch(UIButton sender)
        {
            NavtoConfirm();
        }

        partial void CancelPreview_Touch(UIButton sender)
        {
            ViewModel.NavtoScheduleDate();
        }

        partial void Reschedule_LblTouch(UIButton sender)
        {
            ViewModel.NavtoScheduleView();
        }

        public async void NavtoConfirm()
        {
            await this.ViewModel.BookNow();
            ViewModel.NavtoConfirmSchedule();
        }
    }
}

