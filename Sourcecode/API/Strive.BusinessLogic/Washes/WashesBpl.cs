﻿using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Washes;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Vehicle;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Washes
{
    public class WashesBpl : Strivebase, IWashesBpl
    {
        public WashesBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetAllWashTime(SearchDto searchDto)
        {
            return ResultWrap(new WashesRal(_tenant).GetAllWashTime, searchDto, "Washes");
        }

        public Result GetWashTimeDetail(int id)
        {
            return ResultWrap(new WashesRal(_tenant).GetWashTimeDetail, id, "WashesDetail");
        }

        public Result GetLastServiceVisit(SearchDto searchDto)
        {
            return ResultWrap(new WashesRal(_tenant).GetLastServiceVisit, searchDto, "WashesDetail");
        }


        public Result AddWashTime(WashesDto washes)
        {

            if (washes.Job.ClientId == null && !string.IsNullOrEmpty(washes.Job.BarCode))
            {
                var clientVehicle = new VehicleRal(_tenant).AddDriveUpVehicle(washes.Job.LocationId, washes.Job.BarCode, washes.Job.Make, washes.Job.Model, washes.Job.Color, washes.Job.CreatedBy);
            }

            return ResultWrap(new WashesRal(_tenant).AddWashTime, washes, "Status");
        }

        public Result UpdateWashTime(WashesDto washes)
        {
            //If barcode is not empty, check whether vehicle details is available.                        
            if (!string.IsNullOrEmpty(washes.Job.BarCode))
            {
                BusinessEntities.Model.ClientVehicle clientVehicle = null;
                if (washes.Job.VehicleId != null && washes.Job.VehicleId != 0)
                {
                    VehicleDetailViewModel VehicleDet = new VehicleRal(_tenant).GetVehicleId(washes.Job.VehicleId ?? 0);
                    if (VehicleDet != null)
                    {
                        //If available check whether any changes in vehicle make, model and color.
                        //If any changes are there update in client vehicle.
                        if ((VehicleDet.VehicleMakeId ?? 0) != washes.Job.Make || (VehicleDet.VehicleModelId ?? 0) != washes.Job.Model || (VehicleDet.ColorId ?? 0) != washes.Job.Color)
                        {
                            clientVehicle = new BusinessEntities.Model.ClientVehicle();
                            clientVehicle.VehicleId = VehicleDet.ClientVehicleId;
                            clientVehicle.ClientId = VehicleDet.ClientId;
                            clientVehicle.LocationId = VehicleDet.LocationId;
                            clientVehicle.VehicleNumber = VehicleDet.VehicleNumber;
                            clientVehicle.VehicleMfr = washes.Job.Make;
                            clientVehicle.VehicleModel = washes.Job.Model;
                            clientVehicle.VehicleModelNo = VehicleDet.VehicleModelNo;
                            clientVehicle.VehicleColor = washes.Job.Color;
                            clientVehicle.VehicleYear = VehicleDet.VehicleYear;
                            clientVehicle.Upcharge = VehicleDet.Upcharge;
                            clientVehicle.Barcode = washes.Job.BarCode;
                            clientVehicle.Notes = VehicleDet.Notes;
                            clientVehicle.IsActive = true;
                            clientVehicle.IsDeleted = false;
                            clientVehicle.MonthlyCharge = VehicleDet.MonthlyCharge;
                            clientVehicle.UpdatedDate = DateTime.Now;
                            clientVehicle.UpdatedBy = washes.Job.UpdatedBy;
                        }
                    }
                }
                else
                {
                    List<ClientVehicleViewModel> vehicleVm = new WashesRal(_tenant).GetByBarCode(washes.Job.BarCode);
                    if (vehicleVm != null && vehicleVm.FirstOrDefault() != null)
                    {
                        if (vehicleVm.First().VehicleMfr != washes.Job.Make || (vehicleVm?.First().VehicleModelId ?? 0) != washes.Job.Model || vehicleVm.First().VehicleColor != washes.Job.Color)
                        {
                            clientVehicle = new BusinessEntities.Model.ClientVehicle();
                            clientVehicle.VehicleId = vehicleVm.First().VehicleId;
                            //clientVehicle.ClientId = vehicleVm.First().ClientId;
                            clientVehicle.LocationId = washes.Job.LocationId;
                            clientVehicle.VehicleNumber = vehicleVm.First().VehicleNumber;
                            clientVehicle.VehicleMfr = washes.Job.Make;
                            clientVehicle.VehicleModel = washes.Job.Model;
                            //clientVehicle.VehicleModelNo = vehicleVm.First().VehicleModelNo;
                            clientVehicle.VehicleColor = washes.Job.Color;
                            clientVehicle.VehicleYear = vehicleVm.First().VehicleYear;
                            clientVehicle.Upcharge = vehicleVm.First().Upcharge;
                            clientVehicle.Barcode = washes.Job.BarCode;
                            clientVehicle.Notes = vehicleVm.First().Notes;
                            clientVehicle.IsActive = true;
                            clientVehicle.IsDeleted = false;
                            //clientVehicle.MonthlyCharge = vehicleVm.First().MonthlyCharge;
                            clientVehicle.UpdatedDate = DateTime.Now;
                            clientVehicle.UpdatedBy = washes.Job.UpdatedBy;
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

            if (washes.Job.ClientId == null && !string.IsNullOrEmpty(washes.Job.BarCode))
            {
                var clientVehicle = new VehicleRal(_tenant).AddDriveUpVehicle(washes.Job.LocationId, washes.Job.BarCode, washes.Job.Make, washes.Job.Model, washes.Job.Color, washes.Job.CreatedBy);
            }

            if (!string.IsNullOrEmpty(washes.DeletedJobItemId))
            {
                var deleteJobItem = new CommonRal(_tenant).DeleteJobItem(washes.DeletedJobItemId);
            }

            return ResultWrap(new WashesRal(_tenant).UpdateWashTime, washes, "Status");
        }
        public Result GetDailyDashboard(WashesDashboardDto dashboard)
        {
            return ResultWrap(new WashesRal(_tenant).GetDailyDashboard, dashboard, "Dashboard");
        }
        public Result GetByBarCode(string barcode)
        {
            return ResultWrap(new WashesRal(_tenant).GetByBarCode, barcode, "ClientAndVehicleDetail");
        }
        public Result GetMembershipListByVehicleId(int vehicleId)
        {
            return ResultWrap(new WashesRal(_tenant).GetMembershipListByVehicleId, vehicleId, "VehicleMembershipDetail");
        }

        public Result DeleteWashes(int id)
        {
            return ResultWrap(new WashesRal(_tenant).DeleteWashes, id, "Status");
        }


        public Result GetWashTimeByLocationId(WashTimeDto washTimeDto)
        {
            return ResultWrap(new WashesRal(_tenant).GetWashTime, washTimeDto, "WashTime");
        }

        public Result GetAllLocationWashTime(LocationStoreStatusDto locationStoreStatus)
        {
            return ResultWrap(new WashesRal(_tenant).GetAllLocationWashTime, locationStoreStatus, "Washes");
        }

        public Result GetWashVehiclePrint(PrintTicketDto printTicketDto)
        {
            return ResultWrap(VehicleCopyPrint, printTicketDto, "VehiclePrint");
        }

        public string VehicleCopyPrint(PrintTicketDto print)
        {
            string model = string.Empty;
            if (print.Job.VehicleModel.Contains("/"))
            {
                model = print.Job.VehicleModel.Substring(0, print.Job.VehicleModel.IndexOf("/"));
            }
            else
            {
                model = print.Job.VehicleModel;
            }

            var body = "^XA^AJN,20^FO50,50^FD" + DateTime.Now + "^FS^AJN,20^FO320,50^FD" + print.Job.Title + "^FS";
            body += "^AJN,30^FO50,90^FDIn:" + print.Job.InTime + "^FS^A0N,30,30^FO50,130^FDOut:" + print.Job.TimeOut + "^FS^AJN,30^FO50,170^FDClient:" + print.Job.ClientName + "^FS^AJN,20^FO50,220^FDVehicle:" + model + "^FS^AJN,20^FO50,250^GB700,3,3^FS";
            body += "^AJN,30^FO550,90^FD 7327112021 ^FS^AJN,30^FO495,170^FD(234)235 - 3453^FS^AJN,20^FO440,220^FD" + print.Job.VehicleMake + "^FS^AJN,20^FO690,220^FD" + print.Job.VehicleColor + "^FS^AJN,30";
            int checkboxaxis = 280;

            if (print.JobItem != null)
{
                foreach (var jobItem in print.JobItem)
                {
                    body += "^FO50," + checkboxaxis + "^GB20,20,1^FS^AJN,30^FO80," + checkboxaxis + "^FD" + jobItem.ServiceName + "^FS";
                    checkboxaxis += 40;
                }
            }


            body += "^AJN,30^FO80," + (checkboxaxis + 80) + "^FDTicket Number:" + print.Job.TicketNumber.ToString() + "^FS^XZ";
            return body;

        }

        string CustomerPrintReceipt(PrintTicketDto print)
        {
            string model = string.Empty;
            if (print.Job.VehicleModel.Contains("/"))
            {
                model = print.Job.VehicleModel.Substring(0, print.Job.VehicleModel.IndexOf("/"));
            }
            else
            {
                model = print.Job.VehicleModel;
            }
            var body = "^XA^AJN,30^FO50,50^FDClient:" + print.Job.ClientName + "^FS^AJN,30^FO540,50^FD" + print.Job.PhoneNumber + "^FS";

            body += "^AJN,20^FO50,100^FDVehicle:" + model + " ^FS" +
            "^AJN,20^FO420,100^FD" + print.Job.VehicleMake + "^FS" +
            "^AJN,20^FO690,100^FD" + print.Job.VehicleColor + "^FS";

            var totalAmt = 0f;
            int yaxis = 300;
            body += "^AJN,30^A0N,30,30^FO480,200^FDHand Car Washes^FS";

            body += "^AJN,30^A0N,30,30^FO480,300^FDVehicle Upcharge^FS";
            if (print.JobItem != null)
            {
                foreach (var jobItem in print.JobItem)
                {
                    var price = jobItem.Price.ToString();
                    if ((jobItem.Price % 1) == 0)
                    {
                        price += ":00";
                    }
                    else
                    {
                        var values = jobItem.Price.ToString().Split('.');
                        price = (int)jobItem.Price + ":" + values[1];
                    }

                    if (jobItem.ServiceName != string.Empty)
                    {
                        body += "^AJN,20^FO480,240^FD" + jobItem.ServiceName.Replace(" ", string.Empty) + "-$" + price + "^FS";
                    }
                    else
                    {
                        yaxis += 40;
                        body += "^AJN,20^FO480," + yaxis + "^FD" + jobItem.ServiceName.Replace(" ", string.Empty) + "-$" + price + "^FS";
                    }

                    totalAmt += float.Parse(jobItem.Price.ToString());

                    //Amount = totalAmt;
                }
            }

            body += "^AJN,30^A0N,30,30^FO480," + (yaxis + 100) + "^FDAir Fresheners^FS";

            DateTime intime = DateTime.Parse(print.Job.InTime.Substring(10));
            DateTime Outtime = DateTime.Parse(print.Job.TimeOut);
            int EstimatedTime = Outtime.Minute - intime.Minute;


            body += "^AJN,20^FO50,600^FDIn:" + print.Job.InTime + "^FS" +
            "^AJN,20^FO50,640^FDOut:" + print.Job.InTime + "^FS" +
            "^AJN,20^FO50,680^FDEst " + EstimatedTime.ToString() + "Min^FS";



            body += @"^AJN,30
                ^A0N,30,30^FO300,720^FDNew Customer Info^FS
                ^AJN,30^FO60,900^FDName^FS
                ^AJN,30^FO160,920^GB600,3,3^FS
                ^AJN,30^FO60,940^FDPhone^FS^FO160,960^GB600,3,3^FS
                ^AJN,30^FO60,980^FDEMail^FS
                ^AJN,30^FO160,1000^GB600,3,3^FS";
            if (print.Job.Barcode != null)
            {
                body += @"^AJN,20
                        ^AD^BY5,2,100
                        ^AJN,20^FO100,750^BC^FD" + print.Job.Barcode + "^FS";
            }

            body += "^AJN,20^FO50,1040^FDNote^FS";

            body += "^AJN,20^FO60,140^AD^BY4^FWB^BC,100,Y,N,N^FD" + print.Job.TicketNumber + "^FS";

            body += "^AJN,20^FO180,200^GFA,11400,11400,38," + ZebraPrint.MammothLogo + "^FS^XZ";

            return body;
        }
    }
}
