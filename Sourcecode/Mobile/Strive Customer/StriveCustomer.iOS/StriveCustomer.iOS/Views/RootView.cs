using System;
using UIKit;
using MvvmCross;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using MvvmCross.ViewModels;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class RootView : MvxTabBarViewController<DashboardViewModel>
    {
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

            var viewControllers = new UIViewController[5];
            viewControllers[0] = CreateTabFor(0, "Home", "icon-wash-time", typeof(ForgotPasswordViewModel));
            viewControllers[1] = CreateTabFor(1, "Deals", "icon-wash-time", typeof(ForgotPasswordViewModel));
            viewControllers[2] = CreateTabFor(2, "Schedule", "icon-membership", typeof(ForgotPasswordViewModel));
            viewControllers[3] = CreateTabFor(3, "Account", "icon-inventory", typeof(ForgotPasswordViewModel));
            viewControllers[4] = CreateTabFor(4, "Contact us", "icon-inventory", typeof(ForgotPasswordViewModel));

            ViewControllers = viewControllers;
            CustomizableViewControllers = new UIViewController[] { };
            TabBar.BarTintColor = UIColor.Clear.FromHex(0x1DC9B7);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private UIViewController CreateTabFor(int index, string title, string imageName, Type viewModelType)
        {
            var controller = new UINavigationController();
            var request = new MvxViewModelRequest(viewModelType, null, null);
            var viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
            screen.Title = title;
            //screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle(imageName), index);
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

            //screen.TabBarItem.Image = UIImage.FromBundle(imageName).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
            //screen.TabBarItem.SelectedImage = UIImage.FromBundle(imageName);
           
            controller.PushViewController(screen, true);
            return controller;
        }
    }
}

