﻿using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.iOS.UIUtils;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{
    public partial class MessengerView : MvxViewController<MessengerViewModel>
    {
        public MessengerView() : base("MessengerView", null)
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
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Messenger";
        }
    }
}

