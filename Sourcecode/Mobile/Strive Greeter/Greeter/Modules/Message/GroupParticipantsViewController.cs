using UIKit;

namespace Greeter.Modules.Message
{
    public partial class GroupParticipantsViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupNavigationItem();
        }

        void SetupView()
        {
            View.BackgroundColor = UIColor.White;
        }

        void SetupNavigationItem()
        {
            Title = "New York Branch I";

        }
    }
}