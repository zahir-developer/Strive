using System;
using CoreGraphics;
using Foundation;
using Strive.Core.Models.Employee.CheckOut;
using UIKit;
using Xamarin.Essentials;

namespace StriveOwner.iOS.Views.CheckOut
{
    public partial class CheckOut_Cell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CheckOut_Cell");
        public static readonly UINib Nib;

        UIView statusIndicatorView;
        UILabel checkoutIdLabel;
        UILabel customerNameLabel;
        UILabel serviceInfoLabel;
        UILabel checkInAndOutTimingLabel;
        UIImageView statusIndicatorImage;
        UILabel paidStatusLabel;
        UILabel amountLabel;
        UIView paidStatusContainer;
        UIView membershipNameContainer;
        UILabel membershipNameLabel;


        NSLayoutConstraint membershipContainerTopConstraintToBottomOfPaid;
        NSLayoutConstraint membershipContainerTopConstraintToTopOfParent;

        static CheckOut_Cell()
        {
            Nib = UINib.FromName("CheckOut_Cell", NSBundle.MainBundle);
        }

        protected CheckOut_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            var containerView = new UIView(CGRect.Empty);
            containerView.TranslatesAutoresizingMaskIntoConstraints = false;
            containerView.BackgroundColor = UIColor.White;
            ContentView.Add(containerView);

            statusIndicatorView = new UIView(CGRect.Empty);
            statusIndicatorView.TranslatesAutoresizingMaskIntoConstraints = false;
            statusIndicatorView.BackgroundColor = UIColor.Red;
            containerView.Add(statusIndicatorView);

            checkoutIdLabel = new UILabel(CGRect.Empty);
            checkoutIdLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            checkoutIdLabel.TextColor = UIColor.FromRGB(253.0f / 255.0f, 57.0f / 255.0f, 122.0f / 255.0f);
            checkoutIdLabel.Font = UIFont.BoldSystemFontOfSize(24);
            containerView.Add(checkoutIdLabel);

            customerNameLabel = new UILabel(CGRect.Empty);
            customerNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            customerNameLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            customerNameLabel.Font = UIFont.SystemFontOfSize(22, UIFontWeight.Semibold);
            containerView.Add(customerNameLabel);

            serviceInfoLabel = new UILabel(CGRect.Empty);
            serviceInfoLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            serviceInfoLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            serviceInfoLabel.Font = UIFont.SystemFontOfSize(16);
            serviceInfoLabel.Lines = -1;
            containerView.Add(serviceInfoLabel);

            checkInAndOutTimingLabel = new UILabel(CGRect.Empty);
            checkInAndOutTimingLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            checkInAndOutTimingLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            checkInAndOutTimingLabel.Font = UIFont.SystemFontOfSize(16, UIFontWeight.Semibold);
            checkInAndOutTimingLabel.Layer.CornerRadius = 5;
            containerView.Add(checkInAndOutTimingLabel);

            membershipNameContainer = new UIView(CGRect.Empty);
            membershipNameContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            membershipNameContainer.Layer.CornerRadius = 5;
            membershipNameContainer.BackgroundColor = ColorConverters.FromHex("#E3E3E3").ToPlatformColor();
            membershipNameContainer.Layer.MaskedCorners = CoreAnimation.CACornerMask.MinXMinYCorner | CoreAnimation.CACornerMask.MinXMaxYCorner;
            membershipNameContainer.Layer.CornerRadius = 5;
            membershipNameContainer.Hidden = true;
            containerView.Add(membershipNameContainer);

            var membershipImage = new UIImageView(CGRect.Empty);
            membershipImage.TranslatesAutoresizingMaskIntoConstraints = false;
            membershipImage.Image = UIImage.FromBundle("badge-star");
            membershipNameContainer.Add(membershipImage);

            membershipNameLabel = new UILabel(CGRect.Empty);
            membershipNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            membershipNameLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            membershipNameLabel.Font = UIFont.SystemFontOfSize(12);
            membershipNameContainer.Add(membershipNameLabel);

            paidStatusContainer = new UIView(CGRect.Empty);
            paidStatusContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            paidStatusContainer.Layer.CornerRadius = 5;
            paidStatusContainer.BackgroundColor = UIColor.LightGray;
            paidStatusContainer.Layer.MaskedCorners = CoreAnimation.CACornerMask.MinXMinYCorner | CoreAnimation.CACornerMask.MinXMaxYCorner;
            containerView.Add(paidStatusContainer);

            statusIndicatorImage = new UIImageView(CGRect.Empty);
            statusIndicatorImage.TranslatesAutoresizingMaskIntoConstraints = false;
            statusIndicatorImage.BackgroundColor = UIColor.Blue;
            paidStatusContainer.Add(statusIndicatorImage);

            paidStatusLabel = new UILabel(CGRect.Empty);
            paidStatusLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            paidStatusLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            paidStatusLabel.Font = UIFont.SystemFontOfSize(14);
            paidStatusContainer.Add(paidStatusLabel);

            paidStatusContainer.Hidden = true;

            amountLabel = new UILabel(CGRect.Empty);
            amountLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            amountLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            amountLabel.Font = UIFont.BoldSystemFontOfSize(18);
            containerView.Add(amountLabel);

            containerView.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
            containerView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
            containerView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            containerView.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;

            statusIndicatorView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor).Active = true;
            statusIndicatorView.TopAnchor.ConstraintEqualTo(containerView.TopAnchor).Active = true;
            statusIndicatorView.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor).Active = true;
            statusIndicatorView.WidthAnchor.ConstraintEqualTo(6).Active = true;

            checkoutIdLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 20).Active = true;
            checkoutIdLabel.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, constant: 15).Active = true;

            customerNameLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 20).Active = true;
            customerNameLabel.TopAnchor.ConstraintEqualTo(checkoutIdLabel.BottomAnchor, constant: 5).Active = true;

            serviceInfoLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 20).Active = true;
            serviceInfoLabel.TopAnchor.ConstraintEqualTo(customerNameLabel.BottomAnchor, constant: 5).Active = true;
            serviceInfoLabel.WidthAnchor.ConstraintEqualTo(300).Active = true;

            checkInAndOutTimingLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 20).Active = true;
            checkInAndOutTimingLabel.TopAnchor.ConstraintEqualTo(serviceInfoLabel.BottomAnchor, constant: 5).Active = true;
            checkInAndOutTimingLabel.HeightAnchor.ConstraintEqualTo(30).Active = true;
            checkInAndOutTimingLabel.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor, constant: -15).Active = true;

            paidStatusContainer.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor).Active = true;
            paidStatusContainer.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, constant: 40).Active = true;
            paidStatusContainer.HeightAnchor.ConstraintEqualTo(30).Active = true;

            statusIndicatorImage.LeadingAnchor.ConstraintEqualTo(paidStatusContainer.LeadingAnchor, constant: 12).Active = true;
            statusIndicatorImage.HeightAnchor.ConstraintEqualTo(20).Active = true;
            statusIndicatorImage.WidthAnchor.ConstraintEqualTo(20).Active = true;
            statusIndicatorImage.CenterYAnchor.ConstraintEqualTo(paidStatusContainer.CenterYAnchor).Active = true;

            paidStatusLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorImage.TrailingAnchor, constant: 16).Active = true;
            paidStatusLabel.TrailingAnchor.ConstraintEqualTo(paidStatusContainer.TrailingAnchor, constant: -20).Active = true;
            paidStatusLabel.CenterYAnchor.ConstraintEqualTo(paidStatusContainer.CenterYAnchor).Active = true;

            membershipNameContainer.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor).Active = true;
            membershipNameContainer.HeightAnchor.ConstraintEqualTo(35).Active = true;
            membershipContainerTopConstraintToBottomOfPaid = membershipNameContainer.TopAnchor.ConstraintEqualTo(paidStatusContainer.BottomAnchor, constant: 8);
            membershipContainerTopConstraintToTopOfParent = membershipNameContainer.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, constant: 20);

            membershipImage.LeadingAnchor.ConstraintEqualTo(membershipNameContainer.LeadingAnchor, constant: 4).Active = true;
            membershipImage.HeightAnchor.ConstraintEqualTo(20).Active = true;
            membershipImage.WidthAnchor.ConstraintEqualTo(20).Active = true;
            membershipImage.CenterYAnchor.ConstraintEqualTo(membershipNameContainer.CenterYAnchor).Active = true;

            membershipNameLabel.LeadingAnchor.ConstraintEqualTo(membershipImage.TrailingAnchor, constant: 2).Active = true;
            membershipNameLabel.TrailingAnchor.ConstraintEqualTo(membershipNameContainer.TrailingAnchor, constant: -10).Active = true;
            membershipNameLabel.CenterYAnchor.ConstraintEqualTo(membershipNameContainer.CenterYAnchor).Active = true;


            amountLabel.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, constant: -10).Active = true;
            amountLabel.TopAnchor.ConstraintEqualTo(customerNameLabel.BottomAnchor, constant: 5).Active = true;
        }

        public void SetupData(checkOutViewModel checkout)
        {
            if (checkout.ColorCode != null && checkout.ColorCode != "")
            {
                statusIndicatorView.BackgroundColor = ColorConverters.FromHex(checkout.ColorCode).ToPlatformColor();
            }
            checkoutIdLabel.Text = checkout.TicketNumber;
            customerNameLabel.Text = checkout.CustomerFirstName + " " + checkout.CustomerLastName;
            serviceInfoLabel.Text = checkout.VehicleDescription;
            if (checkout.Services != null) { }
            serviceInfoLabel.Text += "\n" + "Services: " + checkout.Services;
            checkInAndOutTimingLabel.Text = "Check in " + checkout.Checkin + " - " + "Check out " + checkout.Checkout;
            statusIndicatorImage.Image = new UIImage();
            if (string.IsNullOrEmpty(checkout.MembershipName) || string.IsNullOrWhiteSpace(checkout.MembershipName))
            {
                membershipNameContainer.Hidden = true;
            }
            else
            {
                membershipNameContainer.Hidden = false;
                membershipNameLabel.Text = checkout.MembershipName;
            }

            if (checkout.PaymentStatus.Equals("Success"))
            {
                paidStatusLabel.Text = "Paid";
                paidStatusContainer.Hidden = false;
            }
            else
            {
                paidStatusLabel.Text = string.Empty;
                paidStatusContainer.Hidden = true;
            }
            amountLabel.Text = "$" + checkout.Cost;
        }
    }
}
