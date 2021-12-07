// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        public JobItem[] AdditionalServices;
        public JobItem AirFreshner;

        public string Make = string.Empty;
        public string Model = string.Empty;
        public string Color = string.Empty;
        public string Barcode;
        public string CustName;
        public string UpchargeTypeName;
        public ServiceType ServiceType;
        public bool IsMembershipService;

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
            lblCustName.Text = CustomerName;
            lblType.Text = !UpchargeTypeName.IsEmpty() ? UpchargeTypeName : "-";
        }

        string CustomerName => !CustName.IsEmpty() ? CustName : Common.Messages.DRIVE_UP;

        void Initialise()
        {
            Title = SCREEN_TITLE;

            viewCustName.MakecardView();
            viewVehicle.MakecardView();
            viewBarcode.MakecardView();
            viewType.MakecardView();
        }

        //async Task CreateService()
        //{
        //    try
        //    {
        //        ShowActivityIndicator();

        //        var apiService = new WashApiService();
        //        var ticketResponse = await apiService.GetTicketNumber(AppSettings.LocationID);
        //        long jobId = ticketResponse.Ticket.TicketNo;

        //        var jobItems = new List<JobItem>();

        //        MainService.JobID = jobId;
        //        jobItems.Add(MainService);

        //        float serviceTimeMins = 0;

        //        //detailTimeMins += MainService.Time;

        //        if (Upcharge != null)
        //        {
        //            Upcharge.JobID = jobId;
        //            serviceTimeMins += Upcharge.Time;
        //            jobItems.Add(Upcharge);
        //        }

        //        if (AdditionalServices is not null && AdditionalServices.Length > 0)
        //        {
        //            for (int i = 0; i < AdditionalServices.Length; i++)
        //            {
        //                AdditionalServices[i].JobID = jobId;
        //                serviceTimeMins += AdditionalServices[i].Time;
        //                jobItems.Add(AdditionalServices[i]);
        //            }

        //            //Additional.JobId = jobId;
        //            //serviceTimeMins += Additional.Time;
        //            //jobItems.Add(Additional);
        //        }

        //        if (AirFreshner != null)
        //        {
        //            AirFreshner.JobID = jobId;
        //            serviceTimeMins += AirFreshner.Time;
        //            jobItems.Add(AirFreshner);
        //        }

        //        var jobStatusResponse = await new GeneralApiService().GetGlobalData("JOBSTATUS");

        //        long jobStatusId = jobStatusResponse.Codes.Where(x => x.Name.Equals("Waiting")).FirstOrDefault().ID;

        //        if (jobId != 0)
        //        {
        //            var req = new CreateServiceRequest()
        //            {
        //                Job = new Job()
        //                {
        //                    JobID = jobId,
        //                    TicketNumber = jobId,
        //                    JobStatusID = jobStatusId,
        //                    JobTypeID = JobTypeID,
        //                    MakeID = MakeID,
        //                    ModelID = ModelID,
        //                    ColorID = ColorID,
        //                    ClientID = ClientID != 0 ? ClientID : null,
        //                    VehicleID = VehicleID != 0 ? VehicleID : null,
        //                    LocationID = AppSettings.LocationID,
        //                    Barcode = Barcode
        //                },
        //                JobItems = jobItems
        //            };

        //            BaseResponse createServiceResponse = null;

        //            if (ServiceType == ServiceType.Wash)
        //            {
        //                req.Job.TimeIn = DateTime.Now;
        //                req.Job.EstimatedTimeOut = DateTime.Now.AddMinutes(AppSettings.WashTime + serviceTimeMins);
        //                createServiceResponse = await apiService.CreateService(req);
        //            }
        //            else // Detail
        //            {
        //                var getAvailableScheduleReq = new GetAvailableScheduleReq() { LocationID = AppSettings.LocationID };
        //                var availableScheduleResponse = await apiService.GetAvailablilityScheduleTime(getAvailableScheduleReq);

        //                float totalTimeMins = MainService.Time + serviceTimeMins;
        //                //req.Job.EstimatedTimeOut = DateTime.Now.AddMinutes(20);
        //                //#if DEBUG
        //                var bayGroup = availableScheduleResponse.GetTimeInDetails.Distinct().GroupBy(obj => obj.BayID);

        //                GetTimeInDetails matchTimeInDetails = null;
        //                string startTime = string.Empty;
        //                string endTime = string.Empty;

        //                int bayCount = 0;

        //                foreach (IEnumerable<GetTimeInDetails> timeInDetails in bayGroup)
        //                {
        //                    if (matchTimeInDetails is not null) break;

        //                    var timeInDetailsList = timeInDetails.ToList();

        //                    if (timeInDetailsList is not null && timeInDetailsList.Count > 0)
        //                    {
        //                        var previousTimeInDetail = timeInDetails.FirstOrDefault();

        //                        var availableTime = 0; //Time represent in minutes
        //                        bayCount = 0;

        //                        for (int i = 1; i < timeInDetailsList.Count; i++)
        //                        {
        //                            var isThirtyMinuteDistance = IsThirtyMinuteDistance(previousTimeInDetail.TimeIn, timeInDetailsList[i].TimeIn);
        //                            if (isThirtyMinuteDistance)
        //                            {
        //                                availableTime += 60 * 30; //Add 30 minutes
        //                                bayCount += 1;

        //                                if (string.IsNullOrEmpty(startTime))
        //                                {
        //                                    startTime = timeInDetailsList[i].TimeIn;
        //                                }

        //                                if (availableTime >= totalTimeMins)
        //                                {
        //                                    matchTimeInDetails = timeInDetailsList[i];
        //                                    break;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                startTime = string.Empty;
        //                                availableTime = 0;
        //                            }
        //                            previousTimeInDetail = timeInDetailsList[i];
        //                        }
        //                    }
        //                }

        //                if (matchTimeInDetails is null)
        //                {
        //                    startTime = string.Empty;
        //                    //TODO show error message
        //                    ShowAlertMsg(Common.Messages.NO_SLOTS);
        //                    HideActivityIndicator();
        //                    return;
        //                }

        //                req.JobDetail = new JobDetail
        //                {
        //                    JobID = jobId,
        //                    BayID = matchTimeInDetails.BayID
        //                };

        //                req.BaySchedules = new List<BaySchedule>();

        //                //float diff = 0;

        //                for (int i = 0; i < bayCount; i++)
        //                {
        //                    var baySchedule = new BaySchedule()
        //                    {
        //                        BayID = req.JobDetail.BayID,
        //                        JobID = jobId,
        //                        ScheduleInTime = startTime,
        //                        //ScheduleDate = req.Job.JobDate.ToString("yyyy-MM-dd")
        //                        ScheduleDate = req.Job.JobDate
        //                    };


        //                    //if (i != 0)
        //                    //{
        //                    endTime = startTime;
        //                    string[] ds = endTime.Split(":");

        //                    if ((ds[1])[0] == '3')
        //                    {
        //                        int num = Convert.ToInt32(ds[0]);
        //                        num += 1;
        //                        endTime = num.ToString() + ":00";
        //                    }
        //                    else
        //                    {
        //                        endTime = ds[0] + ":30";
        //                    }
        //                    //}

        //                    baySchedule.ScheduleOutTime = endTime;

        //                    //req.BaySchedules.Add(baySchedule);
        //                }
        //                //#endif
        //                string[] sdt = startTime.Split(":");
        //                req.Job.TimeIn = DateTime.Now.Date.AddHours(Convert.ToDouble(sdt[0])).AddMinutes(Convert.ToDouble(sdt[1])).AddSeconds(0);

        //                string[] edt = endTime.Split(":");
        //                req.Job.EstimatedTimeOut = DateTime.Now.Date.AddHours(Convert.ToDouble(edt[0])).AddMinutes(Convert.ToDouble(edt[1])).AddSeconds(0);
        //                createServiceResponse = await apiService.CreateDetailService(req);
        //            }

        //            Debug.WriteLine("Create Serive Req " + JsonConvert.SerializeObject(req));

        //            HideActivityIndicator();

        //            HandleResponse(createServiceResponse);

        //            if (createServiceResponse?.IsSuccess() ?? false)
        //            {
        //                ShowAlertMsg(Common.Messages.SERVICE_CREATED_MSG, () =>
        //                {
        //                    //var vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 3];
        //                    //this.NavigationController.PopToViewController(vc, true);

        //                    // Remove this and service question view controllers from stack
        //                    var nc = NavigationController;
        //                    var navigationViewControllers = NavigationController.ViewControllers.ToList();
        //                    navigationViewControllers.RemoveAt(navigationViewControllers.Count - 1);
        //                    navigationViewControllers.RemoveAt(navigationViewControllers.Count - 1);// You can pass your index here
        //                    NavigationController.ViewControllers = navigationViewControllers.ToArray();

        //                    var vc = (EmailViewController)GetViewController(GetHomeStorybpard(), nameof(EmailViewController));
        //                    vc.Make = Make;
        //                    vc.Model = Model;
        //                    vc.Color = Color;
        //                    vc.CustName = CustName;
        //                    vc.Service = req;
        //                    vc.ServiceType = ServiceType;
        //                    nc.PushViewController(vc, true);
        //                }, titleTxt: Common.Messages.SERVICE);
        //            }
        //            else
        //            {
        //                ShowAlertMsg(Common.Messages.SERVICE_CREATION_ISSUE);
        //            }
        //        }
        //        else
        //        {
        //            ShowAlertMsg(Common.Messages.TICKET_CERATION_ISSUE);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Exception happened and the reason is : " + ex.Message);
        //        HideActivityIndicator();
        //    }
        //}

        // Updated
        async Task CreateService()
        {
            try
            {
                ShowActivityIndicator();

                var apiService = new WashApiService();
                var ticketResponse = await apiService.GetTicketNumber(AppSettings.LocationID);
                long jobId = ticketResponse.Ticket.TicketNo;

                var jobItems = new List<JobItem>();

                MainService.JobID = jobId;
                jobItems.Add(MainService);

                float serviceTimeMins = 0;

                //detailTimeMins += MainService.Time;

                if (Upcharge != null)
                {
                    Upcharge.JobID = jobId;
                    serviceTimeMins += Upcharge.Time;
                    jobItems.Add(Upcharge);
                }

                if (AdditionalServices is not null && AdditionalServices.Length > 0)
                {
                    for (int i = 0; i < AdditionalServices.Length; i++)
                    {
                        AdditionalServices[i].JobID = jobId;
                        serviceTimeMins += AdditionalServices[i].Time;
                        jobItems.Add(AdditionalServices[i]);
                    }

                    //Additional.JobId = jobId;
                    //serviceTimeMins += Additional.Time;
                    //jobItems.Add(Additional);
                }

                if (AirFreshner != null)
                {
                    AirFreshner.JobID = jobId;
                    serviceTimeMins += AirFreshner.Time;
                    jobItems.Add(AirFreshner);
                }

                var jobStatusResponse = await new GeneralApiService().GetGlobalData("JOBSTATUS");

                if (jobId != 0)
                {
                    var req = new CreateServiceRequest()
                    {
                        Job = new Job()
                        {
                            JobID = jobId,
                            TicketNumber = jobId,
                            JobTypeID = JobTypeID,
                            MakeID = MakeID,
                            ModelID = ModelID,
                            ColorID = ColorID,
                            ClientID = ClientID != 0 ? ClientID : null,
                            VehicleID = VehicleID != 0 ? VehicleID : null,
                            LocationID = AppSettings.LocationID,
                            Barcode = Barcode
                        },
                        JobItems = jobItems
                    };

                    BaseResponse createServiceResponse = null;

                    if (ServiceType == ServiceType.Wash)
                    {
                        req.Job.JobStatusID = jobStatusResponse.Codes.Where(x => x.Name.Equals("In Progress")).FirstOrDefault().ID;
                        req.Job.TimeIn = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT_FOR_API);
                        req.Job.EstimatedTimeOut = DateTime.Now.AddMinutes(AppSettings.WashTime + serviceTimeMins).ToString(Constants.DATE_TIME_FORMAT_FOR_API); ;
                        createServiceResponse = await apiService.CreateService(req);
                    }
                    else // Detail
                    {
                        req.Job.JobStatusID = jobStatusResponse.Codes.Where(x => x.Name.Equals("Waiting")).FirstOrDefault().ID;

                        var getAvailableScheduleReq = new GetAvailableScheduleReq() { LocationID = AppSettings.LocationID };
                        var availableScheduleResponse = await apiService.GetAvailablilityScheduleTime(getAvailableScheduleReq);

                        float totalTimeMins = (MainService.Time * 60) + serviceTimeMins;
                        //req.Job.EstimatedTimeOut = DateTime.Now.AddMinutes(20);
                        //#if DEBUG

                        var distinct = availableScheduleResponse.GetTimeInDetails.Distinct();
                        var datetime = DateTime.Now;
#if DEBUG
                        datetime = new DateTime(2021, 11, 26, 5, 15, 0);

                        for (int i = 0; i < availableScheduleResponse.GetTimeInDetails.Count; i++)
                        {
                            Debug.WriteLine("GetTimeIn : " + DateTime.ParseExact(availableScheduleResponse.GetTimeInDetails[i].TimeIn, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);
                            Debug.WriteLine(TimeSpan.Compare(DateTime.ParseExact(availableScheduleResponse.GetTimeInDetails[i].TimeIn, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay, datetime.TimeOfDay));
                        }

                        //totalTimeMins = 60;
#endif
                        Debug.WriteLine("Current time of day : " + datetime.TimeOfDay);

                        availableScheduleResponse.GetTimeInDetails.RemoveAll(x => TimeSpan.Compare(DateTime.ParseExact(x.TimeIn, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay, datetime.TimeOfDay) == -1);
                        Debug.WriteLine("Distinct GetTimeInDetails : " + JsonConvert.SerializeObject(availableScheduleResponse.GetTimeInDetails));

                        var bayGroup = availableScheduleResponse.GetTimeInDetails.Distinct().GroupBy(obj => obj.BayID);

                        //GetTimeInDetails matchTimeInDetails = null;
                        //string startTime = string.Empty;
                        //string endTime = string.Empty;

                        int bayCount = 0;
                        int bayId = -1;
                        string timeIn = null;

                        //var currentTime = DateTime.Now;

                        //if (currentTime.Minute < 30)
                        //{
                        //    currentTime = currentTime.AddMinutes(30 - currentTime.Minute);
                        //}
                        //else if (currentTime.Minute < 60)
                        //{
                        //    currentTime = currentTime.AddMinutes(60 - currentTime.Minute);
                        //}

                        //var timeInFormat = currentTime.ToString("HH:mm");

                        ////var scheduleList = bayGroup.ToList();

                        //var datetime = DateTime.ParseExact(timeInFormat, "HH:mm", CultureInfo.InvariantCulture);

                        //Debug.WriteLine("datetime " + datetime);

                        //var index = availableScheduleResponse.GetTimeInDetails.FindIndex(x => DateTime.ParseExact(x.TimeIn, "HH:mm", CultureInfo.InvariantCulture) > datetime);

                        //foreach (var timeIn in availableScheduleResponse.GetTimeInDetails)
                        //{
                        //    Debug.WriteLine(DateTime.ParseExact(timeIn.TimeIn, "HH:mm", CultureInfo.InvariantCulture));
                        //    //Debug.WriteLine("datetime " + datetime);
                        //}

                        //foreach (IEnumerable<GetTimeInDetails> timeInDetails in bayGroup)
                        //{
                        //    var timeInDetailsList = timeInDetails.ToList();

                        //    bayGroup.

                        //    for (int i = 1; i < timeInDetailsList.Count; i++)
                        //    {

                        //    }
                        //}

                        //bayGroup.ToList().RemoveAll(x => x.time)

                        var bayGroups = bayGroup.ToList();

                        //if (bayGroups.Count > 0)
                        //{

                        //}

                        for (int i = 0; i < bayGroups.Count; i++)
                        {
                            Debug.WriteLine("BayID : " + bayGroups[i].ToList()[0].BayID);
                            Debug.WriteLine("TimeIn : " + bayGroups[i].ToList()[0].TimeIn);

                            if (bayId is -1 && timeIn is null)
                            {
                                bayId = bayGroups[i].ToList()[0].BayID;
                                timeIn = bayGroups[i].ToList()[0].TimeIn;
                            }

                            if (DateTime.ParseExact(timeIn, "HH:mm", CultureInfo.InvariantCulture) > DateTime.ParseExact(bayGroups[i].ToList()[0].TimeIn, "HH:mm", CultureInfo.InvariantCulture))
                            {
                                bayId = bayGroups[i].ToList()[0].BayID;
                                timeIn = bayGroups[i].ToList()[0].TimeIn;
                            }

                            //   //if (matchTimeInDetails is not null) break;

                            //   var timeInDetailsList = bayGroups[i].ToList();

                            //    if (timeInDetailsList is not null && timeInDetailsList.Count > 0)
                            //    {
                            //        GetTimeInDetails previousTimeInDetail = null;

                            //        var availableTime = 0; //Time represent in minutes
                            //        bayCount = 0;

                            //        for (int j = 0; j < timeInDetailsList.Count; j++)
                            //        {
                            //            var isThirtyMinuteDistance = true;

                            //            if(availableTime != 0 && previousTimeInDetail != null)
                            //            {
                            //                isThirtyMinuteDistance = IsThirtyMinuteDistance(previousTimeInDetail.TimeIn, timeInDetailsList[j].TimeIn);
                            //            }

                            //            if (isThirtyMinuteDistance)
                            //            {
                            //                availableTime += 30; //Add 30 minutes
                            //                bayCount += 1;

                            //                if (string.IsNullOrEmpty(startTime))
                            //                {
                            //                    startTime = timeInDetailsList[j].TimeIn;
                            //                }

                            //                if (availableTime >= totalTimeMins)
                            //                {
                            //                    matchTimeInDetails = timeInDetailsList[j];
                            //                    break;
                            //                }
                            //            }
                            //            else
                            //            {
                            //                startTime = string.Empty;
                            //                availableTime = 0;
                            //                j--;
                            //            }

                            //            previousTimeInDetail = timeInDetailsList[j];
                            //        }
                            //    }
                        }

                        //foreach (IEnumerable<GetTimeInDetails> timeInDetails in bayGroup)
                        //{
                        //    if (matchTimeInDetails is not null) break;

                        //    var timeInDetailsList = timeInDetails.ToList();

                        //    if (timeInDetailsList is not null && timeInDetailsList.Count > 0)
                        //    {
                        //        //var previousTimeInDetail = timeInDetails.FirstOrDefault();

                        //        var availableTime = 0; //Time represent in minutes
                        //        bayCount = 0;

                        //        for (int i = 0; i < timeInDetailsList.Count; i++)
                        //        {
                        //            var isThirtyMinuteDistance = IsThirtyMinuteDistance(timeInDetailsList[i].TimeIn, timeInDetailsList[i+ 1].TimeIn);
                        //            if (isThirtyMinuteDistance)
                        //            {
                        //                availableTime += 60 * 30; //Add 30 minutes
                        //                bayCount += 1;

                        //                if (string.IsNullOrEmpty(startTime))
                        //                {
                        //                    startTime = timeInDetailsList[i].TimeIn;
                        //                }

                        //                if (availableTime >= totalTimeMins)
                        //                {
                        //                    matchTimeInDetails = timeInDetailsList[i];
                        //                    break;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                startTime = string.Empty;
                        //                availableTime = 0;
                        //            }
                        //            //previousTimeInDetail = timeInDetailsList[i];
                        //        }
                        //    }
                        //}

                        //if (matchTimeInDetails is null)
                        //{
                        //    startTime = string.Empty;
                        //    //TODO show error message
                        //    ShowAlertMsg(Common.Messages.NO_SLOTS);
                        //    HideActivityIndicator();
                        //    return;
                        //}

                        if (bayId is -1)
                        {
                            ShowAlertMsg(Common.Messages.NO_SLOTS);
                            HideActivityIndicator();
                            return;
                        }

                        req.JobDetail = new JobDetail
                        {
                            JobID = jobId,
                            BayID = bayId
                        };

                        //req.BaySchedules = new List<BaySchedule>();

                        //float diff = 0;

                        //for (int i = 0; i < bayCount; i++)
                        //{
                        //var baySchedule = new BaySchedule()
                        //{
                        //    BayID = req.JobDetail.BayID,
                        //    JobID = jobId,
                        //    ScheduleInTime = startTime,
                        //    //ScheduleDate = req.Job.JobDate.ToString("yyyy-MM-dd")
                        //    ScheduleDate = req.Job.JobDate
                        //};

                        //if (i == 0)
                        //{
                        //    endTime = startTime;
                        //}

                        //string[] ds = endTime.Split(":");

                        //if ((ds[1])[0] == '3')
                        //{
                        //    int num = Convert.ToInt32(ds[0]);
                        //    num += 1;
                        //    endTime = num.ToString() + ":00";
                        //}
                        //else
                        //{
                        //    endTime = ds[0] + ":30";
                        //}
                        //}

                        //baySchedule.ScheduleOutTime = endTime;

                        //req.BaySchedules.Add(baySchedule);
                        //}
                        //#endif
                        //string[] sdt = startTime.Split(":");
                        //req.Job.TimeIn = DateTime.Now.Date.AddHours(Convert.ToDouble(sdt[0])).AddMinutes(Convert.ToDouble(sdt[1])).AddSeconds(0);

                        //string[] edt = endTime.Split(":");
                        req.Job.TimeIn = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT_FOR_API);
                        //req.Job.EstimatedTimeOut = DateTime.Now.Date.AddHours(Convert.ToDouble(edt[0])).AddMinutes(Convert.ToDouble(edt[1])).AddSeconds(0);
                        req.Job.EstimatedTimeOut = DateTime.Parse(req.Job.TimeIn).AddMinutes(totalTimeMins).ToString(Constants.DATE_TIME_FORMAT_FOR_API);
                        createServiceResponse = await apiService.CreateDetailService(req);
                    }

                    Debug.WriteLine("Create Serive Req " + JsonConvert.SerializeObject(req));

                    HideActivityIndicator();

                    HandleResponse(createServiceResponse);

                    if (createServiceResponse?.IsSuccess() ?? false)
                    {
                        ShowAlertMsg(Common.Messages.SERVICE_CREATED_MSG, () =>
                        {
                                    //var vc = NavigationController.ViewControllers[NavigationController.ViewControllers.Length - 3];
                                    //this.NavigationController.PopToViewController(vc, true);

                                    // Remove this and service question view controllers from stack
                                    var nc = NavigationController;
                            var navigationViewControllers = NavigationController.ViewControllers.ToList();
                            navigationViewControllers.RemoveAt(navigationViewControllers.Count - 1);
                            navigationViewControllers.RemoveAt(navigationViewControllers.Count - 1);// You can pass your index here
                                    NavigationController.ViewControllers = navigationViewControllers.ToArray();

                            var vc = (EmailViewController)GetViewController(GetHomeStorybpard(), nameof(EmailViewController));
                            vc.Make = Make;
                            vc.Model = Model;
                            vc.Color = Color;
                            vc.CustName = CustName;
                            vc.Service = req;
                            vc.ServiceType = ServiceType;
                            vc.IsMembershipService = IsMembershipService;
                            nc.PushViewController(vc, true);
                        }, titleTxt: Common.Messages.SERVICE);
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
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened and the reason is : " + ex.Message);
                HideActivityIndicator();
            }
        }

        static bool IsThirtyMinuteDistance(string startTime, string endTime)
        {
            if (int.TryParse(startTime[..^3], out int startTimeHour) &&
                int.TryParse(startTime[3..], out int startTimeMinute) &&
                int.TryParse(endTime[..^3], out int endTimeHour) &&
                int.TryParse(endTime[3..], out int endTimeMinute))
            {
                var minutesDiffrent = endTimeMinute - startTimeMinute;
                if (endTimeHour != startTimeHour)
                {
                    var hoursDifferent = endTimeHour - startTimeHour;

                    if ((Math.Abs(hoursDifferent) == 23 || Math.Abs(hoursDifferent) == 1) && Math.Abs(minutesDiffrent) == 30)
                    {
                        return true;
                    }
                }
                else if (endTimeHour == startTimeHour && Math.Abs(minutesDiffrent) == 30)
                    return true;
            }
            return false;
        }

        void NavigateToEmailScreen()
        {
            UIViewController vc = GetViewController(GetHomeStorybpard(), nameof(EmailViewController));
            NavigateToWithAnim(vc);
        }
    }
}
