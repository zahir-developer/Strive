using System;
using Greeter.Common;
using Greeter.Extensions;
using UIKit;

namespace Greeter
{
    public class BaseViewController : UIViewController
    {
        UIActivityIndicatorView activityIndicator;

        public BaseViewController()
        {
        }

        protected internal BaseViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //SetBinding();
        }

        void SetBinding()
        {
            //var set = this.CreateBindingSet<BaseViewController, BaseViewModel>();
            //set.Bind(View.UserInteractionEnabled).To(vm => vm.);
            //set.Apply();
        }

        void DisableUIInteraction()
        {
            View.UserInteractionEnabled = false;
        }

        void EnableUIInteraction()
        {
            View.UserInteractionEnabled = true;
        }

        public void ShowActivityIndicator()
        {
            DisableUIInteraction();
            activityIndicator = View.AddActivityIndicator();
            activityIndicator.StartAnimating();
        }

        public void HideActivityIndicator()
        {
            EnableUIInteraction();
            activityIndicator.RemoveFromSuperview();
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

        public void ShowAlertMsg(string msg, Action okAction = null)
        {
            string title = "Alert";
            string ok = "OK";

            var okAlertController = UIAlertController.Create(title, msg, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create(ok, UIAlertActionStyle.Default,
                alert =>
                {
                    okAction?.Invoke();
                }));
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

            var barBtnCancel = new UIBarButtonItem(CANCEL_BUTTON_TXT, UIBarButtonItemStyle.Plain, (sender, s) =>
            {
                textField.EndEditing(false);
            });

            var barBtnDone = new UIBarButtonItem(DONE_BUTTON_TXT, UIBarButtonItemStyle.Done, (sender, s) =>
            {
                textField.EndEditing(false);
                action.Invoke();
            });

            var barBtnSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

            var lbl = new UILabel();
            lbl.Text = title;
            lbl.TextAlignment = UITextAlignment.Center;
            lbl.Font = UIFont.BoldSystemFontOfSize(size: 16.0f);
            var lblBtn = new UIBarButtonItem(lbl);

            toolbarDone.Items = new UIBarButtonItem[] { barBtnCancel, barBtnSpace, lblBtn, barBtnSpace, barBtnDone };
            textField.InputAccessoryView = toolbarDone;
        }
    }
}
