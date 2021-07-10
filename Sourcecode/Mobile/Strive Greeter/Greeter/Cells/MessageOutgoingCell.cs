using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Greeter.Cells
{
    public class MessageOutgoingCell : UITableViewCell
    {
        public static readonly NSString Key = new("MessageOutgoingCell");

        public MessageOutgoingCell(IntPtr p) : base(p)
        {
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
            messageLabel.Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
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

        //TODO pass model object here and set real data
        void SetupData()
        {

        }
    }
}