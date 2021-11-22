using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public double Total = 0;
        public List<string> SelectedAdditionalServices = new List<string>();
        UILabel DisplaySelectedServices = new UILabel();
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
            string Datenow = DateTime.Now.Date.ToString("yyyy-MM-dd");
            StartingDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            EndingDate.Text = DateTime.Now.Date.AddMonths(12).ToString("yyyy-MM-dd");
            Date.Text = Datenow.Substring(0, 10);
            string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName.Substring(8) + "-$" + MembershipDetails.selectedMembershipDetail.DiscountedPrice;
            membership_name.Text = membershipname;
            TermsParentView.Layer.CornerRadius = 5;
            GetTotal();


            //termsLabel.Text = "";
        }
        public void GetTotal ()
        {
            double MembershipAmount = MembershipDetails.selectedMembershipDetail.DiscountedPrice;
            var SelectedServices = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach (var Service in SelectedServices)
            {
                if (Service.Upcharges != null )
                {
                    if (Service.Upcharges.Count() > 4)
                    {
                        
                        Console.WriteLine(Service.Upcharges.Split("-")[1].Substring(3));
                        Total += double.Parse(Service.Upcharges.Split("-")[1].Substring(3));
                        SelectedAdditionalServices.Add(Service.ServiceName.Trim() + "-$"+Service.Upcharges.Split("-")[1].Substring(3));
                    } 
                }
                else
                {
                    SelectedAdditionalServices.Add(Service.ServiceName + "-$0");
                }
               
               
                
            }
            string ValuesToDisply = String.Join(", ",SelectedAdditionalServices);
            DisplaySelectedAddtionals.Text = ValuesToDisply;
            AdditionalServicesTotal.Text = "$"+Total.ToString();
            Total += MembershipAmount;
            MonthlyTotal.Text = "$"+Total.ToString();
            total.Text = "$" + Total.ToString();
            Yearlytotal.Text = "$" + (Total * 12).ToString();


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

