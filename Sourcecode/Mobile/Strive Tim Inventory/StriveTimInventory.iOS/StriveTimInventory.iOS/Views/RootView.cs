using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    [MvxRootPresentation]
    public partial class RootView : MvxTabBarViewController<RootViewModel>
    {
        private bool _firstTimePresented = true;

        public RootView() : base("RootView", null)
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (_firstTimePresented)
            {
                _firstTimePresented = false;
                ViewModel.ShowClockInCommand();
            }
        }
    }
}

