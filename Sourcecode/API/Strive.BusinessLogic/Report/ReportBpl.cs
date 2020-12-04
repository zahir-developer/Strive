using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.CashRegister.DTO;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CustomerMonthlyDetailedReport;
using Strive.BusinessEntities.DTO.Report;
using Strive.BusinessEntities.DTO.TimeClock;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
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

       
        public DailyStatusReportPrintViewModel GetDailyStatusExport(EODReportDto eodReportDto)
        {
            try
            {
                DailyStatusReportPrintViewModel dailyStatusReportPrintViewModel = new DailyStatusReportPrintViewModel();
                dailyStatusReportPrintViewModel.EmployeeTimeClock = new TimeClockEmployeeHourViewModel();
                dailyStatusReportPrintViewModel.DailyStatusDetailInfoViews = new List<DailyStatusDetailInfoViewModel>();

                //Employee TimeClock
                TimeClockLocationDto timeClockLocationDto = new TimeClockLocationDto();
                timeClockLocationDto.LocationId = eodReportDto.LocationId;
                timeClockLocationDto.Date =  eodReportDto.Date;
                var timeClockRal = new TimeClockRal(_tenant);
                dailyStatusReportPrintViewModel.EmployeeTimeClock = timeClockRal.TimeClockEmployeeHourDetail(timeClockLocationDto);


                //Detail Info
                DailyStatusReportDto dailyStatusReportDto = new DailyStatusReportDto();
                dailyStatusReportDto.LocationId = eodReportDto.LocationId;
                dailyStatusReportDto.Date = eodReportDto.Date;
                var detailInfoRal = new ReportRal(_tenant);

                dailyStatusReportPrintViewModel.DailyStatusDetailInfoViews = detailInfoRal.GetDailyStatusDetailInfo(dailyStatusReportDto);
                return dailyStatusReportPrintViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EODReportPrintViewModel GetEODSalesExport(EODReportDto eodReportDto)
        {
           try
            {
                EODReportPrintViewModel eodReportPrintViewModel = new EODReportPrintViewModel();
                eodReportPrintViewModel.EmployeeTimeClock = new TimeClockEmployeeHourViewModel();
                eodReportPrintViewModel.Sales = new EODSalesReportViewModel();
                eodReportPrintViewModel.CashRegister = new CashRegisterDetailViewModel();
                eodReportPrintViewModel.DailyStatusReport = new List<DailyStatusReportViewModel>();
                eodReportPrintViewModel.DailyStatusDetailInfoViews = new List<DailyStatusDetailInfoViewModel>();

                //coins,bills,rolls                
                var cashRal = new CashRegisterRal(_tenant);
                eodReportPrintViewModel.CashRegister = cashRal.GetCloseOutRegisterDetails(eodReportDto.CashRegisterType,eodReportDto.LocationId, eodReportDto.Date);
                eodReportPrintViewModel.CashRegister.CashRegisterCoins.Pennies = Convert.ToDecimal(eodReportPrintViewModel.CashRegister.CashRegisterCoins.Pennies) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterCoins.Nickels = Convert.ToDecimal(5 * eodReportPrintViewModel.CashRegister.CashRegisterCoins.Nickels) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterCoins.Dimes = Convert.ToDecimal(10 * eodReportPrintViewModel.CashRegister.CashRegisterCoins.Dimes) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterCoins.Quarters = Convert.ToDecimal(25 * eodReportPrintViewModel.CashRegister.CashRegisterCoins.Quarters) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterCoins.HalfDollars = Convert.ToDecimal(50 * eodReportPrintViewModel.CashRegister.CashRegisterCoins.HalfDollars) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterRolls.Pennies = Convert.ToDecimal(eodReportPrintViewModel.CashRegister.CashRegisterRolls.Pennies) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterRolls.Nickels = Convert.ToDecimal(5 * eodReportPrintViewModel.CashRegister.CashRegisterRolls.Nickels) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterRolls.Dimes = Convert.ToDecimal(10 * eodReportPrintViewModel.CashRegister.CashRegisterRolls.Dimes) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterRolls.Quarters = Convert.ToDecimal(25 * eodReportPrintViewModel.CashRegister.CashRegisterRolls.Quarters) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterRolls.HalfDollars = Convert.ToDecimal(50 * eodReportPrintViewModel.CashRegister.CashRegisterRolls.HalfDollars) / 100;
                eodReportPrintViewModel.CashRegister.CashRegisterBills.s1 = Convert.ToDecimal(eodReportPrintViewModel.CashRegister.CashRegisterBills.s1) ;
                eodReportPrintViewModel.CashRegister.CashRegisterBills.s5 = Convert.ToDecimal(5 * eodReportPrintViewModel.CashRegister.CashRegisterBills.s5) ;
                eodReportPrintViewModel.CashRegister.CashRegisterBills.s10 = Convert.ToDecimal(10 * eodReportPrintViewModel.CashRegister.CashRegisterBills.s10) ;
                eodReportPrintViewModel.CashRegister.CashRegisterBills.s20 = Convert.ToDecimal(20 * eodReportPrintViewModel.CashRegister.CashRegisterBills.s20) ;
                eodReportPrintViewModel.CashRegister.CashRegisterBills.s50 = Convert.ToDecimal(50 * eodReportPrintViewModel.CashRegister.CashRegisterBills.s50) ;
                eodReportPrintViewModel.CashRegister.CashRegisterBills.s100 = Convert.ToDecimal(100 * eodReportPrintViewModel.CashRegister.CashRegisterBills.s100) ;
                
                //Sales
                SalesReportDto salesReportDto = new SalesReportDto();
                salesReportDto.LocationId = eodReportDto.LocationId;
                salesReportDto.FromDate = eodReportDto.Date;
                salesReportDto.EndDate = eodReportDto.Date;

                var salesRal = new ReportRal(_tenant);
                eodReportPrintViewModel.Sales = salesRal.GetEODSalesReport(salesReportDto);

                //Employee TimeClock
                TimeClockLocationDto timeClockLocationDto = new TimeClockLocationDto();
                timeClockLocationDto.LocationId = eodReportDto.LocationId;
                timeClockLocationDto.Date = eodReportDto.Date;
                var timeClockRal = new TimeClockRal(_tenant);
                eodReportPrintViewModel.EmployeeTimeClock = timeClockRal.TimeClockEmployeeHourDetail(timeClockLocationDto);


            
                //Washes and Details
                DailyStatusReportDto dailyStatusReportDto = new DailyStatusReportDto();
                dailyStatusReportDto.LocationId = eodReportDto.LocationId;
                dailyStatusReportDto.Date = eodReportDto.Date;
                var detailRal = new ReportRal(_tenant);
                eodReportPrintViewModel.DailyStatusReport = detailRal.GetDailyStatusReport(dailyStatusReportDto);
                //Detail Info
                DailyStatusReportDto dailyStatusReportInfoDto = new DailyStatusReportDto();
                dailyStatusReportInfoDto.LocationId = eodReportDto.LocationId;
                dailyStatusReportInfoDto.Date = eodReportDto.Date;
                var detailInfoRal = new ReportRal(_tenant);

                eodReportPrintViewModel.DailyStatusDetailInfoViews = detailInfoRal.GetDailyStatusDetailInfo(dailyStatusReportDto);
                return eodReportPrintViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
        public Result GetDailySalesReport(DailySalesReportDto DailySalesReport)
        {
            return ResultWrap(new ReportRal(_tenant).GetDailySalesReport, DailySalesReport, "GetDailySalesReport");
        }

        public Result GetHourlyWashReport(SalesReportDto salesReportDto)
        {
            return ResultWrap(new ReportRal(_tenant).GetHourlyWashReport, salesReportDto, "GetHourlyWashReport");
        }
    }
}
