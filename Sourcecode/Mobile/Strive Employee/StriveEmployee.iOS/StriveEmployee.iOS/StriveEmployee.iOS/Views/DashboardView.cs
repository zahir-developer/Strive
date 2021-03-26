﻿using System;
using MvvmCross;
using UIKit;
using MvvmCross.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.iOS.UIUtils;
using Strive.Core.ViewModels.Employee.MyProfile;

namespace StriveEmployee.iOS.Views
{
    public partial class DashboardView : MvxTabBarViewController<DashboardViewModel>
    {
        private bool _constructed;
        public DashboardView() : base("DashboardView", null)
        {
            _constructed = true;

            ViewDidLoad();
        }

        public override void ViewDidLoad()
        {
            if (!_constructed)
                return;
            base.ViewDidLoad();

            var viewControllers = new UIViewController[4];
            viewControllers[0] = CreateTabFor(0, "Message", "icon-message", "icon-message-active", typeof(MyProfileViewModel));
            viewControllers[1] = CreateTabFor(1, "Schedule", "icon-schedule", "icon-schedule-active", typeof(MyProfileViewModel));
            viewControllers[2] = CreateTabFor(2, "Profile", "icon-profile", "icon-profile-active", typeof(MyProfileViewModel));
            viewControllers[3] = CreateTabFor(3, "Ticket", "icon-ticket", "icon-ticket-active", typeof(MyProfileViewModel));

            ViewControllers = viewControllers;
            CustomizableViewControllers = new UIViewController[] { };
            TabBar.BarTintColor = UIColor.Clear.FromHex(0x1DC9B7);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private UIViewController CreateTabFor(int index, string title, string imageName, string selectedImageName, Type viewModelType)
        {
            var controller = new UINavigationController();
            var request = new MvxViewModelRequest(viewModelType, null, null);
            var viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
            screen.TabBarItem.Image = UIImage.FromBundle(imageName).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            screen.TabBarItem.SelectedImage = UIImage.FromBundle(selectedImageName).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            screen.TabBarItem.ImageInsets = new UIEdgeInsets(5, 0, -5, 0);
            if (index == 2)
                screen.TabBarItem.ImageInsets = new UIEdgeInsets(0, 0, 0, 0);

            controller.PushViewController(screen, true);
            return controller;
        }
    }
}

