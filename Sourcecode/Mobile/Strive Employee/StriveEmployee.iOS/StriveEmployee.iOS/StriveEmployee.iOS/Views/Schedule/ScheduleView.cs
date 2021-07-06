using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Employee.Schedule;
using UIKit;

namespace StriveEmployee.iOS.Views.Schedule
{
    public partial class ScheduleView : MvxViewController<ScheduleViewModel>
    {
        public ScheduleView() : base("ScheduleView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

