using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CustomerMonthlyDetailedReport;
using Strive.BusinessEntities.DTO.Report;
using Strive.BusinessEntities.ViewModel;
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
        Result GetEmployeeTipReport(EmployeeTipReportDto EmployeeTipReport);
        Result GetDailyStatusReport(DailyStatusReportDto DailyStatusReport);
        Result GetDailyStatusInfo(DailyStatusReportDto DailyStatusDailyStatusDetailInfo);
        Result GetDailyClockDetail(DailyStatusReportDto GetDailyClockDetail);
        Result GetMonthlyMoneyOwedReport(MonthlyMoneyOwedDto MonthlyMoneyOwed);
        Result GetMonthlyMoneyOwedReportDetail(MonthlyMoneyOwedDto MonthlyMoneyOwed);
        Result GetEODSalesReport(SalesReportDto salesReportDto);
        EODReportPrintViewModel GetEODSalesExport(EODReportDto eodReportDto);
        HourlyWashSalesReportViewModel GetHourlyWashExport(SalesReportDto salesReportDto);
        DailyStatusReportPrintViewModel GetDailyStatusExport(EODReportDto eodReportDto);

        Result GetDailySalesReport(DailySalesReportDto DailySalesReport);
        Result GetHourlyWashReport(SalesReportDto salesReportDto);
        Result GetIrregularitiesReport(IrregularitiesDto Irregularities);
        IrregularityViewModel GetIrregularitiesExport(IrregularitiesDto eodReportDto);
    }
}
