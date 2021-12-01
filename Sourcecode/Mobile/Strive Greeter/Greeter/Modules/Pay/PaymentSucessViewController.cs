

// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using MessageUI;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class PaymentSucessViewController : BaseViewController, IMFMailComposeViewControllerDelegate, IEmailDelegate
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
        //public bool IsFromNewService = true;
        public ServiceType ServiceType;
        public CreateServiceRequest Service;
        //public bool IsMembershipService;
        public string CardNumber;

        //const string HTML_TEMPLATE = "<!DOCTYPE html><html><head><link href=\"https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css\" rel=\"stylesheet\" /></head><body><div id = \"print-section\" >< div class=\"row\"><div class=\"col-12\"><p>Client: &nbsp;&nbsp;{{ClientName}}</ p ></ div ></ div >< div class= \"row\" >< div class= \"col-4\" > Vehicle: &nbsp;{ { VehicleModel} }</ div >< div class= \"col-4 text-center\" >{ { VehicleMake} }</ div >< div class= \"col-4 text-right\" >{ { VehicleColor} }</ div ></ div >< div class= \"row\" >< div class= \"col-6\" ></ div >< div class= \"col-6 text-right\" >< div class= \"row\" >< div class= \"col-12\" >< P class= \"text-center\" >< strong > Detail Packages </ strong ></ P >< div * ngFor = \"let addService of detailService\" >< p class= \"text-center\" >{ { addService} }</ p ></ div </ div ></ div >< br />< div class= \"row\" >< div class= \"col-12\" >< P class= \"text-center\" >< strong > Vehicle Upcharge </ strong ></ P >< p class= \"text-center\" >{ { upchargeService} }</ p ></ div ></ div >< br />< br />< div class= \"row\" >< div class= \"col-12\" >< P class= \"text-center\" >< strong > Air Fresheners </ strong ></ P >< p class= \"text-center\" >{ { airfreshService} }</ p ></ div ></ div ></ div ></ div >< div class= \"row\" >< div class= \"col-12\" >< div > In: &nbsp;{ { TimeIn} }</ div ></ div ></ div >< br />< div class= \"row\" >< div class= \"col-12\" >< div > Out: &nbsp;{ { EstimatedTimeOut } }</ div ></ div ></ div >< br />< div class= \"row\" >< div class= \"col-12\" >< div > Est: &nbsp;{ { minutes} }&nbsp; Min </ div ></ div ></ div >< br />< div class= \"row\" >< div class= \"col-12 text-center\" >< strong > New Customer Info</strong></div></div><br/><br/><div class= \"row\" >    < div class= \"col-12\" >< div > Name: &nbsp;{ { ClientName} }</ div ></ div ></ div >< br />< div class= \"row\" >< div class= \"col-12\" >< div > Phone: &nbsp;{ { PhoneNumber} }</ div ></ div ></ div >< br />< div class= \"row\" >< div class= \"col-12\" >< div > Email: &nbsp;{ { Email} }</ div ></ div ></ div >< br />< br />< div >< div class= \"col-12\" >< div > Note: { { Notes} }</ div ></ div ></ div ></ div ></ body ></ html >";

        //const string HTML_TEMPLATE = "<!DOCTYPE html><html><head> <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css" rel="stylesheet"/> </head><body><div id="print-section"> <div class="row"> <div class="col-12"> <p>Client: &nbsp;&nbsp;{{ClientName}}</p></div></div><div class="row"> <div class="col-4">Vehicle: &nbsp;{{VehicleModel}}</div><div class="col-4 text-center">{{VehicleMake}}</div><div class="col-4 text-right">{{VehicleColor}}</div></div><div class="row"> <div class="col-6"> </div><div class="col-6 text-right"> <div class="row"> <div class="col-12"> <P class="text-center"><strong>Detail Packages</strong></P> <div *ngFor="let addService of detailService"> <p class="text-center">{{addService}}</p></div></div></div><br/> <div class="row"> <div class="col-12"> <P class="text-center"><strong>Vehicle Upcharge</strong></P> <p class="text-center">{{upchargeService}}</p></div></div><br/> <br/> <div class="row"> <div class="col-12"> <P class="text-center"><strong>Air Fresheners</strong></P> <p class="text-center">{{airfreshService}}</p></div></div></div></div><div class="row"> <div class="col-12"> <div>In: &nbsp;{{TimeIn}}</div></div></div><br/> <div class="row"> <div class="col-12"> <div>Out: &nbsp;{{EstimatedTimeOut}}</div></div></div><br/> <div class="row"> <div class="col-12"> <div>Est: &nbsp;{{minutes}}&nbsp;Min</div></div></div><br/> <div class="row"> <div class="col-12 text-center"> <strong>New Customer Info</strong> </div></div><br/> <br/> <div class="row"> <div class="col-12"> <div> Name: &nbsp;{{ClientName}}</div></div></div><br/> <div class="row"> <div class="col-12"> <div> Phone: &nbsp;{{PhoneNumber}}</div></div></div><br/> <div class="row"> <div class="col-12"> <div> Email: &nbsp;{{Email}}</div></div></div><br/> <br/> <div > <div class="col-12"> <div>Note:{{Notes}}</div></div></div></div></body></html>";

        //const string HTML_TEMPLATE = "<!DOCTYPE html><html><head> <link href=\"https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css\" rel=\"stylesheet\"/> </head><body><div id=\"print-section\"> <div class=\"row\"> <div class=\"col-12\"> <p>Client: &nbsp;&nbsp;{{ClientName}}</p></div></div><div class=\"row\"> <div class=\"col-4\">Vehicle: &nbsp;{{VehicleModel}}</div><div class=\"col-4 text-center\">{{VehicleMake}}</div><div class=\"col-4 text-right\">{{VehicleColor}}</div></div><div class=\"row\"> <div class=\"col-6\"> </div><div class=\"col-6 text-right\"> <div class=\"row\"> <div class=\"col-12\"> <P class=\"text-center\"><strong>Detail Packages</strong></P> <div *ngFor=\"let addService of detailService\"> <p class=\"text-center\">{{addService}}</p></div></div></div><br/> <div class=\"row\"> <div class=\"col-12\"> <P class=\"text-center\"><strong>Vehicle Upcharge</strong></P> <p class=\"text-center\">{{upchargeService}}</p></div></div><br/> <br/> <div class=\"row\"> <div class=\"col-12\"> <P class=\"text-center\"><strong>Air Fresheners</strong></P> <p class=\"text-center\">{{airfreshService}}</p></div></div></div></div><div class=\"row\"> <div class=\"col-12\"> <div>In: &nbsp;{{TimeIn}}</div></div></div><br/> <div class=\"row\"> <div class=\"col-12\"> <div>Out: &nbsp;{{EstimatedTimeOut}}</div></div></div><br/> <div class=\"row\"> <div class=\"col-12\"> <div>Est: &nbsp;{{minutes}}&nbsp;Min</div></div></div><br/> <div class=\"row\"> <div class=\"col-12 text-center\"> <strong>New Customer Info</strong> </div></div><br/> <br/> <div class=\"row\"> <div class=\"col-12\"> <div> Name: &nbsp;{{ClientName}}</div></div></div><br/> <div class=\"row\"> <div class=\"col-12\"> <div> Phone: &nbsp;{{PhoneNumber}}</div></div></div><br/> <div class=\"row\"> <div class=\"col-12\"> <div> Email: &nbsp;{{Email}}</div></div></div><br/> <br/> <div > <div class=\"col-12\"> <div>Note:{{Notes}}</div></div></div></div></body></html>";

        public PaymentSucessViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationController.NavigationBar.Hidden = true;

            Initialise();
            UpdateData();

            lblReceipt.AddGestureRecognizer(new UITapGestureRecognizer(NoReceiptClicked));

            btnPrint.TouchUpInside += delegate
            {
                PrintReceipt();
            };

            btnEmail.TouchUpInside += delegate
            {
                EmailPopupViewController vc = (EmailPopupViewController)GetViewController(GetHomeStorybpard(), nameof(EmailPopupViewController));
                vc.EmailDelegate = this;
                PresentViewController(vc, true, () => { });

                //TestEmailTemplate();
            };
        }

        async Task SendEmailReceipt(string email)
        {
            string emailContentHtml = MakeServiceReceipt();

            string subject = null;
            if (ServiceType == ServiceType.Wash)
                subject = Common.Messages.SERVICE_RECEIPT_SUBJECT;
            else // DETAIL
                subject = Common.Messages.DETAIL_RECEIPT_SUBJECT;

            ShowActivityIndicator();

            var response = await SingleTon.WashApiService.SendEmail(email, subject, emailContentHtml);

            HideActivityIndicator();

            HandleResponse(response);

            if (response.IsSuccess())
            {
                ShowAlertMsg(Common.Messages.EMAIL_SENT_MSG, titleTxt: Common.Messages.EMAIL);
            }

            //EmailServiceReceipt(emailContentHtml, subject);
        }

        async Task TestEmailTemplate()
        {
            ShowActivityIndicator();

            //string email = "";

            //var response = await SingleTon.WashApiService.SendEmail("karthiknever16@gmail.com", "something", HTML_TEMPLATE);

            HideActivityIndicator();
        }

        [Export("mailComposeController:didFinishWithResult:error:")]
        public void Finished(MFMailComposeViewController controller, MFMailComposeResult result, NSError error)
        {
            this.DismissViewController(true, null);
        }

        private void UpdateData()
        {
            lblTicketId.Text = TicketID.ToString();
            lblVehicle.Text = $"{Make} {Model} {Color}";

            if (Service is not null)
            {
                ServiceName = string.Empty;
                for (int i = 0; i < Service.JobItems.Count; i++)
                {
                    ServiceName += Service.JobItems[i].SeriveName;

                    if (i != Service.JobItems.Count - 1)
                    {
                        ServiceName += ",";
                    }
                }
            }

           // var mutableAttributedString = new NSMutableAttributedString(
           //"Services: ",
           //UIFont.SystemFontOfSize(18));
           // var attributedString = new NSAttributedString(
           //    ServiceName,
           //    font: UIFont.SystemFontOfSize(18, UIFontWeight.Semibold)
           //);

           // mutableAttributedString.Append(attributedString);
           // lblService.AttributedText = mutableAttributedString;

           // lblAmount.Text = $"${Amount}";
        }

        void Initialise()
        {
            Title = SCREEN_TITLE;
        }

        void NoReceiptClicked(UITapGestureRecognizer tap)
        {
            NavigateToServiceHome();
        }

        void NavigateToServiceHome()
        {
            UIViewController vc = null;
            //if (IsFromNewService)
            //    vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 6];
            //else
            //    vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 3];
            //NavigationController.PopToViewController(vc, true);
            NavigationController.PopToRootViewController(true);
        }

        string MakeServiceReceipt()
        {
            var body = "<p>Ticket Number : </p>" + TicketID + "<br /><br />";

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

            if (Service is not null)
            {
                //var totalAmt = 0f;
                for (int i = 0; i < Service.JobItems.Count; i++)
                {
                    var job = Service.JobItems[i];
                    var price = job.Price.ToString();
                    if ((job.Price % 1) == 0)
                    {
                        price += ":00";
                    }
                    else
                    {
                        var values = price.Split(".");
                        price = (int)job.Price + ":" + values[1];
                    }

                    body += "<p>" + job.SeriveName + " - " + "$" + price + "</p>";
                    //totalAmt += job.Price;
                    //Amount = totalAmt;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ServiceName))
                {
                    body += "<p>" + ServiceName + "</p>";
                }

                if (!string.IsNullOrEmpty(AdditionalServiceName) && !AdditionalServiceName.Equals("none", StringComparison.OrdinalIgnoreCase))
                {
                    body += "<p>" + AdditionalServiceName + "</p>";
                }
            }

            string stars = string.Empty;

            var shownCountOfCardNumber = 4;

            for (int i = 0; i < CardNumber.Length - shownCountOfCardNumber; i++)
            {
                stars += "*";
            }

            var maskedCardNumber = stars + CardNumber.Substring(CardNumber.Length - shownCountOfCardNumber, shownCountOfCardNumber);

            body += "<br /><p>Payment Details : </p>";
            body += "<p> Payment Type - Credit Card</p>";
            body += "<p> Card Number - " + maskedCardNumber + "</p>";

            body += "<br/ ><p>" + "Total Amount Paid: " + "$" + Amount.ToString() + "</p>";

            Debug.WriteLine("Email Body :" + body);

            return body;
        }

        void PrintReceipt()
        {
            string printContentHtml = MakeServiceReceipt();
            Print(printContentHtml);
        }

        public void SendEmailClicked(string email)
        {
            _ = SendEmailReceipt(email);
        }
    }
}
