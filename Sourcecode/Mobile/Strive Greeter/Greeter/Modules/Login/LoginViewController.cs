
using Greeter.Common;
using Greeter.Modules.User;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace Greeter.Modules.Login
{
    public partial class LoginViewController : MvxViewController<LoginViewModel>
    {
        UITextField userNameTextField;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //View.BackgroundColor = UIColor.White;

            View?.AddSubview(LoginView());

            //SetupView();
        }

        UIView LoginView()
        {
            var bgImgv = new UIImageView();
            bgImgv.Image = UIImage.FromBundle(ImageNames.SPLASH_BG);

            //bgImgv.frs = View.WidthAnchor;

            //bgImgv.Frame = new CoreGraphics.CGRect(0, 0, View.Frame.Width, View.Frame.Height);

            var margins = View.LayoutMarginsGuide;

            // alignments
            bgImgv.LeadingAnchor.ConstraintEqualTo(margins.LeadingAnchor).Active = true;
            //bgImgv.TrailingAnchor.ConstraintEqualTo(View?.TrailingAnchor, 0).Active = true;
            //bgImgv.TopAnchor.ConstraintEqualTo(View?.TopAnchor, 0).Active = true;
            //bgImgv.BottomAnchor.ConstraintEqualTo(View?.BottomAnchor, 0).Active = true;

            return bgImgv;
        }

        //void SetupView()
        //{
        //    userNameTextField = new UITextField(CGRect.Empty)
        //    {
        //        TranslatesAutoresizingMaskIntoConstraints = false,
        //        Placeholder = "User Id",
        //        Font = UIFont.SystemFontOfSize(15)
        //    };

        //    View.Add(userNameTextField);

        //    userNameTextField.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
        //    userNameTextField.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
        //    userNameTextField.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
        //    userNameTextField.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;
        //}
    }
}