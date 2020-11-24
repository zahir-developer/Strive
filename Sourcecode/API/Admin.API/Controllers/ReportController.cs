using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CustomerMonthlyDetailedReport;
using Strive.BusinessEntities.DTO.Report;
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
    public class ReportController : StriveControllerBase<IReportBpl>
    {
        public ReportController(IReportBpl msBpl) : base(msBpl) { }
        #region
        /// <summary>
        /// Method to Get MonthlySales Report.
        /// </summary>
        [HttpPost]
        [Route("GetMonthlySalesReport")]
        public Result GetMonthlySalesReport([FromBody] SalesReportDto monthlysales) => _bplManager.GetMonthlySalesReport(monthlysales);

        /// <summary>
        /// Method to Get Customer Summary Report.
        /// </summary>
        [HttpPost]
        [Route("GetCustomerSummaryReport")]
        public Result GetCustomerSummaryReport([FromBody] CustomerSummaryReportDto customersummary) => _bplManager.GetCustomerSummaryReport(customersummary);
        #endregion

        [HttpPost]
        [Route("GetCustomerMonthlyDetailedReport")]
        public Result GetCustomerMonthlyDetailedReport([FromBody] CustomerMonthlyDetailedReport customerMonthlyDetail) => _bplManager.GetCustomerMonthlyDetailedReport(customerMonthlyDetail);

        [HttpPost]
        [Route("MonthlyDailyTipReport")]
        public Result GetEmployeeTipReport([FromBody] EmployeeTipReportDto EmployeeTipReport) => _bplManager.GetEmployeeTipReport(EmployeeTipReport);
        [HttpPost]
        [Route("DailyStatusReport")]
        public Result uspGetDailyStatusReport([FromBody] DailyStatusReportDto DailyStatusReport) => _bplManager.GetDailyStatusReport(DailyStatusReport);
        [HttpPost]
        [Route("DailyStatusDetailInfo")]
        public Result uspGetDailyStatusDetailInfo([FromBody] DailyStatusReportDto DailyStatusReport) => _bplManager.GetDailyStatusDetailInfo(DailyStatusReport);
        [HttpPost]
        [Route("DailyClockDetail")]
        public Result uspGetDailyClockDetail([FromBody] DailyStatusReportDto DailyClockDetail) => _bplManager.GetDailyClockDetail(DailyClockDetail);
        /// <summary>
        /// Method to Get MonthlyMoneyOwned Report.
        /// </summary>
        [HttpGet]
        [Route("GetMonthlyMoneyOwnedReport/{date}")]
        public Result GetMonthlyMoneyOwnedReport(string date) => _bplManager.GetMonthlyMoneyOwnedReport(date);


        /// <summary>
        /// Method to Get EOD Sales Report.
        /// </summary>
        [HttpPost]
        [Route("EODSalesReport")]
        public Result GetEODSalesReport([FromBody] SalesReportDto salesReportDto) => _bplManager.GetEODSalesReport(salesReportDto);

        [HttpPost]
        [Route("DailySalesReport")]
        public Result uspGetDailySalesReport([FromBody] DailySalesReportDto DailySalesReport) => _bplManager.GetDailySalesReport(DailySalesReport);
    }
}
