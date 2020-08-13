﻿using Admin.API.Helpers;
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
        [HttpGet]
        [Route("DashboardCount/{id}")]
        public Result GetDailyDashboard(int id) => _bplManager.GetDailyDashboard(id);
        #endregion
    }
}
