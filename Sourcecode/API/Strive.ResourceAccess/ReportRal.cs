using Strive.BusinessEntities;
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

namespace Strive.ResourceAccess
{
    public class ReportRal : RalBase
    {
        public ReportRal(ITenantHelper tenant) : base(tenant) { }
        public SalesReportDetailViewModel GetMonthlySalesReport(SalesReportDto monthlysales)
        {
            _prm.Add("@LocationId", monthlysales.LocationId);
            _prm.Add("@FromDate", monthlysales.FromDate);
            _prm.Add("@EndDate", monthlysales.EndDate);
            var result = db.FetchMultiResult<SalesReportDetailViewModel>(EnumSP.SalesReport.USPGETMONTHLYSALESREPORT.ToString(), _prm);
            return result;
        }
        public List<CustomerSummaryViewModel> GetCustomerSummaryReport(CustomerSummaryReportDto customersummary)
        {
            _prm.Add("@LocationId", customersummary.LocationId);
            _prm.Add("@Date", customersummary.Date);
            var result = db.Fetch<CustomerSummaryViewModel>(EnumSP.SalesReport.USPGETCUSTOMERSUMMARYREPORT.ToString(), _prm);
            return result;
        }
        public List<CustomerMonthlyDetailedViewModel> GetCustomerMonthlyDetailReport(CustomerMonthlyDetailedReport customerMonthlyDetail)
        {
            _prm.Add("@LocationId", customerMonthlyDetail.LocationId);
            _prm.Add("@Year", customerMonthlyDetail.Year);
            _prm.Add("@Month", customerMonthlyDetail.Month);
            var result = db.Fetch<CustomerMonthlyDetailedViewModel>(EnumSP.SalesReport.USPMONTHLYCUSTOMERDETAIL.ToString(), _prm);
            return result;
        }

        public List<EmployeeTipViewModel> GetEmployeeTipReport(EmployeeTipReportDto EmployeeTipReport)
        {
            List<EmployeeTipViewModel> result = new List<EmployeeTipViewModel>();
            if (EmployeeTipReport.Date == null)
            {
                _prm.Add("@LocationId", EmployeeTipReport.LocationId);
                _prm.Add("@Year", EmployeeTipReport.year);
                _prm.Add("@Month", EmployeeTipReport.month);
                result = db.Fetch<EmployeeTipViewModel>(EnumSP.SalesReport.uspGetMonthlyTipDetail.ToString(), _prm);
            }
            else
            {
                _prm.Add("@locationId", EmployeeTipReport.LocationId);
                _prm.Add("@Date", EmployeeTipReport.Date);
                result = db.Fetch<EmployeeTipViewModel>(EnumSP.SalesReport.uspGetDailyTipDetail.ToString(), _prm);
            }
            return result;
        }

        public List<DailyStatusReportViewModel> GetDailyStatusReport(DailyStatusReportDto DailyStatusReport)
        {


            _prm.Add("@LocationId", DailyStatusReport.LocationId);
            _prm.Add("@Date", DailyStatusReport.Date);
            //_prm.Add("@ClientId", DailyStatusReport.ClientId);
            var result = db.Fetch<DailyStatusReportViewModel>(EnumSP.SalesReport.uspGetDailyStatusReport.ToString(), _prm);

            return result;
        }
        public DailyStatusViewModel GetDailyStatusInfo(DailyStatusReportDto DailyStatusReport)
        {
            _prm.Add("@LocationId", DailyStatusReport.LocationId);
            _prm.Add("@Date", DailyStatusReport.Date);
            var result = db.FetchMultiResult<DailyStatusViewModel>(EnumSP.SalesReport.uspGetDailyStatusInfo.ToString(), _prm);

            return result;
        }
        public List<DailyClockDetailViewModel> GetDailyClockDetail(DailyStatusReportDto DailyStatusReport)
        {
            _prm.Add("@locationId", DailyStatusReport.LocationId);
            _prm.Add("@Date", DailyStatusReport.Date);
            var result = db.Fetch<DailyClockDetailViewModel>(EnumSP.SalesReport.uspGetDailyClockDetail.ToString(), _prm);

            return result;
        }
        public List<MonthlyMoneyOwnedReportViewModel> GetMonthlyMoneyOwnedReport(MonthlyMoneyOwnedDto MonthlyMoneyOwned)
        {
            _prm.Add("@Date", MonthlyMoneyOwned.Date);
            _prm.Add("@LocationId", MonthlyMoneyOwned.LocationId);
            var result = db.Fetch<MonthlyMoneyOwnedReportViewModel>(EnumSP.SalesReport.USPGETMONTHLYMONEYOWNEDREPORT.ToString(), _prm);
            return result;
        }

        public EODSalesReportViewModel GetEODSalesReport(SalesReportDto salesReportDto)
        {
            _prm.Add("@LocationId", salesReportDto.LocationId);
            _prm.Add("@FromDate", salesReportDto.FromDate);
            _prm.Add("@EndDate", salesReportDto.EndDate);
            return db.FetchMultiResult<EODSalesReportViewModel>(EnumSP.SalesReport.USPGETEODSALESREPORT.ToString(), _prm);
        }

        public List<DailySalesReportViewModel> GetDailySalesReport(DailySalesReportDto DailySalesReport)
        {
            _prm.Add("@LocationId", DailySalesReport.LocationId);
            _prm.Add("@Date", DailySalesReport.Date);
            var result = db.Fetch<DailySalesReportViewModel>(EnumSP.SalesReport.USPGETDAILYSALESREPORT.ToString(), _prm);
            return result;
        }

        public List<WashHoursViewModel> GetHourlyWashReport(SalesReportDto salesReportDto)
        {
            _prm.Add("@LocationId", salesReportDto.LocationId);
            _prm.Add("@FromDate", salesReportDto.FromDate);
            _prm.Add("@EndDate", salesReportDto.EndDate);
            var result = db.Fetch<WashHoursViewModel>(EnumSP.SalesReport.USPGETHOURLYWASHREPORT.ToString(), _prm);
            return result;
        }

        public HourlyWashSalesViewModel GetHourWashSalesReport(SalesReportDto salesReportDto)
        {
            _prm.Add("@LocationId", salesReportDto.LocationId);
            _prm.Add("@FromDate", salesReportDto.FromDate);
            _prm.Add("@EndDate", salesReportDto.EndDate);
            var result = db.FetchMultiResult<HourlyWashSalesViewModel>(EnumSP.SalesReport.USPGETHOURLYWASHSALESREPORT.ToString(), _prm);
            return result;
        }
        public IrregularitiesViewModel GetIrregularitiesReport(IrregularitiesDto irregularitiesDto)
        {
            _prm.Add("@LocationId", irregularitiesDto.LocationId);
            _prm.Add("@FromDate", irregularitiesDto.FromDate);
            _prm.Add("@EndDate", irregularitiesDto.EndDate);
            var result = db.FetchMultiResult<IrregularitiesViewModel>(EnumSP.SalesReport.USPGETIRREGULARITIESREPORT.ToString(), _prm);
            return result;
        }
        
    }
}
