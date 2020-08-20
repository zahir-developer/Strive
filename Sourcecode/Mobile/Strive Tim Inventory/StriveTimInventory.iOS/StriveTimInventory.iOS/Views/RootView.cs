using System;
using MvvmCross;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.UIUtils;
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

            var viewControllers = new UIViewController[4];
            var ClockStatus = EmployeeData.ClockInStatus;
            var difference = new TimeSpan();
            if(ClockStatus != null)
            {
                difference = DateUtils.GetTimeDifferenceValue(ClockStatus.outTime, ClockStatus.inTime);
            }
            if (ClockStatus == null || difference.Hours > 0)
            {
                viewControllers[0] = CreateTabFor(0, "Time Clock", "icon-time-clock", typeof(ClockInViewModel));
            }
            else
            {
                viewControllers[0] = CreateTabFor(0, "Time Clock", "icon-time-clock", typeof(ClockedInViewModel));
            }
            
            viewControllers[1] = CreateTabFor(1, "Wash Times", "icon-wash-time", typeof(WashTimesViewModel));
            viewControllers[2] = CreateTabFor(0, "Membership", "icon-membership", typeof(MembershipClientListViewModel));
            viewControllers[3] = CreateTabFor(1, "Inventory", "icon-inventory", typeof(InventoryViewModel));

            ViewControllers = viewControllers;
            CustomizableViewControllers = new UIViewController[] { };
            TabBar.BarTintColor = UIColor.Clear.FromHex(0x1DC9B7);
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
            TabBar.TintColor = UIColor.Clear.FromHex(0x0C4E47);
            screen.TabBarItem.SetTitleTextAttributes(new UITextAttributes()
            {
                Font = UIFont.FromName("OpenSans-Regular", 10f),
                TextColor = UIColor.Clear.FromHex(0x0C4E47),
            }, UIControlState.Selected);

            screen.TabBarItem.SetTitleTextAttributes(new UITextAttributes()
            {
                Font = UIFont.FromName("OpenSans-Regular", 10f),
                TextColor = UIColor.Clear.FromHex(0xFFFFFF),
            }, UIControlState.Normal);

            screen.TabBarItem.Image = UIImage.FromBundle(imageName).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            screen.TabBarItem.SelectedImage = UIImage.FromBundle(imageName);
           
            controller.PushViewController(screen, true);
            return controller;
        }
    }
}

