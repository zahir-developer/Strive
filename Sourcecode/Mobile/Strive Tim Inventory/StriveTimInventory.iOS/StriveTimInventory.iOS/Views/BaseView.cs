﻿using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class BaseView : MvxViewController<BaseViewModel>
    {
        public BaseView() : base("BaseView", null)
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

