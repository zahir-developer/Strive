using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CustomerMonthlyDetailedReport;
using Strive.BusinessEntities.DTO.Report;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.MonthlySalesReport
{
    public class ReportBpl : Strivebase, IReportBpl
    {
        public ReportBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetMonthlySalesReport(SalesReportDto monthlysales)
        {
            return ResultWrap(new ReportRal(_tenant).GetMonthlySalesReport, monthlysales, "GetMonthlySalesReport");
        }
        public Result GetCustomerSummaryReport(CustomerSummaryReportDto customersummary)
        {
            return ResultWrap(new ReportRal(_tenant).GetCustomerSummaryReport, customersummary, "GetCustomerSummaryReport");
        }
        public Result GetCustomerMonthlyDetailedReport(CustomerMonthlyDetailedReport customerMonthlyDetail)
        {
            return ResultWrap(new ReportRal(_tenant).GetCustomerMonthlyDetailReport, customerMonthlyDetail, "GetCustomerMonthlyDetailReport");
        }

        public Result GetEmployeeTipReport(EmployeeTipReportDto EmployeeTipReport)
        {
            return ResultWrap(new ReportRal(_tenant).GetEmployeeTipReport, EmployeeTipReport, "GetEmployeeTipReport");
        }

        public Result GetDailyStatusReport(DailyStatusReportDto DailyStatusReport)
        {
            return ResultWrap(new ReportRal(_tenant).GetDailyStatusReport, DailyStatusReport, "GetDailyStatusReport");
        }
        public Result GetDailyStatusDetailInfo(DailyStatusReportDto DailyStatusDetailInfo)
        {
            return ResultWrap(new ReportRal(_tenant).GetDailyStatusDetailInfo, DailyStatusDetailInfo, "GetDailyStatusReport");
        }
        public Result GetDailyClockDetail(DailyStatusReportDto DailyClockDetail)
        {
            return ResultWrap(new ReportRal(_tenant).GetDailyClockDetail, DailyClockDetail, "GetDailyClockDetail");
        }
        public Result GetMonthlyMoneyOwnedReport(string date)
        {
            return ResultWrap(new ReportRal(_tenant).GetMonthlyMoneyOwnedReport, date, "GetMonthlyMoneyOwnedReport");
        }

        public Result GetEODSalesReport(SalesReportDto salesReportDto)
        {
            return ResultWrap(new ReportRal(_tenant).GetEODSalesReport, salesReportDto, "GetEODSalesReport");
        }
    }
}
