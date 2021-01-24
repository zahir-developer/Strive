using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectDate_View : MvxViewController<ScheduleAppointmentDateViewModel>
    {
        public Schedule_SelectDate_View() : base("Schedule_SelectDate_View", null)
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
            NavigationItem.Title = "Select Date";

            var rightBtn = new UIButton(UIButtonType.Custom);
            rightBtn.SetTitle("Next", UIControlState.Normal);
            rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var rightBarBtn = new UIBarButtonItem(rightBtn);
            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
            rightBtn.TouchUpInside += (sender, e) =>
            {
               
            };
        }
    }
}

