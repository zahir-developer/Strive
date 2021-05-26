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

        public bool IsEmpty(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return true;
            }

            return false;
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

        public void AddLeftPadding(UITextField txtField, float padding)
        {
            var paddingView = new UIView(frame: new CoreGraphics.CGRect(0, 0, padding, txtField.Frame.Height));
            txtField.LeftView = paddingView;
            txtField.LeftViewMode = UITextFieldViewMode.Always;
        }

        public void AddRightPadding(UITextField txtField, float padding)
        {
            var paddingView = new UIView(frame: new CoreGraphics.CGRect(0, 0, padding, txtField.Frame.Height));
            txtField.RightView = paddingView;
            txtField.RightViewMode = UITextFieldViewMode.Always;
        }
    }
}
