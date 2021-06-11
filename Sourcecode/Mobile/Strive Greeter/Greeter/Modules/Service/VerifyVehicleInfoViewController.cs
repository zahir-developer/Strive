// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using Greeter.Extensions;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class VerifyVehicleInfoViewController : BaseViewController
    {
        public VerifyVehicleInfoViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Init();

            //Clicks
            btnConfirm.TouchUpInside += delegate
            {
                NavigateToVEmailScreen();
            };
        }

        void Init()
        {
            viewCustName.MakecardView();
            viewVehicle.MakecardView();
            viewBarcode.MakecardView();
            viewType.MakecardView();
        }

        void NavigateToVEmailScreen()
        {
            UIViewController vc = GetViewController(GetHomeStorybpard(), nameof(EmailViewController));

            NavigateToWithAnim(vc);
        }
    }
}
