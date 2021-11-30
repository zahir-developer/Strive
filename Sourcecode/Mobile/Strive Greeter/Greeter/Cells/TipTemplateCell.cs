using System;
using CoreGraphics;
using Greeter.Common;
using Greeter.Extensions;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Cells
{
    public class TipTemplateCell : UICollectionViewCell
    {
        UILabel tipTemplateLabel;
        UIView containerView;

        Action<int> action;

        int pos;

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
            containerView = new UIView(CGRect.Empty);
            containerView.TranslatesAutoresizingMaskIntoConstraints = false;
            containerView.MakecardView();
            ContentView.Add(containerView);

            tipTemplateLabel = new UILabel(CGRect.Empty);
            tipTemplateLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            //tipTemplateLabel.TextColor = UIColor.FromRGB(253.0f / 255.0f, 57.0f / 255.0f, 122.0f / 255.0f);
            tipTemplateLabel.TextColor = UIColor.White;
            tipTemplateLabel.Font = UIFont.BoldSystemFontOfSize(24);
            containerView.Add(tipTemplateLabel);

            UpdateContainerColors(ContentView, tipTemplateLabel, false);

            containerView.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
            containerView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
            containerView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            containerView.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;

            tipTemplateLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, 5).Active = true;
            tipTemplateLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -5).Active = true;
            //tipTemplateLabel.CenterXAnchor.ConstraintEqualTo(ContentView.CenterXAnchor, 0).Active = true;
            //tipTemplateLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor, 0).Active = true;
            tipTemplateLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, 5).Active = true;
            tipTemplateLabel.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -5).Active = true;

            var tapGesture = new UITapGestureRecognizer(ContainerTap);
            containerView.AddGestureRecognizer(tapGesture);
        }

        void ContainerTap()
        {
            //if (containerView.BackgroundColor == UIColor.White)
            //{
            //    UpdateSelectedColor(containerView, tipTemplateLabel);
            //}
            //else
            //{
            //    UpdateUnSelectedColor(containerView, tipTemplateLabel);
            //}

            action.Invoke(pos);
        }

        internal void UpdateTipTemplate(string tipTempateText, Action<int> action, bool isSelected, int pos)
        {
            var isNumeric = int.TryParse(tipTempateText, out _);
            if (isNumeric)
            {
                tipTemplateLabel.Text = tipTempateText + "%";
            }
            else
            {
                tipTemplateLabel.Text = tipTempateText;
            }
            UpdateContainerColors(containerView, tipTemplateLabel, isSelected);
            this.action = action;
            this.pos = pos;
        }

        void UpdateContainerColors(UIView containerView, UILabel lbl, bool isSelected)
        {
            if (isSelected)
            {
                UpdateSelectedColor(containerView, lbl);
            }
            else
            {
                UpdateUnSelectedColor(containerView, lbl);
            }
        }

        void UpdateUnSelectedColor(UIView containerView, UILabel lbl)
        {
            containerView.BackgroundColor = UIColor.White;
            lbl.TextColor = Colors.APP_BASE_COLOR.ToPlatformColor();
        }

        void UpdateSelectedColor(UIView containerView, UILabel lbl)
        {
            containerView.BackgroundColor = Colors.APP_BASE_COLOR.ToPlatformColor();
            lbl.TextColor = UIColor.White;
        }
    }
}
