using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger.Chat
{
    public partial class MessageOutgoingCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MessageOutgoingCell");
        public static readonly UINib Nib;

        static MessageOutgoingCell()
        {
            Nib = UINib.FromName("MessageOutgoingCell", NSBundle.MainBundle);
        }

        protected MessageOutgoingCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            var bubbleBackgroundView = new UIView(CGRect.Empty);
            bubbleBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            bubbleBackgroundView.BackgroundColor = UIColor.FromRGB(12.0f / 255.0f, 78.0f / 255.0f, 71.0f / 255.0f);
            bubbleBackgroundView.Layer.CornerRadius = 12;
            bubbleBackgroundView.Layer.MaskedCorners = CACornerMask.MinXMinYCorner | CACornerMask.MaxXMinYCorner | CACornerMask.MinXMaxYCorner;
            ContentView.Add(bubbleBackgroundView);

            var messageLabel = new UILabel(CGRect.Empty);
            messageLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            messageLabel.Text = "Hai";
            messageLabel.Lines = -1;
            messageLabel.TextColor = UIColor.White;
            messageLabel.Font = UIFont.SystemFontOfSize(18);
            bubbleBackgroundView.Add(messageLabel);

            var messageTimeLabel = new UILabel(CGRect.Empty);
            messageTimeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            messageTimeLabel.Text = "11.15 AM | Oct 19";
            messageTimeLabel.Font = UIFont.SystemFontOfSize(14);
            messageTimeLabel.TextColor = UIColor.Gray;
            messageTimeLabel.TextAlignment = UITextAlignment.Right;
            ContentView.Add(messageTimeLabel);

            bubbleBackgroundView.LeadingAnchor.ConstraintGreaterThanOrEqualTo(ContentView.LeadingAnchor, constant: 150).Active = true;
            bubbleBackgroundView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            bubbleBackgroundView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, constant: 10).Active = true;

            messageLabel.LeadingAnchor.ConstraintEqualTo(bubbleBackgroundView.LeadingAnchor, constant: 20).Active = true;
            messageLabel.TrailingAnchor.ConstraintEqualTo(bubbleBackgroundView.TrailingAnchor, constant: -20).Active = true;
            messageLabel.TopAnchor.ConstraintEqualTo(bubbleBackgroundView.TopAnchor, constant: 20).Active = true;
            messageLabel.BottomAnchor.ConstraintEqualTo(bubbleBackgroundView.BottomAnchor, constant: -20).Active = true;

            messageTimeLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 30).Active = true;
            messageTimeLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            messageTimeLabel.TopAnchor.ConstraintEqualTo(bubbleBackgroundView.BottomAnchor, constant: 10).Active = true;
            messageTimeLabel.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, constant: -10).Active = true;
        }
    }
}
