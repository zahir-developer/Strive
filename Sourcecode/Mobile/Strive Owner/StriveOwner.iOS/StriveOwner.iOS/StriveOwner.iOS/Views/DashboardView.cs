using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views
{
    public partial class DashboardView : MvxViewController<DashboardViewModel>
    {
        public DashboardView() : base("DashboardView", null)
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

