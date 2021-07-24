// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreFoundation;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Modules.Pay;
using Greeter.Services.Api;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class EmailViewController : BaseViewController, IUIPickerViewDelegate, IUIPickerViewDataSource
    {
        // Data
        const string SCREEN_TITLE = "Email Receipt";
        //public string[] data = new string[] {
        //    "Main Street 1",
        //    "Main Street 2",
        //    "Main Street 3"
        //};

        public string Make;
        public string Model;
        public string Color;
        public string CustName;
        public CreateServiceRequest Service;
        public ServiceType ServiceType;

        List<Employee> Employees;
        string[] employeeNames;

        string selectedEmpEmailId;

        //Views
        UIPickerView pv = new UIPickerView();

        public EmailViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Initialise();
            //_ = GetData();

#if DEBUG
            tfCust.Text = "karthiknever16@gmail.com";
#endif

            //Clicks
            btnEmpDropdown.TouchUpInside += delegate
            {
                tfEmp.BecomeFirstResponder();
            };

            btnEmpSent.TouchUpInside += delegate
            {
                if (selectedEmpEmailId.IsEmpty())
                {
                    ShowAlertMsg(Common.Messages.EMPLOYEE_MISSING);
                    return;
                }

                _ = SendEmail(selectedEmpEmailId);
            };

            btnCustomerSend.TouchUpInside += delegate
            {
                if (tfCust.Text.IsEmpty())
                {
                    ShowAlertMsg(Common.Messages.EMAIL_MISSING);
                    return;
                }

                _ = SendEmail(tfCust.Text);
            };

            btnPrint.TouchUpInside += delegate
            {
                //TODO : Temprary Loader to hide not done this functionality
                _ = ShowLoader();
            };

            btnPay.TouchUpInside += delegate
            {
                NavigateToPayScreen();
            };
        }

        async Task ShowLoader()
        {
            ShowActivityIndicator();
            await Task.Delay(3000);
            HideActivityIndicator();
        }

        async Task GetData()
        {
            ShowActivityIndicator();
            var apiService = new WashApiService();
            var req = new GetDetailEmployeeReq
            {
                LocationID = AppSettings.LocationID
            };

            var employeesResponse = await apiService.GetDetailEmployees(req);
            Employees = employeesResponse?.EmployeeList;
            employeeNames = Employees?.Select(x => x.FirstName + " " + x.LastName).ToArray();
            HideActivityIndicator();
        }

        async Task SendEmail(string email)
        {
            try
            {
                if (!email.IsEmail())
                {
                    ShowAlertMsg(Common.Messages.EMAIL_WARNING);
                    return;
                }

                ShowActivityIndicator();

                var subject = "Wash Receipt";

                //TODO : Email Body Teplate Issue Fix
                var body = "<p>Ticket Number : </p>" + Service.Job.JobId + "<br /><br />";

                if (Service.Job.ClientId != 0)
                {
                    body += "<p>Client Details : </p>" + ""
                        + "<p>Client Name - " + CustName + "</p><br />";
                }

                body += "<p>Vehicle Details : </p>" +
                     "<p>Make - " + Make + "</p>" +
                    "<p>Model - " + Model + "</p>" +
                     "<p>Color - " + Color + "</p><br />" +
                     "<p>Services : " + "</p>";

                var totalAmt = 0f;
                for (int i = 0; i < Service.JobItems.Count; i++)
                {
                    var job = Service.JobItems[i];
                    body += "<p>" + job.SeriveName + " - " + job.Price + "</p>";
                    totalAmt += job.Price;
                }

                body += "<br/ ><p>" + "Total Amount : " + totalAmt.ToString() + "</p>";

                //body = "<div>Something</div>";

                Debug.WriteLine("Email Body :" + body);

                var response = await new WashApiService().SendEmail(email, subject, body);
                HideActivityIndicator();

                HandleResponse(response);

                if (response.IsSuccess())
                {
                    ShowAlertMsg(Common.Messages.EMAIL_SENT_MSG, titleTxt: Common.Messages.EMAIL);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened and the reason is : " + ex.Message);
                HideActivityIndicator();
            }
        }

        void Initialise()
        {
            Title = SCREEN_TITLE;

            tfEmp.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfEmp.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            tfCust.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfCust.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            AddPickerToolbar(tfEmp, tfEmp.Placeholder, PickerDone);
            tfEmp.InputView = pv;

            // For Restricting typing in the location field
            tfEmp.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            pv.DataSource = this;
            pv.Delegate = this;

            if (ServiceType == ServiceType.Wash)
            {
                viewDetailer.Hidden = true;
            }
        }

        void PickerDone()
        {
            int pos = (int)pv.SelectedRowInComponent(0);
            tfEmp.Text = employeeNames[pos];
            selectedEmpEmailId = Employees[pos].EmailID;
        }

        void NavigateToPayScreen()
        {
            var vc = new PaymentViewController();
            vc.JobID = Service.Job.JobId;
            vc.Make = Make;
            vc.Model = Model;
            vc.Color = Color;
            vc.CustName = CustName;

            var mainService  = Service.JobItems.First(x => x.IsMainService);

            vc.ServiceName = mainService.SeriveName;

            var totalAmt = 0f;
            for (int i = 0; i < Service.JobItems.Count; i++)
            {
                totalAmt += Service.JobItems[i].Price;
            }

            vc.Amount = totalAmt;
            NavigateToWithAnim(vc);
        }

        public nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return employeeNames?.Length ?? 0;
        }

        [Export("pickerView:didSelectRow:inComponent:")]
        public void Selected(UIPickerView pickerView, nint row, nint component)
        {

        }

        [Export("pickerView:titleForRow:forComponent:")]
        public string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return employeeNames[row];
        }
    }
}
