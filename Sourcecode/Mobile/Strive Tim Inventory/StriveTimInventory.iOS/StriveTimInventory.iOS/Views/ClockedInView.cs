using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class ClockedInView : MvxViewController<ClockedInViewModel>
    {
        public ClockedInView() : base("ClockedInView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ClockinView.Layer.CornerRadius = ClockoutView.Layer.CornerRadius = 20;
            ClockinView.Layer.MaskedCorners = (CoreAnimation.CACornerMask)5;
            ClockoutView.Layer.MaskedCorners = (CoreAnimation.CACornerMask)10;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

