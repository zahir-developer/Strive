// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using Newtonsoft.Json;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class VerifyVehicleInfoViewController : BaseViewController
    {
        const string SCREEN_TITLE = "Verify Vehicle Info";

        public long MakeID;
        public long ModelID;
        public long ColorID;
        public long JobTypeID;
        public long ClientID;
        public long VehicleID;
        public JobItem MainService;
        public JobItem Upcharge;
        public JobItem Additional;
        public JobItem AirFreshner;

        public string Make = string.Empty;
        public string Model = string.Empty;
        public string Color = string.Empty;
        public string Barcode;
        public string CustName;
        public string UpchargeTypeName;
        public ServiceType ServiceType;

        public VerifyVehicleInfoViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Initialise();
            UpdateData();

            //Clicks
            btnCancel.TouchUpInside += delegate
            {
                var vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 3];
                NavigationController.PopToViewController(vc, true);
            };

            btnConfirm.TouchUpInside += delegate
            {
                _ = CreateService();
            };
        }

        void UpdateData()
        {
            lblvehicle.Text = Make + " " + Model + " " + Color;
            lblBarcode.Text = !Barcode.IsEmpty() ? Barcode : "-";
            lblCustName.Text = !CustName.IsEmpty() ? CustName : "Drive up";
            lblType.Text = !UpchargeTypeName.IsEmpty() ? UpchargeTypeName : "-";
        }

        void Initialise()
        {
            Title = SCREEN_TITLE;

            viewCustName.MakecardView();
            viewVehicle.MakecardView();
            viewBarcode.MakecardView();
            viewType.MakecardView();
        }

        async Task CreateService()
        {
            ShowActivityIndicator();

            var apiService = new WashApiService();
            var ticketResponse = await apiService.GetTicketNumber(AppSettings.LocationID);
            long jobId = ticketResponse.Ticket.TicketNo;

            var jobItems = new List<JobItem>();

            MainService.JobId = jobId;
            jobItems.Add(MainService);

            float serviceTimeMins = 0;

            //detailTimeMins += MainService.Time;

            if (Upcharge != null)
            {
                Upcharge.JobId = jobId;
                serviceTimeMins += Upcharge.Time;
                jobItems.Add(Upcharge);
            }

            if (Additional != null)
            {
                Additional.JobId = jobId;
                serviceTimeMins += Additional.Time;
                jobItems.Add(Upcharge);
            }

            if (AirFreshner != null)
            {
                AirFreshner.JobId = jobId;
                serviceTimeMins += AirFreshner.Time;
                jobItems.Add(AirFreshner);
            }

            var jobStatusResponse = await new GeneralApiService().GetGlobalData("JOBSTATUS");
            long jobStatusId = jobStatusResponse.Codes.Where(x => x.Name.Equals("In Progress")).FirstOrDefault().ID;

            if (jobId != 0)
            {
                var req = new CreateServiceRequest()
                {
                    Job = new Job()
                    {
                        JobId = jobId,
                        JobStatusID = jobStatusId,
                        JobTypeID = JobTypeID,
                        MakeID = MakeID,
                        ModelID = ModelID,
                        ColorId = ColorID,
                        ClientId = ClientID,
                        VehicleId = VehicleID,
                        LocationID = AppSettings.LocationID,
                    },
                    JobItems = jobItems
                };

                if (ServiceType == ServiceType.Wash)
                    req.Job.EstimatedTimeOut = DateTime.Now.AddMinutes(AppSettings.WashTime + serviceTimeMins);
                else
                    req.Job.EstimatedTimeOut = DateTime.Now.AddMinutes(MainService.Time + serviceTimeMins);

                Debug.WriteLine("Create Serive Req " + JsonConvert.SerializeObject(req));

                var createServiceResponse = await apiService.CreateService(req);
                HideActivityIndicator();

                if (createServiceResponse.IsNoInternet())
                {
                    ShowAlertMsg(createServiceResponse.Message);
                    return;
                }

                if (createServiceResponse?.IsSuccess() ?? false)
                {
                    ShowAlertMsg(Common.Messages.SERVICE_CREATED_MSG, () =>
                    {
                        //var vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 3];
                        //this.NavigationController.PopToViewController(vc, true);

                        var vc = (EmailViewController)GetViewController(GetHomeStorybpard(), nameof(EmailViewController));
                        vc.Make = Make;
                        vc.Model = Model;
                        vc.Color = Color;
                        vc.CustName = CustName;
                        vc.Service = req;
                        NavigationController.PushViewController(vc, true);
                    }, titleTxt : Common.Messages.SERVICE);
                }
                else
                {
                    ShowAlertMsg(Common.Messages.SERVICE_CREATION_ISSUE);
                }
            }
            else
            {
                ShowAlertMsg(Common.Messages.TICKET_CERATION_ISSUE);
            }
        }

        void NavigateToEmailScreen()
        {
            UIViewController vc = GetViewController(GetHomeStorybpard(), nameof(EmailViewController));
            NavigateToWithAnim(vc);
        }
    }
}
