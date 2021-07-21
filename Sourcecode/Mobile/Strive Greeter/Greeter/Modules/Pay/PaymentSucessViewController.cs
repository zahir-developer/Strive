// This file has been autogenerated from a class added in the UI designer.

using System;
using Greeter.Modules.Pay;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class PaymentSucessViewController : UIViewController
    {
        // Data
        const string SCREEN_TITLE = "Pay";

        public long TicketID;
        public string Make;
        public string Model;
        public string Color;
        public string ServiceName;
        public float Amount;
        public bool IsFromNewService = true;

        public PaymentSucessViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Initialise();
            UpdateData();

            lblReceipt.AddGestureRecognizer(new UITapGestureRecognizer(NoReceiptClicked));
        }

        private void UpdateData()
        {
            lblTicketId.Text = TicketID.ToString();
            lblVehicle.Text = $"{Make} {Model} {Color}";
            lblService.Text = ServiceName;
            lblAmount.Text = $"${Amount}";
        }

        void Initialise()
        {
            NavigationController.NavigationBar.Hidden = false;
            Title = SCREEN_TITLE;
        }

        void NoReceiptClicked(UITapGestureRecognizer tap)
        {
            NavigateToServiceHome();
        }

        void NavigateToServiceHome()
        {
            UIViewController vc = null;
            if (IsFromNewService)
               vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 6];
            else
                vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 3];
            NavigationController.PopToViewController(vc, true);
        }
    }
}