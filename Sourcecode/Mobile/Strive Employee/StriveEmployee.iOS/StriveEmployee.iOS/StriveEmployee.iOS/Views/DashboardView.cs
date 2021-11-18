using System;
using MvvmCross;
using UIKit;
using MvvmCross.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.iOS.UIUtils;
using Strive.Core.ViewModels.Employee.MyProfile;
using Strive.Core.ViewModels.Employee.CheckOut;
using Strive.Core.ViewModels.Employee.Schedule;
using Strive.Core.ViewModels.Employee.MyTicket;

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

            var viewControllers = new UIViewController[5];
            viewControllers[0] = CreateTabFor(0, "Message", "icon-message", "icon-message-active", typeof(MessengerViewModel));
            viewControllers[1] = CreateTabFor(1, "Schedule", "icon-schedule", "icon-schedule-active", typeof(ScheduleViewModel));
            viewControllers[2] = CreateTabFor(2, "Profile", "icon-profile", "icon-profile-active", typeof(EmployeeInfoViewModel));
            //viewControllers[3] = CreateTabFor(3, "Ticket", "icon-ticket", "icon-ticket-active", typeof(MyTicketViewModel));
            viewControllers[3] = CreateTabFor(3, "CheckOut", "icon-checkout", "icon-checkout-active", typeof(CheckOutViewModel));
            viewControllers[4] = CreateTabFor(4, "PayRoll", "pay", "pay", typeof(PayRollViewModel));
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

