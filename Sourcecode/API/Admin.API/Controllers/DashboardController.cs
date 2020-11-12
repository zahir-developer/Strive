using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Dashboard;
using Strive.BusinessLogic.DashboardStatistics;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]

    [Route("Admin/[Controller]")]
    public class DashboardController : StriveControllerBase<IDashboardBpl>
    {
        public DashboardController(IDashboardBpl dBpl) : base(dBpl) { }
        #region POST
        /// <summary>
        /// Method to retrieve Dashboard based on given LocationId.
        /// </summary>
        [HttpPost]
        [Route("GetDashboardStatistics")]
        public Result GetDashboardStatistics([FromBody] DashboardDto dashboard) => _bplManager.GetDashboardStatistics(dashboard);
        #endregion
    }
}
