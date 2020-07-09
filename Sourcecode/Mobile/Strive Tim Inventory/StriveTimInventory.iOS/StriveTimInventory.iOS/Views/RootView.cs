﻿using System;
using MvvmCross;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class RootView : MvxTabBarViewController<RootViewModel>
    {
        private bool _firstTimePresented = true;

        private bool _constructed;

        public RootView() : base("RootView", null)
        {
            _constructed = true;

            // need this additional call to ViewDidLoad because UIkit creates the view before the C# hierarchy has been constructed
            ViewDidLoad();
        }

        public override void ViewDidLoad()
        {
            if (!_constructed)
                return;

            base.ViewDidLoad();

            var viewControllers = new UIViewController[]
            {
            CreateTabFor(0, "Time Clock", "TabIcon", typeof(ClockInViewModel)),
            CreateTabFor(1, "Wash Times", "TabIcon", typeof(WashTimesViewModel))
            };

            ViewControllers = viewControllers;
            CustomizableViewControllers = new UIViewController[] { };
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
            //if (_firstTimePresented)
            //{
            //    _firstTimePresented = false;
            //    ViewModel.ShowClockInCommand();
            //}
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
        }

        public override UITraitCollection TraitCollection
        {
            get
            {
                return UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
            }
        }

        private UIViewController CreateTabFor(int index, string title, string imageName, Type viewModelType)
        {
            var controller = new UINavigationController();
            var request = new MvxViewModelRequest(viewModelType, null, null);
            var viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
            screen.Title = title;
            screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle(imageName), index);
            screen.TabBarItem.SetTitleTextAttributes(new UITextAttributes()
            {
                Font = UIFont.FromName("OpenSans-Regular", 10f),
                TextColor = UIColor.Blue,
            }, UIControlState.Normal);
            
            controller.PushViewController(screen, true);
            return controller;
        }
    }
}

