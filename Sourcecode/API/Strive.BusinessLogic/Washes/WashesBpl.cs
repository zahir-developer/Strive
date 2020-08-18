using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.Washes;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Vehicle;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Washes
{
    public class WashesBpl : Strivebase, IWashesBpl
    {
        public WashesBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetAllWashTime()
        {
            return ResultWrap(new WashesRal(_tenant).GetAllWashTime, "Washes");
        }

        public Result GetWashTimeDetail(int id)
        {
            return ResultWrap(new WashesRal(_tenant).GetWashTimeDetail, id, "WashesDetail");
        }
        public Result AddWashTime(WashesDto washes)
        {
            var result = new VehicleBpl(_cache, _tenant).GetVehicleId(washes.Job.VehicleId);
            //if(result.ResultData.)
            //result.ResultData.FirstOrDefault(g=>g.)
            foreach (var item in result.ResultData)
            {
                //if (item.toFloat() = "")
                //{
                //    var check = new CommonBpl(_cache, _tenant).RandomNumber(10);
                //}
            }

            return ResultWrap(new WashesRal(_tenant).AddWashTime, washes, "Status");
        }

        public Result UpdateWashTime(WashesDto washes)
        {
            return ResultWrap(new WashesRal(_tenant).UpdateWashTime, washes, "Status");
        }
        public Result GetDailyDashboard(DashboardDto dashboard)
        {
            return ResultWrap(new WashesRal(_tenant).GetDailyDashboard,dashboard, "Dashboard");
        }
        public Result GetByBarCode(string barcode)
        {
            return ResultWrap(new WashesRal(_tenant).GetByBarCode, barcode, "ClientAndVehicleDetail");
        }
        public Result DeleteWashes(int id)
        {
            return ResultWrap(new WashesRal(_tenant).DeleteWashes, id, "Status");
        }

    }
}
