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
        public static UIImage contract;
        public static UIImage TermsConfirmView;
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
            SwitchMembershipFee.Hidden = true;
            UpchargesLbl.Hidden = true;
            NavigationItem.Title = "Vehicle";
            string Datenow = DateTime.Now.Date.ToString("yyyy-MM-dd");
            StartingDate.Text = new DateTime(DateTime.Now.Date.AddMonths(1).Year, DateTime.Now.Date.AddMonths(1).Month, 1).ToString("yyyy-MM-dd"); 
            EndingDate.Text = "Open";
            Date.Text = Datenow.Substring(0, 10);
            if (MembershipDetails.selectedMembershipDetail.MembershipName.Length<8)
            {

                string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName+ "- $" + (VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
                membership_name.Text = membershipname;
            }

            else
            {
                string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName.Substring(8) + "- $" + (VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
                membership_name.Text = membershipname;
            }
            //string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName.Substring(8) + "- $" + (VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
           
            TermsParentView.Layer.CornerRadius = 5;
            GetTotal();


            //termsLabel.Text = "";
        }
        public void GetTotal ()
        {
            double MembershipAmount = VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price;
            var SelectedServices = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach (var Service in SelectedServices)
            {
                if (Service.Price != null)
                {
                    
                    //Console.WriteLine(Service.Upcharges.Split("-")[1].Substring(3));
                    Total += (double)Service.Price;
                    SelectedAdditionalServices.Add(Service.ServiceName.Trim() + "-$"+ string.Format("{0:0.00}", Service.Price));
                     
                }
                else
                {
                    SelectedAdditionalServices.Add(Service.ServiceName.Trim() + "-$0");
                }
               
               
                
            }
            if (CustomerInfo.MembershipFee!=0)
            {
                MembershipAmount += CustomerInfo.MembershipFee;
                SwitchMembershipFee.Hidden = false;
            }
            if (MembershipDetails.modelUpcharge.upcharge.Count!=0)
            {
                UpchargesLbl.Hidden = false;
                MembershipAmount += MembershipDetails.modelUpcharge.upcharge[0].Price;
                UpchargesLbl.Text = "Wash Upcharges:   "+"$"+string.Format("{0:0.00}", MembershipDetails.modelUpcharge.upcharge[0].Price);
            }
            string ValuesToDisply = "Additional Services: "+ string.Join(", ",SelectedAdditionalServices);
            DisplaySelectedAddtionals.Text = ValuesToDisply;
            AdditionalServicesTotal.Text = "$"+ string.Format("{0:0.00}", Total);
            Total += MembershipAmount;
            MonthlyTotal.Text = "$"+ string.Format("{0:0.00}", Total);
            total.Text = "$" + string.Format("{0:0.00}", Total);
            Yearlytotal.Text = "$" + string.Format("{0:0.00}", Total * 12);


        }

        
        partial void AgreeBtn_Touch(UIButton sender)
        {
            contract = UIViewExtensions.AsImage(termsLabel);
            TermsConfirmView= UIViewExtensions.AsImage(_TermsConfirmView);
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
    public static class UIViewExtensions
    {

        public static UIImage AsImage(this UIView view)
        {
            UIGraphics.BeginImageContextWithOptions(view.Bounds.Size, view.Opaque, 0);
            view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            UIImage img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return img;
        }

        public static UIImage TakeScreenshot()
        {
            return UIApplication.SharedApplication.KeyWindow.AsImage();
        }

    }
}

