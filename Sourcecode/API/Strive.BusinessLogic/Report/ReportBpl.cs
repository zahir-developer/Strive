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
    }
}
