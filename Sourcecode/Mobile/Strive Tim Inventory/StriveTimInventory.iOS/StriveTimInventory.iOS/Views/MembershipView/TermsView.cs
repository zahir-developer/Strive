using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class TermsView : MvxViewController<TermsViewModel>
    {

        public static UIImage TermsViewImg;
        public TermsView() : base("TermsView", null)
        {
        }
        string Model;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<TermsView, TermsViewModel>();
            //set.Bind(AgreeButton).To(vm => vm.Commands["Next"]);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(DisagreeButton).To(vm => vm.Commands["Disagree"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void InitialSetup()
        {
            Membership_name.Text = MembershipData.SelectedMembership.MembershipName + ": $"+(MembershipData.SelectedMembershipPrice);
            if (MembershipData.SelectedVehicle.VehicleModel.Contains("/"))
            {
                Model = MembershipData.SelectedVehicle.VehicleModel.Substring(0, Model.IndexOf("/"));
            }
            else
            {
                Model = MembershipData.SelectedVehicle.VehicleModel;
            }
            Vehicle.Text = MembershipData.SelectedVehicle.VehicleMfr + "/" + Model + "/" + MembershipData.SelectedVehicle.VehicleColor;
            Total.Text = "Total: $"+Math.Round(MembershipData.CalculatedPrice).ToString();
            YearlyTotal.Text = "$"+Math.Round(MembershipData.CalculatedPrice * 12).ToString();
            MonthlyTotal.Text = "$"+Math.Round(MembershipData.CalculatedPrice).ToString();
            AdditionalServicesTotal.Text = "Additional Services: $"+(MembershipData.AdditionalServicesPrice != 0 ? Math.Round(MembershipData.AdditionalServicesPrice).ToString() : "0.00");
            Upchargeslbl.Text = "Upcharge: $" + (MembershipData.UpchargesPrice != 0 ? MembershipData.UpchargesPrice.ToString() : "0.00");
            Date.Text = "Date:" + DateTime.Now.ToString("dd-MM-yyyy");
            StartingDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            EndingDate.Text = "Open";
            DisplaySelectedAdditionals.Text = MembershipData.additionalserviceswithprice;
            if (MembershipData.isMembershipSwitch == true)
            {
                SwitchMembershipFee.Hidden = false;
            }
            else
            {
                SwitchMembershipFee.Hidden = true;
            }
        }

        partial void AgreeButtonTouch(UIButton sender)
        {
            UIImage Original = UIViewExtensions.AsImage(ContractView);
            TermsViewImg = Original;
            //string  = Original.AsJPEG(0.15f).GetBase64EncodedString(NSDataBase64EncodingOptions.None);

            ViewModel.NextCommand();
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
    public static class CropImage
    {
        public static UIImage cropImage(this UIImage sourceImage, int crop_x, int crop_y, int width, int height)
        {
            var imgSize = sourceImage.Size;
            UIGraphics.BeginImageContext(new SizeF(width, height));
            var context = UIGraphics.GetCurrentContext();
            var clippedRect = new RectangleF(0, 0, width, height);
            context.ClipToRect(clippedRect);
            var drawRect = new CGRect(-crop_x, -crop_y, imgSize.Width, imgSize.Height);
            sourceImage.Draw(drawRect);
            var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return modifiedImage;
        }

    }


}

