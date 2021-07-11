using System;
using CoreGraphics;
using Foundation;
using Greeter.Common;
using UIKit;

namespace Greeter.Cells
{
    public class ContactCell : UITableViewCell
    {
        public static readonly NSString Key = new("ContactCell");

        UILabel contactIntialLabel;
        UILabel contactNameLabel;
        UIImageView selectionImageView;

        public ContactCell(IntPtr p) : base(p)
        {
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            contactIntialLabel = new UILabel(CGRect.Empty);
            contactIntialLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            contactIntialLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            contactIntialLabel.TextColor = UIColor.Black;
            contactIntialLabel.TextAlignment = UITextAlignment.Center;
            contactIntialLabel.Layer.CornerRadius = 25;
            contactIntialLabel.Layer.MasksToBounds = true;
            contactIntialLabel.BackgroundColor = UIColor.FromRGB(162.0f / 255.0f, 229.0f / 255.0f, 221.0f / 255.0f);
            ContentView.Add(contactIntialLabel);

            contactNameLabel = new UILabel(CGRect.Empty);
            contactNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            contactNameLabel.Font = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
            contactNameLabel.TextColor = UIColor.Black;
            ContentView.Add(contactNameLabel);

            selectionImageView = new UIImageView(CGRect.Empty);
            selectionImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            selectionImageView.Image = UIImage.FromBundle(ImageNames.TICK);
            ContentView.Add(selectionImageView);

            contactIntialLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 30).Active = true;
            contactIntialLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            contactIntialLabel.WidthAnchor.ConstraintEqualTo(50).Active = true;
            contactIntialLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            contactNameLabel.LeadingAnchor.ConstraintEqualTo(contactIntialLabel.TrailingAnchor, constant: 20).Active = true;
            contactNameLabel.TrailingAnchor.ConstraintEqualTo(selectionImageView.LeadingAnchor, constant: -10).Active = true;
            contactNameLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;

            selectionImageView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            selectionImageView.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            selectionImageView.WidthAnchor.ConstraintEqualTo(20).Active = true;
            selectionImageView.HeightAnchor.ConstraintEqualTo(20).Active = true;
        }

        //TODO Pass Real time data here and set later
        public void SetupData()
        {
            contactIntialLabel.Text = "WH";
            contactNameLabel.Text = "William Hoeger";

            ContentView.BackgroundColor = UIColor.FromRGB(225.0f / 255.0f, 255.0f / 255.0f, 251.0f / 255.0f);
        }
    }
}