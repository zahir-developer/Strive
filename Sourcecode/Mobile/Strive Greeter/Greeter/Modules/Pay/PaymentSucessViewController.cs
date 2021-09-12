// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class PaymentSucessViewController : BaseViewController
    {
        // Data
        const string SCREEN_TITLE = "Pay";

        public long TicketID;
        public string Make;
        public string Model;
        public string Color;
        public string ServiceName;
        public string AdditionalServiceName;
        public float Amount;
        public string CustomerName;
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

            var mutableAttributedString = new NSMutableAttributedString(
               "Services: ",
               UIFont.SystemFontOfSize(18));
            var attributedString = new NSAttributedString(
               ServiceName,
               font: UIFont.SystemFontOfSize(18, UIFontWeight.Semibold)
           );

            mutableAttributedString.Append(attributedString);
            lblService.AttributedText = mutableAttributedString;

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

        async Task Print()
        {
            try
            {
                ShowActivityIndicator();

                var subject = "Wash Receipt";

                var body = "<p>Ticket Number : </p>" + TicketID + "<br /><br />";

                //if (Service.Job.ClientId != 0 && Service.Job.ClientId is not null)
                //{
                //    body += "<p>Customer Details : </p>" + ""
                //        + "<p>Customer Name - " + "Jimmy" + "</p><br />";
                //}

                if (!string.IsNullOrEmpty(CustomerName))
                {
                    body += "<p>Customer Details : </p>" + ""
                        + "<p>Customer Name - " + CustomerName + "</p><br />";
                }

                body += "<p>Vehicle Details : </p>" +
                     "<p>Make - " + Make + "</p>" +
                    "<p>Model - " + Model + "</p>" +
                     "<p>Color - " + Color + "</p><br />" +
                     "<p>Services : " + "</p>";

                //var totalAmt = 0f;
                //for (int i = 0; i < Service.JobItems.Count; i++)
                //{
                //    var job = Service.JobItems[i];
                //    var price = job.Price.ToString();
                //    if ((job.Price % 1) == 0)
                //    {
                //        price += ":00";
                //    }
                //    else
                //    {
                //        var values = price.Split(".");
                //        price = (int)job.Price + ":" + values[1];
                //    }

                //    body += "<p>" + job.SeriveName + " - " + "$" + price + "</p>";
                //    //totalAmt += job.Price;
                //}

                if (!string.IsNullOrEmpty(ServiceName))
                {
                    body += "<p>" + CustomerName + "</p>";
                }

                if (!string.IsNullOrEmpty(AdditionalServiceName))
                {
                    body += "<p>" + AdditionalServiceName + "</p>";
                }

                body += "<br/ ><p>" + "Total Amount : " + Amount.ToString() + "</p>";

                //body = "<div>Something</div>";

                Debug.WriteLine("Email Body :" + body);

                Print(body);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened and the reason is : " + ex.Message);
                HideActivityIndicator();
            }
        }
    }
}