using System;
using CoreGraphics;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using MapKit;
using UIKit;

namespace Greeter.Modules.Home
{
    [Register("WashStationAnnotation")]
    public class WashStationAnnotationView : MKAnnotationView
    {
        UILabel stationNameLabel;
        UILabel timeLabel;
        UILabel statusLabel;

        public WashStationAnnotationView(IntPtr handle) : base(handle)
        {
            SetupView();
        }

        void SetupView()
        {
            BackgroundColor = UIColor.Clear;
            Frame = new CGRect(0, 0, 260, 180);
            CenterOffset = new CGPoint(x: 0, y: -Frame.Size.Height / 2);

            var outerCircle = new UIView(CGRect.Empty);
            outerCircle.TranslatesAutoresizingMaskIntoConstraints = false;
            outerCircle.BackgroundColor = UIColor.FromRGBA(153.0f / 255.0f, 254.0f / 255.0f, 243.0f / 255.0f, 0.3f);
            outerCircle.Layer.CornerRadius = 67;
            outerCircle.Layer.MasksToBounds = true;
            Add(outerCircle);

            var innerCircle1 = new UIView(CGRect.Empty);
            innerCircle1.TranslatesAutoresizingMaskIntoConstraints = false;
            innerCircle1.BackgroundColor = UIColor.FromRGBA(153.0f / 255.0f, 254.0f / 255.0f, 243.0f / 255.0f, 0.3f);
            innerCircle1.Layer.CornerRadius = 34;
            innerCircle1.Layer.MasksToBounds = true;
            outerCircle.Add(innerCircle1);

            var innerCircle2 = new UIView(CGRect.Empty);
            innerCircle2.TranslatesAutoresizingMaskIntoConstraints = false;
            innerCircle2.BackgroundColor = UIColor.FromRGBA(153.0f / 255.0f, 254.0f / 255.0f, 243.0f / 255.0f, 0.3f);
            innerCircle2.Layer.CornerRadius = 22;
            innerCircle2.Layer.MasksToBounds = true;
            outerCircle.Add(innerCircle2);

            var centerCircle = new UIView(CGRect.Empty);
            centerCircle.TranslatesAutoresizingMaskIntoConstraints = false;
            centerCircle.BackgroundColor = UIColor.FromRGB(253.0f / 255.0f, 204.0f / 255.0f, 83.0f / 255.0f);
            centerCircle.Layer.CornerRadius = 9;
            centerCircle.Layer.MasksToBounds = true;
            outerCircle.Add(centerCircle);

            var infoContainerView = new PinView(CGRect.Empty);
            infoContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            infoContainerView.Layer.ShadowColor = UIColor.Black.CGColor;
            infoContainerView.Layer.ShadowOffset = new CGSize(0f, 3f);
            infoContainerView.Layer.ShadowOpacity = 0.5f;
            infoContainerView.Layer.ShadowRadius = 3;
            Add(infoContainerView);

            stationNameLabel = new(CGRect.Empty);
            stationNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            stationNameLabel.TextColor = UIColor.FromRGB(42.0f / 255.0f, 193.0f / 255.0f, 177.0f / 255.0f);
            stationNameLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);
            stationNameLabel.Lines = 2;
            infoContainerView.Add(stationNameLabel);

            statusLabel = new(CGRect.Empty);
            statusLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            statusLabel.TextColor = UIColor.White;
            statusLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);
            statusLabel.Lines = 2;
            infoContainerView.Add(statusLabel);

            var timeContainerView = new UIView(CGRect.Empty);
            timeContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            timeContainerView.BackgroundColor = UIColor.FromRGB(253.0f / 255.0f, 204.0f / 255.0f, 83.0f / 255.0f);
            timeContainerView.Layer.CornerRadius = 5;
            timeContainerView.ClipsToBounds = true;
            infoContainerView.Add(timeContainerView);

            var carImageView = new UIImageView(CGRect.Empty);
            carImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            carImageView.Image = UIImage.FromBundle(ImageNames.CAR);
            timeContainerView.Add(carImageView);

            timeLabel = new(CGRect.Empty);
            timeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            timeLabel.TextColor = UIColor.FromRGB(0f / 255.0f, 46.0f / 255.0f, 41.0f / 255.0f);
            timeLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);
            timeContainerView.Add(timeLabel);

            outerCircle.TopAnchor.ConstraintEqualTo(TopAnchor).Active = true;
            outerCircle.CenterXAnchor.ConstraintEqualTo(CenterXAnchor).Active = true;
            outerCircle.HeightAnchor.ConstraintEqualTo(134).Active = true;
            outerCircle.WidthAnchor.ConstraintEqualTo(134).Active = true;

            innerCircle1.CenterXAnchor.ConstraintEqualTo(outerCircle.CenterXAnchor).Active = true;
            innerCircle1.CenterYAnchor.ConstraintEqualTo(outerCircle.CenterYAnchor).Active = true;
            innerCircle1.HeightAnchor.ConstraintEqualTo(68).Active = true;
            innerCircle1.WidthAnchor.ConstraintEqualTo(68).Active = true;

            innerCircle2.CenterXAnchor.ConstraintEqualTo(outerCircle.CenterXAnchor).Active = true;
            innerCircle2.CenterYAnchor.ConstraintEqualTo(outerCircle.CenterYAnchor).Active = true;
            innerCircle2.HeightAnchor.ConstraintEqualTo(44).Active = true;
            innerCircle2.WidthAnchor.ConstraintEqualTo(44).Active = true;

            centerCircle.CenterXAnchor.ConstraintEqualTo(outerCircle.CenterXAnchor).Active = true;
            centerCircle.CenterYAnchor.ConstraintEqualTo(outerCircle.CenterYAnchor).Active = true;
            centerCircle.HeightAnchor.ConstraintEqualTo(18).Active = true;
            centerCircle.WidthAnchor.ConstraintEqualTo(18).Active = true;

            infoContainerView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor).Active = true;
            infoContainerView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor).Active = true;
            infoContainerView.BottomAnchor.ConstraintEqualTo(BottomAnchor).Active = true;
            infoContainerView.TopAnchor.ConstraintEqualTo(centerCircle.CenterYAnchor).Active = true;

            stationNameLabel.LeadingAnchor.ConstraintEqualTo(infoContainerView.LeadingAnchor, constant: 16).Active = true;
            stationNameLabel.TrailingAnchor.ConstraintEqualTo(timeContainerView.LeadingAnchor, constant: -16).Active = true;
            stationNameLabel.TopAnchor.ConstraintEqualTo(infoContainerView.TopAnchor, constant: 20).Active = true;
            //stationNameLabel.BottomAnchor.ConstraintLessThanOrEqualTo(infoContainerView.BottomAnchor, constant: -8).Active = true;
            //stationNameLabel.CenterYAnchor.ConstraintEqualTo(infoContainerView.CenterYAnchor, constant: 5).Active = true;
            stationNameLabel.SetContentCompressionResistancePriority(249, UILayoutConstraintAxis.Horizontal);
            stationNameLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

            statusLabel.LeadingAnchor.ConstraintEqualTo(stationNameLabel.LeadingAnchor, constant: 0).Active = true;
            statusLabel.TrailingAnchor.ConstraintEqualTo(timeContainerView.TrailingAnchor, constant: 0).Active = true;
            statusLabel.TopAnchor.ConstraintEqualTo(stationNameLabel.BottomAnchor, constant: 10).Active = true;
            statusLabel.BottomAnchor.ConstraintLessThanOrEqualTo(infoContainerView.BottomAnchor, constant: -8).Active = true;

            timeContainerView.TrailingAnchor.ConstraintEqualTo(infoContainerView.TrailingAnchor, constant: -16).Active = true;
            timeContainerView.TopAnchor.ConstraintEqualTo(stationNameLabel.TopAnchor, constant: 0).Active = true;
            timeContainerView.HeightAnchor.ConstraintEqualTo(40).Active = true;

            carImageView.LeadingAnchor.ConstraintEqualTo(timeContainerView.LeadingAnchor, constant: 12).Active = true;
            carImageView.TrailingAnchor.ConstraintEqualTo(timeLabel.LeadingAnchor, constant: -6).Active = true;
            carImageView.CenterYAnchor.ConstraintEqualTo(timeContainerView.CenterYAnchor).Active = true;
            carImageView.HeightAnchor.ConstraintEqualTo(20).Active = true;
            carImageView.WidthAnchor.ConstraintEqualTo(20).Active = true;

            timeLabel.TrailingAnchor.ConstraintEqualTo(timeContainerView.TrailingAnchor, constant: -12).Active = true;
            timeLabel.CenterYAnchor.ConstraintEqualTo(timeContainerView.CenterYAnchor).Active = true;
        }

        nfloat GetContentHeight(NSString nSString)
        {
            var constraintRect = new CGSize(width: 260, height: double.PositiveInfinity);
            var boundingBox = nSString.GetBoundingRect(
                constraintRect,
                options: NSStringDrawingOptions.UsesLineFragmentOrigin,
                attributes: new UIStringAttributes {
                    Font = UIFont.BoldSystemFontOfSize(18)
                },
                null);
            return boundingBox.Height;
        }

        internal void SetupData(Location location)
        {
            stationNameLabel.Text = location.Name;
            timeLabel.Text = $"{location.WashTimeMinutes}Mins";

            //if (string.IsNullOrEmpty(location.StoreStatus))
            //{
            //    statusLabel.Text = "Closed";
            //}
            //else
                statusLabel.Text = location.StoreStatus;
        }
    }

    class PinView : UIView
    {
        public PinView(CGRect frame) : base(frame: frame)
        {
            BackgroundColor = UIColor.Clear;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var path = new UIBezierPath();
            path.MoveTo(new CGPoint(rect.GetMinX(), y: rect.GetMinY() + 10));
            path.AddLineTo(new CGPoint(rect.GetMidX() - 10, y: rect.GetMinY() + 10));
            path.AddLineTo(new CGPoint(rect.GetMidX(), y: rect.GetMinY()));
            path.AddLineTo(new CGPoint(rect.GetMidX() + 10, y: rect.GetMinY() + 10));
            path.AddLineTo(new CGPoint(rect.GetMaxX(), y: rect.GetMinY() + 10));
            path.AddLineTo(new CGPoint(rect.GetMaxX(), y: rect.GetMaxY()));
            path.AddLineTo(new CGPoint(rect.GetMinX(), y: rect.GetMaxY()));
            path.ClosePath();

            UIColor.FromRGB(0.0f / 255.0f, 88.0f / 255.0f, 79.0f / 255.0f).SetColor();
            path.Fill();
        }
    }
}