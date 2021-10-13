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
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Vehicle";

            TermsParentView.Layer.CornerRadius = 5;

            //termsLabel.Text = "";
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

