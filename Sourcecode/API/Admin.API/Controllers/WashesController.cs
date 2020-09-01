using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetAllWashTime")]
        public Result GetAllWashTime() => _bplManager.GetAllWashTime();
        #endregion
        #region
        [HttpGet]
        [Route("GetWashTimeDetail/{id}")]
        public Result GetWashTimeDetail(int id) => _bplManager.GetWashTimeDetail(id);
        #endregion
        [HttpPost]
        [Route("AddWashTime")]
        public Result AddWashTime([FromBody] WashesDto washes) => _bplManager.AddWashTime(washes);
        #region
        [HttpPost]
        [Route("UpdateWashTime")]
        public Result UpdateWashTime([FromBody] WashesDto washes) => _bplManager.UpdateWashTime(washes);
        #endregion
        #region
        [HttpPost]
        [Route("DashboardCount")]
        public Result GetDailyDashboard([FromBody]DashboardDto dashboard) => _bplManager.GetDailyDashboard(dashboard);
        #endregion
        #region
        [HttpGet]
        [Route("GetByBarCode/{barcode}")]
        public Result GetByBarCode(string barcode) => _bplManager.GetByBarCode(barcode);
        [HttpGet]
        [Route("GetMembershipListByVehicleId/{vehicleId}")]
        public Result GetMembershipListByVehicleId(int vehicleId) => _bplManager.GetMembershipListByVehicleId(vehicleId);  
        #endregion
        #region
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteWashes(int id) => _bplManager.DeleteWashes(id);
        #endregion
        #region
        [HttpGet]
        [Route("GetTicketNumber")]
        public string GetTicketNumber() => _bplManager.GetTicketNumber();
        #endregion
    }
}
