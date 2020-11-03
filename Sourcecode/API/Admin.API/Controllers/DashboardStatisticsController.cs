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
    public class DashboardStatisticsController : StriveControllerBase<IDashboardStatisticsBpl>
    {
        public DashboardStatisticsController(IDashboardStatisticsBpl dBpl) : base(dBpl) { }
        #region
        /// <summary>
        /// Method to retrieve Dashboard based on given LocationId.
        /// </summary>
        [HttpGet]
        [Route("GetDashboardStatisticsForLocationId/{id}")]
        public Result GetDashboardStatisticsForLocationId(int id) => _bplManager.GetDashboardStatisticsForLocationId(id);
        #endregion
    }
}
