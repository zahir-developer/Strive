using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Washes;
using Strive.BusinessLogic.Washes;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class WashesController : StriveControllerBase<IWashesBpl>
    {
        public WashesController(IWashesBpl colBpl) : base(colBpl) { }

        #region GET

        [HttpGet]
        [Route("GetWashTimeDetail/{id}")]
        public Result GetWashTimeDetail(int id) => _bplManager.GetWashTimeDetail(id);

        [HttpGet]
        [Route("GetByBarCode/{barcode}")]
        public Result GetByBarCode(string barcode) => _bplManager.GetByBarCode(barcode);

        [HttpGet]
        [Route("GetMembershipListByVehicleId/{vehicleId}")]
        public Result GetMembershipListByVehicleId(int vehicleId) => _bplManager.GetMembershipListByVehicleId(vehicleId);


        [HttpGet]
        [Route("GetAllLocationWashTime")]
        public Result GetAllLocationWashTime(LocationStoreStatusDto locationStoreStatusDto) => _bplManager.GetAllLocationWashTime(locationStoreStatusDto);

        #endregion

        #region POST

        [HttpPost]
        [Route("GetAllWashes")]
        public Result GetAllWashTime([FromBody] SearchDto searchDto) => _bplManager.GetAllWashTime(searchDto);
         
        [HttpPost]
        [Route("AddWashTime")]
        public Result AddWashTime([FromBody] WashesDto washes) => _bplManager.AddWashTime(washes);

        [HttpPost]
        [Route("UpdateWashTime")]
        public Result UpdateWashTime([FromBody] WashesDto washes) => _bplManager.UpdateWashTime(washes);
        
        [HttpPost]
        [Route("DashboardCount")]
        public Result GetDailyDashboard([FromBody]WashesDashboardDto dashboard) => _bplManager.GetDailyDashboard(dashboard);

        #endregion

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteWashes(int id) => _bplManager.DeleteWashes(id);


        [HttpPost]
        [Route("GetWashTimeByLocationId")]
        public Result GetLocationById([FromBody] WashTimeDto washTimeDto) => _bplManager.GetWashTimeByLocationId(washTimeDto);


    }
}
