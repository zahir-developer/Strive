using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class SchedulePreviewDetailsViewModel : BaseViewModel
    {
        public GetTicketNumber ticketNumber { get; set; }
        public List<Code> jobStatusCodes = new List<Code>();
        public List<Code> jobTypeCodes = new List<Code>();
        public int jobStatusID = 67;
        public int jobTypeID = 58;

        public async Task BookNow()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);          

            var ticketResult = await AdminService.GetTicketNumber(1);
            var codeResult = await AdminService.GetCodes();

            if(ticketResult.GetTicketNumber != null)
            {
                ticketNumber = ticketResult.GetTicketNumber;
            }
            else
            {
                _userDialog.Alert("There is no ticket number");
            }
            if(codeResult.Codes != null)
            {
                foreach(var item in codeResult.Codes)
                {
                    if(item.Category == "JobStatus")
                    {
                        jobStatusCodes.Add(item);
                    }
                }
                int jobStatusId = jobStatusCodes.Where(x => x.CodeValue.Equals("Waiting")).FirstOrDefault().CodeId;
                foreach (var item in codeResult.Codes)
                {
                    if (item.Category == "JobType")
                    {
                        jobTypeCodes.Add(item);
                    }
                }
                int jobTypeID = jobTypeCodes.Where(x => x.CodeValue.Equals("Detail")).FirstOrDefault().CodeId;
            }
            else
            {
                _userDialog.Alert("No code value");
            }
            _userDialog.HideLoading();

            var newSchedule = prepareDetailSchedule();
            await AdminService.ScheduleDetail(newSchedule);
        }

        public async void NavtoScheduleDate()
        {
            await _navigationService.Navigate<ScheduleAppointmentDateViewModel>();
        }
        public async void NavtoScheduleView()
        {
            await _navigationService.Navigate<ScheduleViewModel>();
        }
        public async void NavtoConfirmSchedule()
        {
            await _navigationService.Navigate<ScheduleConfirmationViewModel>();
        }

        private DetailSchedule prepareDetailSchedule()
        {
            var inTime = DateUtils.GetDateFromString(CustomerScheduleInformation.ScheduleTime);            
            var estimatedTime = inTime.AddHours(CustomerScheduleInformation.ScheduleServiceEstimatedTime);
            var EstimatedOutTime = DateUtils.GetStringFromDate(estimatedTime) + "+05:30";
            
            Job newJob = new Job()
            {
                jobId = ticketNumber.JobId,
                ticketNumber = int.Parse(ticketNumber.TicketNumber),
                barcode = CustomerScheduleInformation.ScheduleSelectedVehicle.Barcode,
                locationId = CustomerScheduleInformation.ScheduleLocationCode.ToString(),
                clientId = CustomerInfo.ClientID,
                vehicleId = CustomerScheduleInformation.ScheduleSelectedVehicle.VehicleId,
                make = CustomerScheduleInformation.ScheduleSelectedVehicle.VehicleMakeId,
                model = CustomerScheduleInformation.ScheduleSelectedVehicle.VehicleModelId,
                color = CustomerScheduleInformation.ScheduleSelectedVehicle.VehicleColorId,
                jobType = jobTypeID,
                jobDate = CustomerScheduleInformation.ScheduleFullDate,
                jobStatus = jobStatusID,
                timeIn = CustomerScheduleInformation.ScheduleTime + ":00+05:30",
                estimatedTimeOut = EstimatedOutTime,
                actualTimeOut = null,
                isActive = true,
                isDeleted = false,
                createdBy = 0,
                updatedBy = 0,
                notes = null
            };

            JobDetail jobDetail = new JobDetail()
            {
                jobDetailId = 0,
                jobId = ticketNumber.JobId,
                bayId = CustomerScheduleInformation.ScheduledBayId,
                isActive = true,
                isDeleted = false,
                createdBy = 0,
                updatedBy = 0
            };

            JobItem jobItem = new JobItem()
            {
                jobItemId = 0,
                jobId = ticketNumber.JobId,
                serviceId = CustomerScheduleInformation.ScheduleServiceID,
                isActive = true,
                isDeleted = false,
                commission = 0,
                price = CustomerScheduleInformation.ScheduleServicePrice,
                quantity = 1,
                createdBy = 0,
                updatedBy = 0
            };
            List<JobItem> jobList = new List<JobItem>();
            jobList.Add(jobItem);

            List<BaySchedule> bayList = new List<BaySchedule>();

            while (inTime != estimatedTime)
            {
                var outTime = inTime.AddMinutes(30);
                BaySchedule bayItem = new BaySchedule()
                {
                    bayScheduleId = 0,
                    bayId = CustomerScheduleInformation.ScheduledBayId,
                    jobId = ticketNumber.JobId,
                    scheduleDate = CustomerScheduleInformation.ScheduleFullDate,
                    scheduleInTime = inTime.TimeOfDay.ToString().Substring(0, 5),
                    //scheduleInTime = inTime.ToShortTimeString().Substring(0,5),
                    scheduleOutTime = outTime.TimeOfDay.ToString().Substring(0,5),
                    isActive = true,
                    isDeleted = false,
                    createdBy = 0,
                    updatedBy = 0
                };
                inTime = outTime;
                bayList.Add(bayItem);
            }

            DetailSchedule detail = new DetailSchedule()
            {
                job = newJob,
                jobItem = jobList,
                jobDetail = jobDetail,
                BaySchedule = null
            };

            return detail;
        }
    }
}
