using System;
using CoreGraphics;
using UIKit;

namespace Greeter.Cells
{
    public class TipTemplateCell : UICollectionViewCell
    {
        UILabel tipTemplateLabel;

        public TipTemplateCell()
        {
            //SetupView();
        }

        protected internal TipTemplateCell(IntPtr handle) : base(handle)
        {
            SetupView();
        }

        void SetupView()
        {
            var containerView = new UIView(CGRect.Empty);
            containerView.TranslatesAutoresizingMaskIntoConstraints = false;
            containerView.BackgroundColor = UIColor.Red;
            ContentView.Add(containerView);

            tipTemplateLabel = new UILabel(CGRect.Empty);
            tipTemplateLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            //tipTemplateLabel.TextColor = UIColor.FromRGB(253.0f / 255.0f, 57.0f / 255.0f, 122.0f / 255.0f);
            tipTemplateLabel.TextColor = UIColor.White;
            tipTemplateLabel.Font = UIFont.BoldSystemFontOfSize(24);
            containerView.Add(tipTemplateLabel);

            containerView.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
            containerView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
            containerView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            containerView.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;

            tipTemplateLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, 5).Active = true;
            tipTemplateLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -5).Active = true;
            tipTemplateLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, 5).Active = true;
            tipTemplateLabel.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -5).Active = true;
        }

        internal void UpdateTipTemplate(string tipTempateText)
        {
            tipTemplateLabel.Text = tipTempateText;
        }
    }
}
