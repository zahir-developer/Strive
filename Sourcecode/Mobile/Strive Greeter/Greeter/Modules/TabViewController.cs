// This file has been autogenerated from a class added in the UI designer.

using System;
using Greeter.Common;
using Greeter.Modules.Home;
using Greeter.Modules.Pay;
using UIKit;

namespace Greeter
{
    public partial class TabViewController : UITabBarController
    {
        public TabViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBar.Hidden = false;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var vc1 = new UINavigationController(new WashTimeViewController());
            var tabbarItem1 = new UITabBarItem(title: "Home", image: UIImage.FromBundle(ImageNames.HOME), null);
            vc1.TabBarItem = tabbarItem1;
            var item3 = new PaymentViewController();
            var icon3 = new UITabBarItem(title: "Pay", image: UIImage.FromBundle(ImageNames.DOLLAR), null);
            item3.TabBarItem = icon3;
            var item4 = new CheckoutViewController();
            var icon4 = new UITabBarItem(title: "Checkout", image: UIImage.FromBundle(ImageNames.CHECKOUT), null);
            item4.TabBarItem = icon4;
            var controllers = ViewControllers;
            controllers[0] = vc1;
            controllers[2] = item3;
            controllers[3] = item4;
            ViewControllers = controllers;
            NavigationController.NavigationBar.Hidden = true;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }
    }
}
