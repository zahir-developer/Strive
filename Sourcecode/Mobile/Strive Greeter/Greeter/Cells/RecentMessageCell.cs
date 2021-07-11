using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Greeter.Cells
{
    public class RecentMessageCell : UITableViewCell
    {
        public static readonly NSString Key = new("RecentMessageCell");

        UILabel userIntialLabel;
        UILabel userNameLabel;
        UILabel dateTimeLabel;
        UILabel messageContentLabel;

        public RecentMessageCell(IntPtr p) : base(p)
        {
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            userIntialLabel = new UILabel(CGRect.Empty);
            userIntialLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            userIntialLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            userIntialLabel.TextColor = UIColor.Black;
            userIntialLabel.TextAlignment = UITextAlignment.Center;
            userIntialLabel.Layer.CornerRadius = 25;
            userIntialLabel.Layer.MasksToBounds = true;
            userIntialLabel.BackgroundColor = UIColor.FromRGB(162.0f / 255.0f, 229.0f / 255.0f, 221.0f / 255.0f);
            ContentView.Add(userIntialLabel);

            userNameLabel = new UILabel(CGRect.Empty);
            userNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            userNameLabel.Font = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
            userNameLabel.TextColor = UIColor.Black;
            ContentView.Add(userNameLabel);

            dateTimeLabel = new UILabel(CGRect.Empty);
            dateTimeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            dateTimeLabel.Font = UIFont.SystemFontOfSize(14);
            dateTimeLabel.TextColor = UIColor.Gray;
            ContentView.Add(dateTimeLabel);

            messageContentLabel = new UILabel(CGRect.Empty);
            messageContentLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            messageContentLabel.Font = UIFont.SystemFontOfSize(15);
            messageContentLabel.TextColor = UIColor.Gray;
            ContentView.Add(messageContentLabel);

            userIntialLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 30).Active = true;
            userIntialLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            userIntialLabel.WidthAnchor.ConstraintEqualTo(50).Active = true;
            userIntialLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            userNameLabel.LeadingAnchor.ConstraintEqualTo(userIntialLabel.TrailingAnchor, constant: 20).Active = true;
            userNameLabel.TrailingAnchor.ConstraintEqualTo(dateTimeLabel.LeadingAnchor, constant: -10).Active = true;
            userNameLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, constant: 15).Active = true;

            dateTimeLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            dateTimeLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, constant: 10).Active = true;

            messageContentLabel.LeadingAnchor.ConstraintEqualTo(userIntialLabel.TrailingAnchor, constant: 20).Active = true;
            messageContentLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            messageContentLabel.TopAnchor.ConstraintEqualTo(userNameLabel.BottomAnchor, constant: 3).Active = true;
        }

        //TODO Pass Real time data here and set later
        public void SetupData()
        {
            userIntialLabel.Text = "WH";
            userNameLabel.Text = "William Hoeger";
            dateTimeLabel.Text = "5.18 PM";
            messageContentLabel.Text = "Checkout cashier section";
        }
    }
}