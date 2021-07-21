using CoreGraphics;
using Greeter.Common;
using UIKit;

namespace Greeter.Modules.Message
{
    class MoreOptionViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
        }

        void SetupView()
        {
            View.Layer.CornerRadius = 5;
            View.Layer.ShadowColor = UIColor.Black.CGColor;
            View.Layer.ShadowOpacity = 0.3f;
            View.Layer.ShadowOffset = new CGSize(0, 1);

            var createGroupContainer = new UIView(frame: CGRect.Empty);
            createGroupContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            createGroupContainer.AddGestureRecognizer(new UITapGestureRecognizer(AddGroupTapped));
            View.Add(createGroupContainer);

            var createGroupLabel = new UILabel(frame: CGRect.Empty);
            createGroupLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            createGroupLabel.Text = "Create Group";
            createGroupLabel.TextColor = UIColor.FromRGB(95.0f / 255.0f, 98.0f / 255.0f, 129.0f / 255.0f);
            createGroupLabel.Font = UIFont.SystemFontOfSize(15);
            createGroupContainer.Add(createGroupLabel);

            var createGroupImageView = new UIImageView(frame: CGRect.Empty);
            createGroupImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            createGroupImageView.Image = UIImage.FromBundle(ImageNames.ADD_GROUP);
            createGroupImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            createGroupContainer.Add(createGroupImageView);

            var refreshContainer = new UIView(frame: CGRect.Empty);
            refreshContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            refreshContainer.AddGestureRecognizer(new UITapGestureRecognizer(RefreshTapped));
            View.Add(refreshContainer);

            var refreshLabel = new UILabel(frame: CGRect.Empty);
            refreshLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            refreshLabel.Text = "Refresh";
            refreshLabel.TextColor = UIColor.FromRGB(95.0f / 255.0f, 98.0f / 255.0f, 129.0f / 255.0f);
            refreshLabel.Font = UIFont.SystemFontOfSize(15);
            refreshContainer.Add(refreshLabel);

            var refreshImageView = new UIImageView(frame: CGRect.Empty);
            refreshImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            refreshImageView.Image = UIImage.FromBundle(ImageNames.REFRESH);
            refreshImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            refreshContainer.Add(refreshImageView);

            createGroupContainer.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            createGroupContainer.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            createGroupContainer.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;
            createGroupContainer.HeightAnchor.ConstraintEqualTo(50).Active = true;

            createGroupLabel.LeadingAnchor.ConstraintEqualTo(createGroupContainer.LeadingAnchor, constant: 20).Active = true;
            createGroupLabel.TrailingAnchor.ConstraintEqualTo(createGroupImageView.LeadingAnchor, constant: -20).Active = true;
            createGroupLabel.CenterYAnchor.ConstraintEqualTo(createGroupContainer.CenterYAnchor).Active = true;

            createGroupImageView.TrailingAnchor.ConstraintEqualTo(createGroupContainer.TrailingAnchor, constant: -20).Active = true;
            createGroupImageView.CenterYAnchor.ConstraintEqualTo(createGroupContainer.CenterYAnchor).Active = true;
            createGroupImageView.HeightAnchor.ConstraintEqualTo(22).Active = true;
            createGroupImageView.WidthAnchor.ConstraintEqualTo(22).Active = true;

            refreshContainer.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            refreshContainer.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            refreshContainer.TopAnchor.ConstraintEqualTo(createGroupContainer.BottomAnchor).Active = true;
            refreshContainer.HeightAnchor.ConstraintEqualTo(50).Active = true;

            refreshLabel.LeadingAnchor.ConstraintEqualTo(refreshContainer.LeadingAnchor, constant: 20).Active = true;
            refreshLabel.TrailingAnchor.ConstraintEqualTo(refreshImageView.LeadingAnchor, constant: -20).Active = true;
            refreshLabel.CenterYAnchor.ConstraintEqualTo(refreshContainer.CenterYAnchor).Active = true;

            refreshImageView.TrailingAnchor.ConstraintEqualTo(refreshContainer.TrailingAnchor, constant: -20).Active = true;
            refreshImageView.CenterYAnchor.ConstraintEqualTo(refreshContainer.CenterYAnchor).Active = true;
            refreshImageView.HeightAnchor.ConstraintEqualTo(20).Active = true;
            refreshImageView.WidthAnchor.ConstraintEqualTo(20).Active = true;
        }

        void AddGroupTapped()
        {

        }

        void RefreshTapped()
        {

        }
    }
}
