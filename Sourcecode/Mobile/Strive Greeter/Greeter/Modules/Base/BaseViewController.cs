using System;
using Greeter.Common;
using MvvmCross.Platforms.Ios.Views;
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

        public UIViewController GetViewController(UIStoryboard sb, string vcId)
        {
            //string dsa = nameof(t);

            return sb.InstantiateViewController(vcId);
        }

        UIStoryboard GetStoryboard(string name)
        {
            return UIStoryboard.FromName(name, null);
        }

        public UIStoryboard GetHomeStorybpard()
        {
            return GetStoryboard(StoryBoardNames.HOME);
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

        //public void AddLeftPadding(UITextField tf, float padding)
        //{
        //    UIView paddingView = new UIView(new CoreGraphics.CGRect(0, 0, padding, tf.Frame.Height));
        //    tf.LeftView = paddingView;
        //    tf.LeftViewMode = UITextFieldViewMode.Always;
        //}

        //public void AddRightPadding(UITextField tf, float padding)
        //{
        //    UIView paddingView = new UIView(new CoreGraphics.CGRect(0, 0, padding, tf.Frame.Height));
        //    tf.RightView = paddingView;
        //    tf.RightViewMode = UITextFieldViewMode.Always;
        //}

        public void AddPickerToolbar(UITextField textField, string title, Action action)
        {
            const string CANCEL_BUTTON_TXT = "Cancel";

            const string DONE_BUTTON_TXT = "Done";

            var toolbarDone = new UIToolbar();

            toolbarDone.SizeToFit();

            UIBarButtonItem barBtnCancel = new UIBarButtonItem(CANCEL_BUTTON_TXT, UIBarButtonItemStyle.Plain, (sender, s) =>
            {
                textField.EndEditing(false);
            });

            UIBarButtonItem barBtnDone = new UIBarButtonItem(DONE_BUTTON_TXT, UIBarButtonItemStyle.Done, (sender, s) =>
            {
                textField.EndEditing(false);
                action.Invoke();
            });

            //barBtnDone.CustomView = btnDone;

            UIBarButtonItem barBtnSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

            UILabel lbl = new UILabel();

            //lbl.Frame = new CoreGraphics.CGRect(0, 0, View.Frame.Width / 3, toolbarDone.Frame.Height);

            lbl.Text = title;

            lbl.TextAlignment = UITextAlignment.Center;

            lbl.Font = UIFont.BoldSystemFontOfSize(size: 16.0f);

            UIBarButtonItem lblBtn = new UIBarButtonItem(lbl);

            toolbarDone.Items = new UIBarButtonItem[] { barBtnCancel, barBtnSpace, lblBtn, barBtnSpace, barBtnDone };

            textField.InputAccessoryView = toolbarDone;
        }
    }
}
