using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        #region
        /// <summary>
        /// Method to retrieve Dashboard based on given LocationId.
        /// </summary>
        [HttpGet]
        [Route("GetDashboardStatistics/{locationId}")]
        public Result GetDashboardStatistics(int locationId) => _bplManager.GetDashboardStatistics(locationId);
        #endregion
    }
}
