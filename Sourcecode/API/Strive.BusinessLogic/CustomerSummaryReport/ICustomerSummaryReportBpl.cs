using Strive.BusinessEntities.DTO.CustomerMonthlyDetailedReport;
using Strive.BusinessEntities.DTO.CustomerSummaryReport;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.CustomerSummaryReport
{
    public interface ICustomerSummaryReportBpl
    {
        Result GetCustomerSummaryReport(CustomerSummaryReportDto customersummary);

        Result GetCustomerMonthlyDetailedReport(CustomerMonthlyDetailedReport customerMonthlyDetail);
    }
}
