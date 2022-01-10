using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Details;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Details
{
    public class DetailsBpl : Strivebase, IDetailsBpl
    {
        public DetailsBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {

        }

        public Result AddDetails(DetailsDto details)
        {
            //If barcode is not empty, check whether vehicle details is available.                        
            if (!string.IsNullOrEmpty(details.Job.BarCode))
            {
                BusinessEntities.Model.ClientVehicle clientVehicle = null;
                if (details.Job.VehicleId != null && details.Job.VehicleId != 0)
                {
                    VehicleDetailViewModel VehicleDet = new VehicleRal(_tenant).GetVehicleId(details.Job.VehicleId ?? 0);
                    if (VehicleDet != null)
                    {
                        //If available check whether any changes in vehicle make, model and color.
                        //If any changes are there update in client vehicle.
                        if ((VehicleDet.VehicleMakeId ?? 0) != details.Job.Make || (VehicleDet.VehicleModelId ?? 0) != details.Job.Model || (VehicleDet.ColorId ?? 0) != details.Job.Color)
                        {
                            clientVehicle = new BusinessEntities.Model.ClientVehicle();
                            clientVehicle.VehicleId = VehicleDet.ClientVehicleId;
                            clientVehicle.ClientId = VehicleDet.ClientId;
                            clientVehicle.LocationId = VehicleDet.LocationId;
                            clientVehicle.VehicleNumber = VehicleDet.VehicleNumber;
                            clientVehicle.VehicleMfr = details.Job.Make;
                            clientVehicle.VehicleModel = details.Job.Model;
                            clientVehicle.VehicleModelNo = VehicleDet.VehicleModelNo;
                            clientVehicle.VehicleColor = details.Job.Color;
                            clientVehicle.VehicleYear = VehicleDet.VehicleYear;
                            clientVehicle.Upcharge = VehicleDet.Upcharge;
                            clientVehicle.Barcode = details.Job.BarCode;
                            clientVehicle.Notes = VehicleDet.Notes;
                            clientVehicle.IsActive = true;
                            clientVehicle.IsDeleted = false;
                            clientVehicle.MonthlyCharge = VehicleDet.MonthlyCharge;
                            clientVehicle.UpdatedDate = DateTime.Now;
                            clientVehicle.UpdatedBy = details.Job.UpdatedBy;

                        }
                    }
                }
                else
                {
                    List<ClientVehicleViewModel> vehicleVm = new WashesRal(_tenant).GetByBarCode(details.Job.BarCode);
                    if (vehicleVm != null && vehicleVm.FirstOrDefault() != null)
                    {
                        if (vehicleVm.First().VehicleMfr != details.Job.Make || (vehicleVm?.First().VehicleModelId ?? 0) != details.Job.Model || vehicleVm.First().VehicleColor != details.Job.Color)
                        {
                            clientVehicle = new BusinessEntities.Model.ClientVehicle();
                            clientVehicle.VehicleId = vehicleVm.First().VehicleId;
                            //clientVehicle.ClientId = vehicleVm.First().ClientId;
                            clientVehicle.LocationId = details.Job.LocationId;
                            clientVehicle.VehicleNumber = vehicleVm.First().VehicleNumber;
                            clientVehicle.VehicleMfr = details.Job.Make;
                            clientVehicle.VehicleModel = details.Job.Model;
                            //clientVehicle.VehicleModelNo = vehicleVm.First().VehicleModelNo;
                            clientVehicle.VehicleColor = details.Job.Color;
                            clientVehicle.VehicleYear = vehicleVm.First().VehicleYear;
                            clientVehicle.Upcharge = vehicleVm.First().Upcharge;
                            clientVehicle.Barcode = details.Job.BarCode;
                            clientVehicle.Notes = vehicleVm.First().Notes;
                            clientVehicle.IsActive = true;
                            clientVehicle.IsDeleted = false;
                            //clientVehicle.MonthlyCharge = vehicleVm.First().MonthlyCharge;
                            clientVehicle.UpdatedDate = DateTime.Now;
                            clientVehicle.UpdatedBy = details.Job.UpdatedBy;
                        }
                    }
                }
                if (clientVehicle != null)
                {
                    BusinessEntities.Model.ClientVehicleModel ClientVehicleModel = new BusinessEntities.Model.ClientVehicleModel();
                    ClientVehicleModel.ClientVehicle = clientVehicle;

                    var saveVehicle = new VehicleRal(_tenant).SaveVehicle(ClientVehicleModel);
                    if (!saveVehicle)
                        return ResultWrap<BusinessEntities.Model.ClientVehicle>(false, "Result", "Failed to save vehicle details.");
                }
            }

            if (!string.IsNullOrEmpty(details.DeletedJobItemId))
            {
                var deleteJobItem = new CommonRal(_tenant).DeleteJobItem(details.DeletedJobItemId);
            }

            if (details.Job.ClientId == null && !string.IsNullOrEmpty(details.Job.BarCode))
            {
                var clientVehicle = new VehicleRal(_tenant).AddDriveUpVehicle(details.Job.LocationId, details.Job.BarCode, details.Job.Make, details.Job.Model, details.Job.Color, details.Job.CreatedBy);
            }

            var baySlot = GetBaySlot(details.Job.JobId, details.JobDetail.BayId.GetValueOrDefault(), details.Job.JobDate, details.Job.TimeIn.GetValueOrDefault(), details.Job.EstimatedTimeOut.GetValueOrDefault());

            details.BaySchedule = baySlot;

            return ResultWrap(new DetailsRal(_tenant).UpdateDetails, details, "Status");
        }

        private List<BusinessEntities.Model.BaySchedule> GetBaySlot(int jobId, int bayId, DateTime jobDate, DateTimeOffset initialTimeIn, DateTimeOffset finalDueTime)
        {

            var bayScheduleList = new List<BusinessEntities.Model.BaySchedule>();

            DateTime scheduleDate = jobDate;

            var initialHour = initialTimeIn.Hour;

            var initialDay = initialTimeIn.Day;
            var initialminutes = initialTimeIn.Minute;


            var finalHour = finalDueTime.Hour;
            var finalminutes = finalDueTime.Minute;
            var finalDay = finalDueTime.Day;


            var tempfinalminutes = finalminutes;
            var tempinitialHour = initialHour;
            var tempinitialDay = initialDay;

            var baySchedule = new BusinessEntities.Model.BaySchedule();


            if (((finalHour == initialHour && initialDay == finalDay) || (initialHour == finalHour)) && ((initialminutes) == 30 && initialminutes != finalminutes && finalminutes == 30))
            {

                int startTime;
                int endTime;

                var hour = initialHour;
                var endHour = initialHour;

                //HH:00, HH:30
                if (initialminutes < tempfinalminutes)
                {
                    startTime = 0;
                    endTime = 30;
                    tempfinalminutes = 0;
                }
                //HH:30, HH+1:00
                else if (finalHour > initialHour)
                {
                    startTime = 30;
                    endTime = 0;
                    endHour = initialHour + 1;
                }
                else //HH:30, HH:00
                {
                    startTime = 30;
                    endTime = 0;
                    tempfinalminutes = 30;
                    hour = initialHour + 1;
                }

                baySchedule = new BusinessEntities.Model.BaySchedule
                {
                    BayScheduleId = 0,
                    BayId = bayId,
                    JobId = jobId,
                    ScheduleDate = scheduleDate,
                    ScheduleInTime = new TimeSpan(hour, startTime, 0),
                    ScheduleOutTime = new TimeSpan(endHour, endTime, 0),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = 0,
                    UpdatedBy = 0,
                };


                if (hour >= 7 && (hour <= 19 && endTime <= 30))
                    bayScheduleList.Add(baySchedule);
            }
            else
            {

                //Loop 1
                int startTime;
                int endTime;
                var hour = initialHour;
                var endHour = initialHour;
                int tempEndHour = -1;
                int tempEndMinute = 0;

                tempinitialHour = initialHour;
                tempinitialDay = initialDay;
                //2:00 > 1:00
                while (true)
                {

                    if (tempinitialHour == 23)
                    {
                        tempinitialDay++;
                        tempinitialHour = -1;
                    }

                    var temp = new List<int>();

                    hour = tempinitialHour;
                    var tempInitialminutes = initialminutes;

                    if (tempEndHour == 0)
                    {
                        scheduleDate = scheduleDate.AddDays(1);
                    }


                    if (tempEndHour == -1)
                    {
                        tempEndHour = 0;
                    }
                    for (int i = 1; i <= 2; i++)
                    {

                        if (finalminutes != tempInitialminutes || finalHour != tempinitialHour || finalDay != tempinitialDay)
                        {

                            if (finalminutes <= tempEndMinute && finalHour == tempEndHour && finalDay == tempinitialDay)
                                break;

                            //HH:30, HH:00
                            if (tempInitialminutes > tempfinalminutes)
                            {
                                startTime = 30;
                                endTime = 0;
                                tempfinalminutes = 30;
                                tempInitialminutes = 0;
                                endHour = tempinitialHour + 1;
                            }
                            else
                              //HH:00, HH:30
                              if (tempInitialminutes < tempfinalminutes)
                            {
                                startTime = 0;
                                endTime = 30;
                                tempfinalminutes = 0;
                                tempInitialminutes = 30;
                                if (endHour != hour)
                                    hour = endHour;

                                if (tempfinalminutes < 30)
                                    tempfinalminutes = 30;
                            }
                            else if (tempInitialminutes == tempfinalminutes && (tempInitialminutes == 0))
                            {
                                startTime = 0;
                                endTime = 30;
                                tempInitialminutes = 30;
                            }
                            else if (tempInitialminutes == tempfinalminutes && (tempInitialminutes == 30))
                            {
                                startTime = 30;
                                endTime = 0;
                                tempInitialminutes = 0;
                                tempfinalminutes = 30;
                                endHour = tempinitialHour + 1;

                            }
                            else //HH:30, HH:00
                            {
                                startTime = 30;
                                endTime = 0;
                                tempfinalminutes = 30;
                                endHour = tempinitialHour + 1;
                            }

                            baySchedule = new BusinessEntities.Model.BaySchedule
                            {
                                BayScheduleId = 0,
                                BayId = bayId,
                                JobId = jobId,
                                ScheduleDate = scheduleDate,
                                ScheduleInTime = new TimeSpan(hour, startTime, 0),
                                ScheduleOutTime = new TimeSpan(endHour, endTime, 0),
                                IsActive = true,
                                IsDeleted = false,
                                CreatedBy = 0,
                                UpdatedBy = 0,
                            };


                            if (hour >= 7 && hour <= 19)
                            {
                                bayScheduleList.Add(baySchedule);
                            }

                            tempEndHour = endHour;

                            tempEndMinute = endTime;
                        }
                    }


                    if (finalHour <= tempinitialHour && tempinitialDay == finalDay)
                        break;


                    tempinitialHour++;
                }

            }

            return bayScheduleList;

        }

        public Result UpdateDetails(DetailsDto details)
        {
            //If barcode is not empty, check whether vehicle details is available.                        
            if (!string.IsNullOrEmpty(details.Job.BarCode))
            {
                BusinessEntities.Model.ClientVehicle clientVehicle = null;
                if (details.Job.VehicleId != null && details.Job.VehicleId != 0)
                {
                    VehicleDetailViewModel VehicleDet = new VehicleRal(_tenant).GetVehicleId(details.Job.VehicleId ?? 0);
                    if (VehicleDet != null)
                    {
                        //If available check whether any changes in vehicle make, model and color.
                        //If any changes are there update in client vehicle.
                        if ((VehicleDet.VehicleMakeId ?? 0) != details.Job.Make || (VehicleDet.VehicleModelId ?? 0) != details.Job.Model || (VehicleDet.ColorId ?? 0) != details.Job.Color)
                        {
                            clientVehicle = new BusinessEntities.Model.ClientVehicle();
                            clientVehicle.VehicleId = VehicleDet.ClientVehicleId;
                            clientVehicle.ClientId = VehicleDet.ClientId;
                            clientVehicle.LocationId = VehicleDet.LocationId;
                            clientVehicle.VehicleNumber = VehicleDet.VehicleNumber;
                            clientVehicle.VehicleMfr = details.Job.Make;
                            clientVehicle.VehicleModel = details.Job.Model;
                            clientVehicle.VehicleModelNo = VehicleDet.VehicleModelNo;
                            clientVehicle.VehicleColor = details.Job.Color;
                            clientVehicle.VehicleYear = VehicleDet.VehicleYear;
                            clientVehicle.Upcharge = VehicleDet.Upcharge;
                            clientVehicle.Barcode = details.Job.BarCode;
                            clientVehicle.Notes = VehicleDet.Notes;
                            clientVehicle.IsActive = true;
                            clientVehicle.IsDeleted = false;
                            clientVehicle.MonthlyCharge = VehicleDet.MonthlyCharge;
                            clientVehicle.UpdatedDate = DateTime.Now;
                            clientVehicle.UpdatedBy = details.Job.UpdatedBy;

                        }
                    }
                }
                else
                {
                    List<ClientVehicleViewModel> vehicleVm = new WashesRal(_tenant).GetByBarCode(details.Job.BarCode);
                    if (vehicleVm != null && vehicleVm.FirstOrDefault() != null)
                    {
                        if (vehicleVm.First().VehicleMfr != details.Job.Make || (vehicleVm?.First().VehicleModelId ?? 0) != details.Job.Model || vehicleVm.First().VehicleColor != details.Job.Color)
                        {
                            clientVehicle = new BusinessEntities.Model.ClientVehicle();
                            clientVehicle.VehicleId = vehicleVm.First().VehicleId;
                            //clientVehicle.ClientId = vehicleVm.First().ClientId;
                            clientVehicle.LocationId = details.Job.LocationId;
                            clientVehicle.VehicleNumber = vehicleVm.First().VehicleNumber;
                            clientVehicle.VehicleMfr = details.Job.Make;
                            clientVehicle.VehicleModel = details.Job.Model;
                            //clientVehicle.VehicleModelNo = vehicleVm.First().VehicleModelNo;
                            clientVehicle.VehicleColor = details.Job.Color;
                            clientVehicle.VehicleYear = vehicleVm.First().VehicleYear;
                            clientVehicle.Upcharge = vehicleVm.First().Upcharge;
                            clientVehicle.Barcode = details.Job.BarCode;
                            clientVehicle.Notes = vehicleVm.First().Notes;
                            clientVehicle.IsActive = true;
                            clientVehicle.IsDeleted = false;
                            //clientVehicle.MonthlyCharge = vehicleVm.First().MonthlyCharge;
                            clientVehicle.UpdatedDate = DateTime.Now;
                            clientVehicle.UpdatedBy = details.Job.UpdatedBy;
                        }
                    }
                }
                if (clientVehicle != null)
                {
                    BusinessEntities.Model.ClientVehicleModel ClientVehicleModel = new BusinessEntities.Model.ClientVehicleModel();
                    ClientVehicleModel.ClientVehicle = clientVehicle;

                    var saveVehicle = new VehicleRal(_tenant).SaveVehicle(ClientVehicleModel);
                    if (!saveVehicle)
                        return ResultWrap<BusinessEntities.Model.ClientVehicle>(false, "Result", "Failed to save vehicle details.");
                }
            }

            if (!string.IsNullOrEmpty(details.DeletedJobItemId))
            {
                var deleteJobItem = new CommonRal(_tenant).DeleteJobItem(details.DeletedJobItemId);
            }

            if (details.Job.ClientId == null && !string.IsNullOrEmpty(details.Job.BarCode))
            {
                var clientVehicle = new VehicleRal(_tenant).AddDriveUpVehicle(details.Job.LocationId, details.Job.BarCode, details.Job.Make, details.Job.Model, details.Job.Color, details.Job.CreatedBy);
            }

            var updateDetail = new DetailsRal(_tenant).UpdateDetails(details);

            return ResultWrap(updateDetail, "Status");
        }

        public Result AddServiceEmployee(JobServiceEmployeeDto jobServiceEmployee)
        {
            var assignDetailer = new DetailsRal(_tenant).AddServiceEmployee(jobServiceEmployee);

            if (assignDetailer)
            {
                if (jobServiceEmployee.JobId.GetValueOrDefault() != 0 || jobServiceEmployee.JobId != null)
                    DetailAssignedToEmployee(jobServiceEmployee.JobId.GetValueOrDefault());
            }
            return ResultWrap(assignDetailer, "Status");
        }

        public Result GetBaySchedulesDetails(DetailsGridDto detailsGrid)
        {
            return ResultWrap(new DetailsRal(_tenant).GetBaySchedulesDetails, detailsGrid, "GetBaySchedulesDetails");
        }
        public Result GetDetailsById(int id)
        {
            return ResultWrap(new DetailsRal(_tenant).GetDetailsById, id, "DetailsForDetailId");
        }
        public Result GetAllBayById(int id)
        {
            return ResultWrap(new DetailsRal(_tenant).GetAllBayById, id, "BayDetailsForLocationId");
        }
        public Result GetPastClientNotesById(int id)
        {
            return ResultWrap(new DetailsRal(_tenant).GetPastClientNotesById, id, "PastClientNotesByClientId");
        }
        public Result GetJobType()
        {
            return ResultWrap(new DetailsRal(_tenant).GetJobType, "GetJobType");
        }
        public Result GetAllDetails(DetailsGridDto detailsGrid)
        {
            return ResultWrap(new DetailsRal(_tenant).GetAllDetails, detailsGrid, "DetailsGrid");
        }
        public Result GetAllDetailSearch(SearchDto searchDto)
        {
            return ResultWrap(new DetailsRal(_tenant).GetAllDetailSearch, searchDto, "DetailsGrid");
        }
        public Result DeleteDetails(int id)
        {
            return ResultWrap(new DetailsRal(_tenant).DeleteDetails, id, "DeleteRespectiveDetail");
        }

        public Result GetDetailScheduleStatus(DetailScheduleDto scheduleDto)
        {
            return ResultWrap(new DetailsRal(_tenant).GetDetailScheduleStatus, scheduleDto, "Status");
        }

        public void DetailAssignedToEmployee(int jobId)
        {

            var detail = new DetailsRal(_tenant).GetDetailsById(jobId);

            if (!string.IsNullOrEmpty(detail.Email?.Email?.Trim()))
            {

                var commonBpl = new CommonBpl(_cache, _tenant);

                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("{{CustomerName}}", detail.Details.ClientName);
                keyValues.Add("{{TicketNumber}}", detail.Details.TicketNumber);
                keyValues.Add("{{JobType}}", "Detail");
                keyValues.Add("{{JobDate}}", detail.Details.JobDate.ToString("MM-dd-yyy"));
                keyValues.Add("{{Vehicle}}", detail.Details.VehicleMake + "," + detail.Details.VehicleModel + "," + detail.Details.VehicleColor);
                keyValues.Add("{{Barcode}}", detail.Details.Barcode);

                string services = string.Empty;

                foreach (var item in detail.DetailsJobServiceEmployee)
                {
                    services += "<tr>";
                    services += "<td>" + item.EmployeeId + "</td>";
                    services += "<td>" + item.EmployeeName + "</td>";
                    services += "<td>" + item.CommissionAmount + "</td>";
                    services += "<td>" + item.ServiceName + "</td>";
                    services += "</tr>";
                }

                keyValues.Add("{{Services}}", services);

                commonBpl.SendEmail(HtmlTemplate.DetailAssignedToEmployee, detail.Email.Email, keyValues, "Detail Add/Update - Job Assigned !");
            }
        }

        public Result UpdateJobStatus(JobStatusDto jobStatus)
        {
            return ResultWrap(new DetailsRal(_tenant).UpdateJobStatus, jobStatus, "Status");
        }

    }
}
