using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Employee.MyTicket;
using StriveEmployee.iOS.UIUtils;
using UIKit;

namespace StriveEmployee.iOS.Views.Tickets
{
    public partial class TicketView : MvxViewController<MyTicketViewModel>
    {
        public TicketView() : base("TicketView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Tickets";
        }
    }
}

