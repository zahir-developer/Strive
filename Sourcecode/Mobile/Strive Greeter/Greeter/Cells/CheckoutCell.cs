using System;
using CoreGraphics;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Cells
{
    public class CheckoutCell : UITableViewCell
    {
        public static readonly NSString Key = new("CheckoutCell");

        UIView statusIndicatorView;
        UILabel checkoutIdLabel;
        UILabel customerNameLabel;
        UILabel serviceInfoLabel;
        UILabel checkInAndOutTimingLabel;
        UIImageView statusIndicatorImage;
        UILabel paidStatusLabel;
        UILabel amountLabel;
        //UILabel remainingBalanceLabel;
        UIView paidStatusContainer;
        UIView membershipNameContainer;
        UILabel membershipNameLabel;
        UIButton payButton;

        NSLayoutConstraint membershipContainerTopConstraintToBottomOfPaid;
        NSLayoutConstraint membershipContainerTopConstraintToTopOfParent;

        Checkout checkout;
        Action<Checkout> pay = null;

        public CheckoutCell(IntPtr p) : base(p)
        {
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
            checkInAndOutTimingLabel.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            checkInAndOutTimingLabel.Font = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
            checkInAndOutTimingLabel.Layer.CornerRadius = 5;
            containerView.Add(checkInAndOutTimingLabel);

            paidStatusContainer = new UIView(CGRect.Empty);
            paidStatusContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            paidStatusContainer.Layer.CornerRadius = 5;
            paidStatusContainer.BackgroundColor = ColorConverters.FromHex("#AFE9E3").ToPlatformColor();
            paidStatusContainer.Layer.MaskedCorners = CoreAnimation.CACornerMask.MinXMinYCorner | CoreAnimation.CACornerMask.MinXMaxYCorner;
            paidStatusContainer.Layer.CornerRadius = 5;
            paidStatusContainer.Hidden = true;
            containerView.Add(paidStatusContainer);

            statusIndicatorImage = new UIImageView(CGRect.Empty);
            statusIndicatorImage.TranslatesAutoresizingMaskIntoConstraints = false;
            statusIndicatorImage.Image = UIImage.FromBundle(ImageNames.PAID);
            paidStatusContainer.Add(statusIndicatorImage);

            paidStatusLabel = new UILabel(CGRect.Empty);
            paidStatusLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            paidStatusLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            paidStatusLabel.Font = UIFont.SystemFontOfSize(16);
            paidStatusContainer.Add(paidStatusLabel);

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
            membershipImage.Image = UIImage.FromBundle(ImageNames.MEMBER);
            membershipNameContainer.Add(membershipImage);

            membershipNameLabel = new UILabel(CGRect.Empty);
            membershipNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            membershipNameLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            membershipNameLabel.Font = UIFont.SystemFontOfSize(16);
            membershipNameContainer.Add(membershipNameLabel);

            payButton = new UIButton(CGRect.Empty);
            payButton.TranslatesAutoresizingMaskIntoConstraints = false;
            payButton.SetTitle("PAY", UIControlState.Normal);
            payButton.TitleLabel.TextColor = UIColor.White;
            payButton.BackgroundColor = Colors.APP_BASE_COLOR.ToPlatformColor();
            payButton.Font = UIFont.BoldSystemFontOfSize(18);

            containerView.Add(payButton);

            payButton.Hidden = false;

            amountLabel = new UILabel(CGRect.Empty);
            amountLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            amountLabel.TextColor = UIColor.FromRGB(2.0f / 255.0f, 20.0f / 255.0f, 61.0f / 255.0f);
            amountLabel.Font = UIFont.BoldSystemFontOfSize(30);
            containerView.Add(amountLabel);

            //remainingBalanceLabel = new UILabel(CGRect.Empty);
            //remainingBalanceLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            //remainingBalanceLabel.TextColor = UIColor.White;
            //remainingBalanceLabel.BackgroundColor = UIColor.Red;
            //remainingBalanceLabel.Font = UIFont.BoldSystemFontOfSize(15);
            //remainingBalanceLabel.Layer.CornerRadius = 5;
            //remainingBalanceLabel.Layer.MasksToBounds = true;
            //containerView.Add(remainingBalanceLabel);

            containerView.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
            containerView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
            containerView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            containerView.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;

            statusIndicatorView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor).Active = true;
            statusIndicatorView.TopAnchor.ConstraintEqualTo(containerView.TopAnchor).Active = true;
            statusIndicatorView.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor).Active = true;
            statusIndicatorView.WidthAnchor.ConstraintEqualTo(6).Active = true;

            checkoutIdLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 35).Active = true;
            checkoutIdLabel.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, constant: 25).Active = true;

            customerNameLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 35).Active = true;
            customerNameLabel.TopAnchor.ConstraintEqualTo(checkoutIdLabel.BottomAnchor, constant: 5).Active = true;

            serviceInfoLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 35).Active = true;
            serviceInfoLabel.TopAnchor.ConstraintEqualTo(customerNameLabel.BottomAnchor, constant: 10).Active = true;
            serviceInfoLabel.TrailingAnchor.ConstraintEqualTo(amountLabel.LeadingAnchor, constant: -20).Active = true;
            serviceInfoLabel.SetContentCompressionResistancePriority(249, UILayoutConstraintAxis.Horizontal);
            serviceInfoLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

            checkInAndOutTimingLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorView.TrailingAnchor, constant: 35).Active = true;
            checkInAndOutTimingLabel.TopAnchor.ConstraintEqualTo(serviceInfoLabel.BottomAnchor, constant: 10).Active = true;
            checkInAndOutTimingLabel.HeightAnchor.ConstraintEqualTo(45).Active = true;
            checkInAndOutTimingLabel.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor, constant: -25).Active = true;

            paidStatusContainer.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor).Active = true;
            paidStatusContainer.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, constant: 20).Active = true;
            paidStatusContainer.HeightAnchor.ConstraintEqualTo(40).Active = true;

            payButton.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, constant: -20).Active = true;
            payButton.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor, constant: -20).Active = true;
            payButton.HeightAnchor.ConstraintEqualTo(40).Active = true;
            payButton.WidthAnchor.ConstraintEqualTo(100).Active = true;

            statusIndicatorImage.LeadingAnchor.ConstraintEqualTo(paidStatusContainer.LeadingAnchor, constant: 12).Active = true;
            statusIndicatorImage.HeightAnchor.ConstraintEqualTo(24).Active = true;
            statusIndicatorImage.WidthAnchor.ConstraintEqualTo(24).Active = true;
            statusIndicatorImage.CenterYAnchor.ConstraintEqualTo(paidStatusContainer.CenterYAnchor).Active = true;

            paidStatusLabel.LeadingAnchor.ConstraintEqualTo(statusIndicatorImage.TrailingAnchor, constant: 16).Active = true;
            paidStatusLabel.TrailingAnchor.ConstraintEqualTo(paidStatusContainer.TrailingAnchor, constant: -20).Active = true;
            paidStatusLabel.CenterYAnchor.ConstraintEqualTo(paidStatusContainer.CenterYAnchor).Active = true;

            membershipNameContainer.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor).Active = true;
            membershipNameContainer.HeightAnchor.ConstraintEqualTo(40).Active = true;
            membershipContainerTopConstraintToBottomOfPaid = membershipNameContainer.TopAnchor.ConstraintEqualTo(paidStatusContainer.BottomAnchor, constant: 8);
            membershipContainerTopConstraintToTopOfParent = membershipNameContainer.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, constant: 20);

            membershipImage.LeadingAnchor.ConstraintEqualTo(membershipNameContainer.LeadingAnchor, constant: 5).Active = true;
            membershipImage.HeightAnchor.ConstraintEqualTo(30).Active = true;
            membershipImage.WidthAnchor.ConstraintEqualTo(30).Active = true;
            membershipImage.CenterYAnchor.ConstraintEqualTo(membershipNameContainer.CenterYAnchor).Active = true;

            membershipNameLabel.LeadingAnchor.ConstraintEqualTo(membershipImage.TrailingAnchor, constant: 14).Active = true;
            membershipNameLabel.TrailingAnchor.ConstraintEqualTo(membershipNameContainer.TrailingAnchor, constant: -20).Active = true;
            membershipNameLabel.CenterYAnchor.ConstraintEqualTo(membershipNameContainer.CenterYAnchor).Active = true;

            amountLabel.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, constant: -100).Active = true;
            amountLabel.TopAnchor.ConstraintEqualTo(containerView.CenterYAnchor).Active = true;

            //remainingBalanceLabel.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, constant: -50).Active = true;
            //remainingBalanceLabel.TopAnchor.ConstraintEqualTo(amountLabel.BottomAnchor, constant: 10).Active = true;
            //remainingBalanceLabel.HeightAnchor.ConstraintEqualTo(30).Active = true;

            //Clicks
            payButton.TouchUpInside += (s, e) => pay?.Invoke(checkout);
        }

        public void SetupData(Checkout checkout, bool isPayOptionNeeded = false, Action<Checkout> pay = null)
        {
            if (!checkout.ColorCode.IsEmpty())
                statusIndicatorView.BackgroundColor = ColorConverters.FromHex(checkout.ColorCode).ToPlatformColor();
            checkoutIdLabel.Text = checkout.ID.ToString();
            customerNameLabel.Text = checkout.CustomerFirstName + " " + checkout.CustomerLastName;
            serviceInfoLabel.Text = checkout.VehicleMake + "/" + checkout.VehicleModel + "/" + checkout.VehicleColor + "\n" + "Services: " + checkout.Services;
            if (checkout.AdditionalServices is not null)
                serviceInfoLabel.Text += "\n" + "Additional Services: " + checkout.AdditionalServices;
            checkInAndOutTimingLabel.Text = "  Check in " + checkout.CheckinTime + " - " + "Check out " + checkout.CheckoutTime + "  ";
            //statusIndicatorImage.Image = new UIImage();
            if (checkout.PaymentStatus.Equals("Success"))
            {
                paidStatusLabel.Text = "Paid";
                paidStatusContainer.Hidden = false;

                membershipContainerTopConstraintToTopOfParent.Active = false;
                membershipContainerTopConstraintToBottomOfPaid.Active = true;
            }
            else
            {
                paidStatusLabel.Text = string.Empty;
                paidStatusContainer.Hidden = true;

                membershipContainerTopConstraintToBottomOfPaid.Active = false;
                membershipContainerTopConstraintToTopOfParent.Active = true;
            }

            if (string.IsNullOrEmpty(checkout.MembershipName) || string.IsNullOrWhiteSpace(checkout.MembershipName))
            {
                membershipNameContainer.Hidden = true;
            }
            else
            {
                membershipNameContainer.Hidden = false;
                membershipNameLabel.Text = checkout.MembershipName;
            }

            if (isPayOptionNeeded)
            {
                //payButton.Hidden = !checkout.PaymentStatus.Equals("Success") ? false : true;
            }

            amountLabel.Text = "$" + checkout.Cost;
            //remainingBalanceLabel.Text = "    Remaining Bal. $15    ";

            this.checkout = checkout;
            this.pay = pay;
        }
    }
}