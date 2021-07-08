using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.CheckOut
{
    public partial class CheckOutView : MvxViewController<CheckOutViewModel>
    {
        public CheckOutView() : base("CheckOutView", null)
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

