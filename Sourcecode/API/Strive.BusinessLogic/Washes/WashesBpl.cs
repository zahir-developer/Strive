using Microsoft.Extensions.Caching.Distributed;
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

            if(washes.Job.ClientId == null && !string.IsNullOrEmpty(washes.Job.BarCode))
            {
                var clientVehicle = new VehicleRal(_tenant).AddDriveUpVehicle(washes.Job.LocationId, washes.Job.BarCode, washes.Job.Make, washes.Job.Model, washes.Job.Color, washes.Job.CreatedBy);
            }

            return ResultWrap(new WashesRal(_tenant).AddWashTime, washes, "Status");
        }

        public Result UpdateWashTime(WashesDto washes)
        {
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
    }
}
