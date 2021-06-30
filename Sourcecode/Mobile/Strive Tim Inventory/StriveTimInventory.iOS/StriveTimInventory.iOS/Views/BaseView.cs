using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class BaseView : UIViewController
    {
        public BaseView() : base("BaseView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

