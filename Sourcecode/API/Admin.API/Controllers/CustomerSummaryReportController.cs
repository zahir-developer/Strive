using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.CustomerSummaryReport;
using Strive.BusinessLogic.CustomerSummaryReport;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]

    [Route("Admin/[Controller]")]
    public class CustomerSummaryReportController : StriveControllerBase<ICustomerSummaryReportBpl>
    {
        public CustomerSummaryReportController(ICustomerSummaryReportBpl msBpl) : base(msBpl) { }
        #region
        /// <summary>
        /// Method to Get Customer Summary Report.
        /// </summary>
        [HttpPost]
        [Route("GetCustomerSummaryReport")]
        public Result GetCustomerSummaryReport([FromBody] CustomerSummaryReportDto customersummary) => _bplManager.GetCustomerSummaryReport(customersummary);
        #endregion
    }
}
