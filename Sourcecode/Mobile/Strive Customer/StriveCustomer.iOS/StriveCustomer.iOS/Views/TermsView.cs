using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
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
            string Datenow= DateTime.Now.Date.ToString();
            Date.Text = Datenow.Substring(0,10);
            string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName + "-$"+MembershipDetails.selectedMembershipDetail.DiscountedPrice;
            membership_name.Text = membershipname;
            TermsParentView.Layer.CornerRadius = 5;
            _TermsConfirmView.Layer.CornerRadius = 5;

            //termsLabel.Text = "";
        }
        
        partial void AgreeBtn_Touch(UIButton sender)
        {
            ViewModel.NavToSignatureView();
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
                ViewModel.NavToSignatureView();
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

