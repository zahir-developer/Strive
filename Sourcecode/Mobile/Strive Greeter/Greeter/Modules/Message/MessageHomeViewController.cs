using CoreGraphics;
using Greeter.Common;
using UIKit;

namespace Greeter.Modules.Message
{
    public partial class MessageHomeViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
        }

        void SetupView()
        {
            var backgroundImage = new UIImageView(UIImage.FromBundle(ImageNames.SPLASH_BG));
            backgroundImage.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundImage.ContentMode = UIViewContentMode.ScaleAspectFill;
            View.Add(backgroundImage);

            var backgroundContainerView = new UIView(CGRect.Empty);
            backgroundContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundContainerView.BackgroundColor = UIColor.White;
            backgroundContainerView.Layer.CornerRadius = 8;
            backgroundContainerView.Layer.MasksToBounds = true;
            View.Add(backgroundContainerView);

            backgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            backgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            backgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            backgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            backgroundContainerView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor, constant: 30).Active = true;
            backgroundContainerView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor, constant: -30).Active = true;
            backgroundContainerView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, constant: 30).Active = true;
            backgroundContainerView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, constant: -30).Active = true;
        }
    }
}