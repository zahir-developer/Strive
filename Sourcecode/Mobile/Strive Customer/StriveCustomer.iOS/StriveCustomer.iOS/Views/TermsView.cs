using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class TermsView : MvxViewController<TermsAndConditionsViewModel>
    {
        public TermsView() : base("TermsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void InitialSetup()
        {
            var rightBtn = new UIButton(UIButtonType.Custom);
            rightBtn.SetTitle("Next", UIControlState.Normal);
            rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var rightBarBtn = new UIBarButtonItem(rightBtn);
            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
            rightBtn.TouchUpInside += (sender, e) =>
            {
                
            };

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Vehicle";

            TermsParentView.Layer.CornerRadius = 5;
        }

        partial void AgreeBtn_Touch(UIButton sender)
        {
            AgreeTerms();
        }

        partial void DisAgreeBtn_Touch(UIButton sender)
        {
            DisagreeMembership();
        }

        private async void AgreeTerms()
        {
            //CancelMembership
            var result = await ViewModel.AgreeMembership();
            if (result)
            {                
                SignatureClass.signaturePoints = null;
                ViewModel.NavigateToLanding();
            }
        }

        private async void DisagreeMembership()
        {
            var result = await ViewModel.DisagreeMembership();

            if(result)
            {
                SignatureClass.signaturePoints = null;
                ViewModel.NavigateToLanding();
            }
        }
    }
}

