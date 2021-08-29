using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using Greeter.DTOs;
using Greeter.Extensions;
using UIKit;

namespace Greeter.Cells
{
    public class MessageIncomingCell : UITableViewCell
    {
        public static readonly NSString Key = new("MessageIncomingCell");

        UILabel userIntialLabel;
        UILabel userNameLabel;
        UILabel messageLabel;
        UILabel messageTimeLabel;

        public MessageIncomingCell(IntPtr p) : base(p)
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
            userNameLabel.TextAlignment = UITextAlignment.Center;
            userNameLabel.Font = UIFont.SystemFontOfSize(14);
            ContentView.Add(userNameLabel);

            var bubbleBackgroundView = new UIView(CGRect.Empty);
            bubbleBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            bubbleBackgroundView.BackgroundColor = UIColor.FromRGB(220.0f / 255.0f, 220.0f / 255.0f, 220.0f / 255.0f);
            bubbleBackgroundView.Layer.CornerRadius = 12;
            bubbleBackgroundView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner | CACornerMask.MaxXMaxYCorner;
            ContentView.Add(bubbleBackgroundView);

            messageLabel = new UILabel(CGRect.Empty);
            messageLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            messageLabel.Lines = -1;
            messageLabel.TextColor = UIColor.Black;
            messageLabel.Font = UIFont.SystemFontOfSize(18);
            bubbleBackgroundView.Add(messageLabel);

            messageTimeLabel = new UILabel(CGRect.Empty);
            messageTimeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            messageTimeLabel.Font = UIFont.SystemFontOfSize(14);
            messageTimeLabel.TextColor = UIColor.Gray;
            ContentView.Add(messageTimeLabel);

            userIntialLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 30).Active = true;
            userIntialLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, constant: 10).Active = true;
            userIntialLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;
            userIntialLabel.WidthAnchor.ConstraintEqualTo(50).Active = true;

            userNameLabel.TopAnchor.ConstraintEqualTo(userIntialLabel.BottomAnchor, constant: 10).Active = true;
            userNameLabel.CenterXAnchor.ConstraintEqualTo(userIntialLabel.CenterXAnchor).Active = true;
            userNameLabel.WidthAnchor.ConstraintEqualTo(70).Active = true;

            bubbleBackgroundView.LeadingAnchor.ConstraintEqualTo(userIntialLabel.TrailingAnchor, constant: 30).Active = true;
            bubbleBackgroundView.TrailingAnchor.ConstraintLessThanOrEqualTo(ContentView.TrailingAnchor, constant: -150).Active = true;
            bubbleBackgroundView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, constant: 10).Active = true;

            messageLabel.LeadingAnchor.ConstraintEqualTo(bubbleBackgroundView.LeadingAnchor, constant: 20).Active = true;
            messageLabel.TrailingAnchor.ConstraintEqualTo(bubbleBackgroundView.TrailingAnchor, constant: -20).Active = true;
            messageLabel.TopAnchor.ConstraintEqualTo(bubbleBackgroundView.TopAnchor, constant: 20).Active = true;
            messageLabel.BottomAnchor.ConstraintEqualTo(bubbleBackgroundView.BottomAnchor, constant: -20).Active = true;

            messageTimeLabel.LeadingAnchor.ConstraintEqualTo(userIntialLabel.TrailingAnchor, constant: 30).Active = true;
            messageTimeLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            messageTimeLabel.TopAnchor.ConstraintEqualTo(bubbleBackgroundView.BottomAnchor, constant: 10).Active = true;
            messageTimeLabel.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, constant: -10).Active = true;
        }

        public void SetupData(ChatMessage message)
        {
            userIntialLabel.Text = message.SenderFirstName?.Substring(0, 1);
            if (!message.SenderLastName.IsEmpty())
                userIntialLabel.Text += message.SenderLastName?.Substring(0, 1);

            userNameLabel.Text = message.SenderFirstName;
            messageLabel.Text = message.MessageBody;
            if (message.CreatedDate.HasValue)
            {
                messageTimeLabel.Text = message.CreatedDate?.ToString("h:mm tt | MMM yy");
            }
        }
    }
}