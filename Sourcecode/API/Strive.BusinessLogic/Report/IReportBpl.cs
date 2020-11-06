using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CustomerMonthlyDetailedReport;
using Strive.BusinessEntities.DTO.Report;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.MonthlySalesReport
{
    public interface IReportBpl
    {
        Result GetMonthlySalesReport(SalesReportDto monthlysales);
        Result GetCustomerSummaryReport(CustomerSummaryReportDto customersummary);
        Result GetCustomerMonthlyDetailedReport(CustomerMonthlyDetailedReport customerMonthlyDetail);
    }
}
