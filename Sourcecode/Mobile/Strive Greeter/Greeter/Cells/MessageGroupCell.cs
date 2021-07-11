using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Greeter.Cells
{
    public class MessageGroupCell: UITableViewCell
    {
        public static readonly NSString Key = new("MessageGroupCell");

        UILabel groupIntialLabel;
        UILabel groupNameLabel;

        public MessageGroupCell(IntPtr p) : base(p)
        {
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            groupIntialLabel = new UILabel(CGRect.Empty);
            groupIntialLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            groupIntialLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            groupIntialLabel.TextColor = UIColor.Black;
            groupIntialLabel.TextAlignment = UITextAlignment.Center;
            groupIntialLabel.Layer.CornerRadius = 25;
            groupIntialLabel.Layer.MasksToBounds = true;
            groupIntialLabel.BackgroundColor = UIColor.FromRGB(162.0f / 255.0f, 229.0f / 255.0f, 221.0f / 255.0f);
            ContentView.Add(groupIntialLabel);

            groupNameLabel = new UILabel(CGRect.Empty);
            groupNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            groupNameLabel.Font = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
            groupNameLabel.TextColor = UIColor.Black;
            ContentView.Add(groupNameLabel);

            groupIntialLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 30).Active = true;
            groupIntialLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            groupIntialLabel.WidthAnchor.ConstraintEqualTo(50).Active = true;
            groupIntialLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            groupNameLabel.LeadingAnchor.ConstraintEqualTo(groupIntialLabel.TrailingAnchor, constant: 20).Active = true;
            groupNameLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            groupNameLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
        }

        //TODO Pass Real time data here and set later
        public void SetupData()
        {
            groupIntialLabel.Text = "NY";
            groupNameLabel.Text = "New York Branch I";
        }
    }
}
