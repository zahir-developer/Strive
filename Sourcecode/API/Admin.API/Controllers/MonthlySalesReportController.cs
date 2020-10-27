using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessLogic.MonthlySalesReport;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]

    [Route("Admin/[Controller]")]
    public class MonthlySalesReportController : StriveControllerBase<IMonthlySalesReportBpl>
    {
        public MonthlySalesReportController(IMonthlySalesReportBpl msBpl) : base(msBpl) { }
        #region
        /// <summary>
        /// Method to Get MonthlySales Report in Grid.
        /// </summary>
        [HttpPost]
        [Route("GetMonthlySalesReport")]
        public Result GetMonthlySalesReport([FromBody] MonthlySalesReportDto monthlysales) => _bplManager.GetMonthlySalesReport(monthlysales);
        #endregion

    }
}
