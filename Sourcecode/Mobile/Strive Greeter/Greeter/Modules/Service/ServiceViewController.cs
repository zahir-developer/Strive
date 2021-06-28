// This file has been autogenerated from a class added in the UI designer.

using System;
using Greeter.Common;
using Greeter.Extensions;
using Greeter.Modules.Service;
using Greeter.Storyboards;
using UIKit;

namespace Greeter
{
    public partial class ServiceViewController : BaseViewController
    {
        // Static Data
        readonly UIColor unselectedBtnBgColor = UIColor.FromRGB(204, 255, 248);
        readonly UIColor unselectedBtnTxtColor = UIColor.Black;
        readonly UIColor selectedBtnBgColor = UIColor.FromRGB(1, 100, 87);
        readonly UIColor selectedBtnTxtColor = UIColor.White;

        // State Values
        bool IsWash = true;

        public ServiceViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Initial UI Settings
            txtFieldBarcode.AddLeftPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            txtFieldBarcode.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            //Clicks
            //btnBack.TouchUpInside += delegate
            //{
            //    GoBackWithAnimation();
            //};

            btnWash.TouchUpInside += delegate
            {
                IsWash = true;
                UnSelectButton(btnDetail);
                SelectButton(btnWash);
            };

            btnDetail.TouchUpInside += delegate
            {
                IsWash = false;
                UnSelectButton(btnWash);
                SelectButton(btnDetail);
            };

            btnCloseBarcode.TouchUpInside += delegate
            {
                EmptyTextField(txtFieldBarcode);
            };

            btnCancel.TouchUpInside += delegate
            {
                GoBackWithAnimation();
            };

            btnSelect.TouchUpInside += delegate
            {
                if (txtFieldBarcode.Text.IsEmpty())
                {
                    ShowAlertMsg(Common.Messages.BARCODE_EMPTY);
                }
                else
                {
                    NavigateToWashOrDetailScreen();
                }
            };

            btnDriveUp.TouchUpInside += delegate
            {
                NavigateToWashOrDetailScreen();
            };

            UITapGestureRecognizer lastSeviceGesture = new UITapGestureRecognizer(LastServiceTap);
            //tapGesture.NumberOfTapsRequired = 1;
            lblLastService.AddGestureRecognizer(lastSeviceGesture);

            UITapGestureRecognizer viewIssueGesture = new UITapGestureRecognizer(NavigateToIssue);
            //tapGesture.NumberOfTapsRequired = 1;
            lblViewIssue.AddGestureRecognizer(viewIssueGesture);
        }

        void LastServiceTap(UITapGestureRecognizer tap)
        {
            NavigateToLastService();
        }

        void NavigateToLastService()
        {
            NavigateToWithAnim(new LastVisitViewController());
        }

        void NavigateToIssue()
        {
            var vc = this.Storyboard.InstantiateViewController(nameof(IssuesViewController));
            NavigateToWithAnim(vc);
        }

        void NavigateToWashOrDetailScreen()
        {
            //if (IsWash)
            //    ShowAlertMsg(btnWash.TitleLabel.Text);
            //else
            //    ShowAlertMsg(btnDetail.TitleLabel.Text);

            //UIStoryboard sb = UIStoryboard.FromName("", null);

            var vc = (ServiceQuestionViewController)this.Storyboard.InstantiateViewController(nameof(ServiceQuestionViewController));

            if (IsWash)
            {
                vc.ServiceType = ServiceType.Wash;
            }
            else
            {
                vc.ServiceType = ServiceType.Detail;
            }

            NavigateToWithAnim(vc);
        }

        void UnSelectButton(UIButton btn)
        {
            btn.BackgroundColor = unselectedBtnBgColor;
            SetTextColor(btn, unselectedBtnTxtColor);
        }

        void SelectButton(UIButton btn)
        {
            btn.BackgroundColor = selectedBtnBgColor;
            SetTextColor(btn, selectedBtnTxtColor);
        }

        void EmptyTextField(UITextField txtField)
        {
            txtField.Text = string.Empty;
        }

        void SetTextColor(UIButton btn, UIColor color)
        {
            btn.SetTitleColor(color, UIControlState.Normal);
        }
    }
}
