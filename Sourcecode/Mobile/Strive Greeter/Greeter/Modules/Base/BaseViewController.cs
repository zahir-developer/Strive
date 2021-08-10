using System;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using UIKit;

namespace Greeter
{
    public abstract class BaseViewController : UIViewController
    {
        UIActivityIndicatorView activityIndicator;
        UITapGestureRecognizer tapArroundTapGesture;

        bool dismissKeyboardOnTapArround;

        protected virtual bool DismissKeyboardOnTapArround
        {
            get => dismissKeyboardOnTapArround;
            set
            {
                dismissKeyboardOnTapArround = value;
                if (value)
                    SetTapArround();
                else
                    RemoveTapArround();
            }
        }

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

            if (DismissKeyboardOnTapArround)
                SetTapArround();
        }

        void SetTapArround()
        {
            if (tapArroundTapGesture is not null && !IsViewLoaded) return;

            tapArroundTapGesture = new UITapGestureRecognizer(_ => View.EndEditing(true));

            View.AddGestureRecognizer(tapArroundTapGesture);
        }

        void RemoveTapArround()
        {
            if (tapArroundTapGesture is null && !IsViewLoaded) return;

            View.RemoveGestureRecognizer(tapArroundTapGesture);
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

        // Show alert for response if needed
        public void HandleResponse(BaseResponse response)
        {
            if (response.IsNoInternet())
            {
                ShowAlertMsg(Common.Messages.NO_INTERNET_MSG);
                return;
            }

            if (response.IsUnAuthorised())
            {
                ShowAlertMsg(Common.Messages.SESSION_TIMED_OUT, () =>
                {
                    Logout();
                });
                return;
            }

            if (response.IsInternalServerError())
            {
                ShowAlertMsg(Common.Messages.INTERNAL_SERVER_ERROR);
                return;
            }

            if (response.IsBadRequest())
            {
                ShowAlertMsg(Common.Messages.BAD_REQUEST);
                return;
            }
        }

        public void Logout()
        {
            AppSettings.Clear();

            UIViewController loginViewController = UIStoryboard.FromName(StoryBoardNames.USER, null)
                                  .InstantiateViewController(nameof(LoginViewController));

            var nc = TabBarController?.NavigationController ?? NavigationController;
            nc?.SetViewControllers(new UIViewController[] { loginViewController }, true);
        }

        public void ShowAlertMsg(string msg, Action okAction = null, bool isCancel = false, string titleTxt = null)
        {
            string title = "Alert";
            string ok = "Ok";
            string cancel = "Cancel";

            if (!string.IsNullOrEmpty(titleTxt))
            {
                title = titleTxt;
            }

            var okAlertController = UIAlertController.Create(title, msg, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create(ok, UIAlertActionStyle.Default,
                alert =>
                {
                    okAction?.Invoke();
                }));
            if (isCancel)
                okAlertController.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, null));
            PresentViewController(okAlertController, true, null);
        }

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
