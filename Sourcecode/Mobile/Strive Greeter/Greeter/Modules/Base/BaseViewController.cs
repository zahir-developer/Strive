using System;
using UIKit;

namespace Greeter
{
    public class BaseViewController : UIViewController
    {
        public BaseViewController()
        {
        }

        protected internal BaseViewController(IntPtr handle) : base(handle)
        {
        }

        void GoBack(bool isAnimation)
        {
            this.NavigationController.PopViewController(isAnimation);
        }

        void NavigateTo(UIViewController vc, bool isAnimation)
        {
            this.NavigationController.PushViewController(vc, isAnimation);
        }

        public void NavigateToWithoutAnim(UIViewController vc)
        {
            NavigateTo(vc, false);
        }

        public void NavigateToWithAnim(UIViewController vc)
        {
            NavigateTo(vc, true);
        }

        public void GoBackWithoutAnimation()
        {
            GoBack(false);
        }

        public void GoBackWithAnimation()
        {
            GoBack(true);
        }

        public void ShowAlertMsg(string msg)
        {
            string title = "Alert";

            string ok = "OK";

            var okAlertController = UIAlertController.Create(title, msg, UIAlertControllerStyle.Alert);

            //Add Action
            okAlertController.AddAction(UIAlertAction.Create(ok, UIAlertActionStyle.Default, null));

            // Present Alert
            PresentViewController(okAlertController, true, null);
        }
    }
}
