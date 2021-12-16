using Admin.API.Helpers;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CustomerMonthlyDetailedReport;
using Strive.BusinessEntities.DTO.Report;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.MonthlySalesReport;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        [Route("DailyStatusInfo")]
        public Result uspGetDailyStatusInfo([FromBody] DailyStatusReportDto DailyStatusReport) => _bplManager.GetDailyStatusInfo(DailyStatusReport);
        [HttpPost]
        [Route("DailyClockDetail")]
        public Result uspGetDailyClockDetail([FromBody] DailyStatusReportDto DailyClockDetail) => _bplManager.GetDailyClockDetail(DailyClockDetail);
        /// <summary>
        /// Method to Get MonthlyMoneyOwned Report.
        /// </summary>
        [HttpGet]
        [Route("GetMonthlyMoneyOwedReport")]
        public Result GetMonthlyMoneyOwedReport(MonthlyMoneyOwedDto MonthlyMoneyOwned) => _bplManager.GetMonthlyMoneyOwedReportDetail(MonthlyMoneyOwned);

        /// <summary>
        /// HarlyWashReport
        /// </summary>
        /// <param name="salesReportDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetHourlyWashReport")]
        public Result GetHourlyWashReport([FromBody] SalesReportDto salesReportDto) => _bplManager.GetHourlyWashReport(salesReportDto);


        [HttpPost]
        [Route("HourlyWashExport")]
        public IActionResult GetHourlyWashExport([FromBody] SalesReportDto salesReportDto)
        {

            var hourlyWashResult = _bplManager.GetHourlyWashExport(salesReportDto);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HourlyWashReport");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Day & Date";
                worksheet.Cell(currentRow, 2).Value = "Temp";
                worksheet.Cell(currentRow, 3).Value = "Rain";
                worksheet.Cell(currentRow, 4).Value = "No of washes";
                worksheet.Cell(currentRow, 5).Value = "Score";
                worksheet.Cell(currentRow, 6).Value = "_7 AM";
                worksheet.Cell(currentRow, 7).Value = "_8 AM";
                worksheet.Cell(currentRow, 8).Value = "_9 AM";
                worksheet.Cell(currentRow, 9).Value = "_10 AM";
                worksheet.Cell(currentRow, 10).Value = "_11 AM";
                worksheet.Cell(currentRow, 11).Value = "_12 PM";
                worksheet.Cell(currentRow, 12).Value = "_1 PM";
                worksheet.Cell(currentRow, 13).Value = "_2 PM";
                worksheet.Cell(currentRow, 14).Value = "_3 PM";
                worksheet.Cell(currentRow, 15).Value = "_4 PM";
                worksheet.Cell(currentRow, 16).Value = "_5 PM";
                worksheet.Cell(currentRow, 17).Value = "_6 PM";
                worksheet.Cell(currentRow, 18).Value = "_7 PM";

                if (hourlyWashResult.WashHoursViewModel != null)
                {
                    foreach (var oItem in hourlyWashResult.WashHoursViewModel)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = oItem.JobDate;
                        worksheet.Cell(currentRow, 2).Value = oItem.Temperature;
                        worksheet.Cell(currentRow, 3).Value = oItem.Rain;
                        worksheet.Cell(currentRow, 4).Value = oItem.NoOfWashes;
                        worksheet.Cell(currentRow, 5).Value = oItem.Goal;
                        worksheet.Cell(currentRow, 6).Value = oItem._7AMTotal;
                        worksheet.Cell(currentRow, 7).Value = oItem._8AMTotal;
                        worksheet.Cell(currentRow, 8).Value = oItem._9AMTotal;
                        worksheet.Cell(currentRow, 9).Value = oItem._10AMTotal;
                        worksheet.Cell(currentRow, 10).Value = oItem._11AMTotal;
                        worksheet.Cell(currentRow, 11).Value = oItem._12AMTotal;
                        worksheet.Cell(currentRow, 12).Value = oItem._1PMTotal;
                        worksheet.Cell(currentRow, 13).Value = oItem._2PMTotal;
                        worksheet.Cell(currentRow, 14).Value = oItem._3PMTotal;
                        worksheet.Cell(currentRow, 15).Value = oItem._4PMTotal;
                        worksheet.Cell(currentRow, 16).Value = oItem._5PMTotal;
                        worksheet.Cell(currentRow, 17).Value = oItem._6PMTotal;
                        worksheet.Cell(currentRow, 18).Value = oItem._7PMTotal;
                        currentRow++;
                        worksheet.Cell(currentRow, 6).Value = oItem._7AM;
                        worksheet.Cell(currentRow, 7).Value = oItem._8AM;
                        worksheet.Cell(currentRow, 8).Value = oItem._9AM;
                        worksheet.Cell(currentRow, 9).Value = oItem._10AM;
                        worksheet.Cell(currentRow, 10).Value = oItem._11AM;
                        worksheet.Cell(currentRow, 11).Value = oItem._12AM;
                        worksheet.Cell(currentRow, 12).Value = oItem._1PM;
                        worksheet.Cell(currentRow, 13).Value = oItem._2PM;
                        worksheet.Cell(currentRow, 14).Value = oItem._3PM;
                        worksheet.Cell(currentRow, 15).Value = oItem._4PM;
                        worksheet.Cell(currentRow, 16).Value = oItem._5PM;
                        worksheet.Cell(currentRow, 17).Value = oItem._6PM;
                        worksheet.Cell(currentRow, 18).Value = oItem._7PM;
                    }
                }

                var worksheet2 = workbook.Worksheets.Add("HourlySalesDetail");
                currentRow = 1;
                int currentColumn = 11;

                worksheet2.Cell(currentRow, 1).Value = "JobDate";
                worksheet2.Cell(currentRow, 2).Value = "Day";
                worksheet2.Cell(currentRow, 3).Value = "Account";
                worksheet2.Cell(currentRow, 4).Value = "Actual";
                worksheet2.Cell(currentRow, 5).Value = "BC";
                worksheet2.Cell(currentRow, 6).Value = "Deposits";
                worksheet2.Cell(currentRow, 7).Value = "Difference";
                worksheet2.Cell(currentRow, 8).Value = "GiftCard";
                worksheet2.Cell(currentRow, 9).Value = "Managers";
                worksheet2.Cell(currentRow, 10).Value = "Sales";
                foreach (var oItem in hourlyWashResult.LocationWashServiceViewModel)
                {
                    currentColumn++;
                    worksheet2.Cell(currentRow, currentColumn).Value = oItem.WashCount;
                }
                if (hourlyWashResult.SalesSummaryViewModel != null)
                {
                    foreach (var oItem in hourlyWashResult.SalesSummaryViewModel)
                    {
                        currentRow++;

                        worksheet2.Cell(currentRow, 1).Value = oItem.JobDate;
                        worksheet2.Cell(currentRow, 2).Value = oItem.JobDate.ToString("ddd");
                        worksheet2.Cell(currentRow, 3).Value = oItem.Account;
                        worksheet2.Cell(currentRow, 4).Value = oItem.Total + oItem.Tips;
                        worksheet2.Cell(currentRow, 5).Value = oItem.Credit;
                        worksheet2.Cell(currentRow, 6).Value = oItem.Cash;
                        worksheet2.Cell(currentRow, 7).Value = oItem.Tips;
                        worksheet2.Cell(currentRow, 8).Value = oItem.GiftCard;
                        worksheet2.Cell(currentRow, 10).Value = oItem.Total;
                        currentColumn = 11;
                        foreach (var oWash in hourlyWashResult.LocationWashServiceViewModel)
                        {
                            if (oWash.JobDate == oItem.JobDate)
                            {
                                currentColumn++;
                                worksheet2.Cell(currentRow, currentColumn).Value = oWash.WashCount;
                            }
                        }
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "HourlyWashReport.xlsx");
                }
            }
        }
        /// <summary>
        /// Method to Get EOD Sales Report.
        /// </summary>
        [HttpPost]
        [Route("EODSalesReport")]
        public Result GetEODSalesReport([FromBody] SalesReportDto salesReportDto) => _bplManager.GetEODSalesReport(salesReportDto);

        [HttpPost]
        [Route("EODSalesExport")]
        public IActionResult GetEODSalesExport([FromBody] EODReportDto reportDto)
        {
            //EODReportDto reportDto = new EODReportDto();
            //reportDto.Date = Convert.ToDateTime("2020-11-23");
            //reportDto.LocationId = 2033;
            //reportDto.cashRegisterType = "CLOSEOUT";
            var eodResult = _bplManager.GetEODSalesExport(reportDto);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employee Time Clock");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "First Name";
                worksheet.Cell(currentRow, 2).Value = "Last Name";
                worksheet.Cell(currentRow, 3).Value = "Wash Hours";
                worksheet.Cell(currentRow, 4).Value = "Detail Hours";
                worksheet.Cell(currentRow, 5).Value = "Others";
                if (eodResult.EmployeeTimeClock.TimeClockDetails != null)
                {
                    foreach (var employeeTimeClock in eodResult.EmployeeTimeClock.TimeClockEmployeeDetails)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = employeeTimeClock.FirstName;
                        worksheet.Cell(currentRow, 2).Value = employeeTimeClock.LastName;
                        worksheet.Cell(currentRow, 3).Value = employeeTimeClock.WashHours;
                        worksheet.Cell(currentRow, 4).Value = employeeTimeClock.DetailHours;
                        worksheet.Cell(currentRow, 5).Value = employeeTimeClock.OtherHours;
                    }
                }
                var worksheet2 = workbook.Worksheets.Add("CloseOutRegister");
                currentRow = 1;


                /*
                int cell = 1;
                foreach (var prop in eodResult.CashRegister.CashRegisterBills.GetType().GetProperties())
                    if (!prop.Name.Contains("Id") && !prop.Name.Contains("IsDeleted") && !prop.Name.Contains("IsActive"))
                        worksheet2.Cell(cell, 1).Value = prop.Name;
                    cell++;
                }*/


                worksheet2.Cell(1, 1).Value = "COINS";
                worksheet2.Cell(2, 1).Value = "Pennies";
                worksheet2.Cell(3, 1).Value = "Nickels";
                worksheet2.Cell(4, 1).Value = "Dimes";
                worksheet2.Cell(5, 1).Value = "Quarters";
                worksheet2.Cell(6, 1).Value = "HalfDollars";

                worksheet2.Cell(7, 1).Value = "ROLLS";
                worksheet2.Cell(8, 1).Value = "Pennies";
                worksheet2.Cell(9, 1).Value = "Nickels";
                worksheet2.Cell(10, 1).Value = "Dimes";
                worksheet2.Cell(11, 1).Value = "Quarters";

                worksheet2.Cell(12, 1).Value = "BILLS";
                worksheet2.Cell(13, 1).Value = "1's";
                worksheet2.Cell(14, 1).Value = "5's";
                worksheet2.Cell(15, 1).Value = "10's";
                worksheet2.Cell(16, 1).Value = "20's";
                worksheet2.Cell(17, 1).Value = "50's";
                worksheet2.Cell(18, 1).Value = "100's";


                if (eodResult.CashRegister.CashRegisterCoins != null)
                {
                    var coins = eodResult.CashRegister.CashRegisterCoins;
                    {
                        worksheet2.Cell(1, 2).Value = "";
                        worksheet2.Cell(2, 2).Value = coins.Pennies.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(3, 2).Value = coins.Nickels.ToString("C", CultureInfo.GetCultureInfo("en-US")); ;
                        worksheet2.Cell(4, 2).Value = coins.Dimes.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(5, 2).Value = coins.Quarters.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(6, 2).Value = coins.HalfDollars.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    }
                }
                if (eodResult.CashRegister.CashRegisterRolls != null)
                {
                    var rolls = eodResult.CashRegister.CashRegisterRolls;
                    {
                        worksheet2.Cell(7, 2).Value = "";
                        worksheet2.Cell(8, 2).Value = rolls.Pennies.ToString("C", CultureInfo.GetCultureInfo("en-US")); ;
                        worksheet2.Cell(9, 2).Value = rolls.Nickels.ToString("C", CultureInfo.GetCultureInfo("en-US")); ;
                        worksheet2.Cell(10, 2).Value = rolls.Dimes.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(11, 2).Value = rolls.Quarters.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    }
                }
                if (eodResult.CashRegister.CashRegisterBills != null)
                {
                    var bills = eodResult.CashRegister.CashRegisterBills;
                    {
                        worksheet2.Cell(12, 2).Value = "";
                        worksheet2.Cell(13, 2).Value = bills.s1.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(14, 2).Value = bills.s5.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(15, 2).Value = bills.s10.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(16, 2).Value = bills.s20.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(17, 2).Value = bills.s50.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                        worksheet2.Cell(18, 2).Value = bills.s100.ToString("C", CultureInfo.GetCultureInfo("en-US"));

                    }
                }


                var worksheet3 = workbook.Worksheets.Add("Daily Status");
                currentRow = 1;
                worksheet3.Cell(currentRow, 1).Value = "Number";
                worksheet3.Cell(currentRow, 2).Value = "Service Name";
                worksheet3.Cell(currentRow, 3).Value = "Job Type";
                worksheet3.Cell(currentRow, 4).Value = "Job Date";

                foreach (var details in eodResult.DailyStatusReport)
                {
                    currentRow++;
                    worksheet3.Cell(currentRow, 1).Value = details.Number;
                    worksheet3.Cell(currentRow, 2).Value = details.ServiceName;
                    worksheet3.Cell(currentRow, 3).Value = details.JobType;
                    worksheet3.Cell(currentRow, 4).Value = details.JobDate;

                }

                var worksheet4 = workbook.Worksheets.Add("Detail Info");
                currentRow = 1;
                worksheet4.Cell(currentRow, 1).Value = "TicketNumber";
                worksheet4.Cell(currentRow, 2).Value = "EmployeeName";
                worksheet4.Cell(currentRow, 3).Value = "Commision";
                if (eodResult.DailyStatusDetailInfoViews.DailyStatusDetailInfo != null)
                {
                    foreach (var detailsInfo in eodResult.DailyStatusDetailInfoViews.DailyStatusDetailInfo)
                    {
                        currentRow++;
                        worksheet4.Cell(currentRow, 1).Value = detailsInfo.TicketNumber;
                        worksheet4.Cell(currentRow, 2).Value = detailsInfo.EmployeeName;
                        worksheet4.Cell(currentRow, 3).Value = detailsInfo.Commission;

                    }
                }
                var worksheet5 = workbook.Worksheets.Add("Sales");
                currentRow = 1;
                worksheet5.Cell(1, 1).Value = "SALES";
                worksheet5.Cell(2, 1).Value = "IN";
                worksheet5.Cell(3, 1).Value = "OUT";
                worksheet5.Cell(4, 1).Value = "Difference";
                worksheet5.Cell(5, 1).Value = "BC Credit Cards";
                worksheet5.Cell(6, 1).Value = "Total Expenses";
                worksheet5.Cell(7, 1).Value = "Account";
                worksheet5.Cell(8, 1).Value = "Gift Cards";
                worksheet5.Cell(9, 1).Value = "Grand Total";
                worksheet5.Cell(10, 1).Value = "Total";
                worksheet5.Cell(11, 1).Value = "Tax";
                worksheet5.Cell(12, 1).Value = "Grand Total";
                worksheet5.Cell(13, 1).Value = "Cash";
                worksheet5.Cell(14, 1).Value = "Check";
                worksheet5.Cell(15, 1).Value = "Charge Cards";
                worksheet5.Cell(16, 1).Value = "Account";
                worksheet5.Cell(17, 1).Value = "Gift Card";
                worksheet5.Cell(18, 1).Value = "Total Paid";
                worksheet5.Cell(19, 1).Value = "Cash back";
                
                var inTotal = eodResult.CashInRegister.CashRegisterCoins.Pennies + eodResult.CashInRegister.CashRegisterCoins.Nickels +
                                                eodResult.CashInRegister.CashRegisterCoins.Dimes + eodResult.CashInRegister.CashRegisterCoins.Quarters +
                                                eodResult.CashInRegister.CashRegisterCoins.HalfDollars + eodResult.CashInRegister.CashRegisterRolls.Pennies +
                                                eodResult.CashInRegister.CashRegisterRolls.Nickels + eodResult.CashInRegister.CashRegisterRolls.Dimes +
                                                eodResult.CashInRegister.CashRegisterRolls.Quarters + eodResult.CashInRegister.CashRegisterRolls.HalfDollars +
                                                eodResult.CashInRegister.CashRegisterBills.s1 + eodResult.CashInRegister.CashRegisterBills.s5 +
                                                eodResult.CashInRegister.CashRegisterBills.s10 + eodResult.CashInRegister.CashRegisterBills.s20 +
                                                eodResult.CashInRegister.CashRegisterBills.s50 + eodResult.CashInRegister.CashRegisterBills.s100;
                var outTotal = eodResult.CashRegister.CashRegisterCoins.Pennies + eodResult.CashRegister.CashRegisterCoins.Nickels +
                                                eodResult.CashRegister.CashRegisterCoins.Dimes + eodResult.CashRegister.CashRegisterCoins.Quarters +
                                                eodResult.CashRegister.CashRegisterCoins.HalfDollars + eodResult.CashRegister.CashRegisterRolls.Pennies +
                                                eodResult.CashRegister.CashRegisterRolls.Nickels + eodResult.CashRegister.CashRegisterRolls.Dimes +
                                                eodResult.CashRegister.CashRegisterRolls.Quarters + eodResult.CashRegister.CashRegisterRolls.HalfDollars + eodResult.CashRegister.CashRegisterBills.s1 + eodResult.CashRegister.CashRegisterBills.s5 + eodResult.CashRegister.CashRegisterBills.s10 + eodResult.CashRegister.CashRegisterBills.s20 + eodResult.CashRegister.CashRegisterBills.s50 + eodResult.CashRegister.CashRegisterBills.s100
                                                + eodResult.CashRegister.CashRegister.Tips;

                var sales = eodResult.Sales.EODSalesDetails;
                {

                    worksheet5.Cell(1, 2).Value = sales.Total.GetValueOrDefault() + sales.TaxAmount.GetValueOrDefault();
                    worksheet5.Cell(2, 2).Value = inTotal;
                    worksheet5.Cell(3, 2).Value = outTotal;
                    worksheet5.Cell(4, 2).Value = inTotal - outTotal;
                    worksheet5.Cell(5, 2).Value = sales.Credit.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(6, 2).Value = sales.TotalPaid.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(7, 2).Value = sales.Account.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(8, 2).Value = sales.GiftCard.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(9, 2).Value = sales.TotalPaid.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(10, 2).Value = sales.Total.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(11, 2).Value = sales.TaxAmount.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(12, 2).Value = sales.GrandTotal.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(13, 2).Value = sales.Cash.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(14, 2).Value = sales.Credit.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(15, 2).Value = "0.00";
                    worksheet5.Cell(16, 2).Value = sales.Account.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(17, 2).Value = sales.GiftCard.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(18, 2).Value = sales.TotalPaid.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(19, 2).Value = sales.CashBack.GetValueOrDefault().ToString("C", CultureInfo.GetCultureInfo("en-US"));
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "EODReport.xlsx");
                }
            }
        }

        [HttpPost]
        [Route("DailyStatusExport")]
        public IActionResult GetDailyStatusExport([FromBody] EODReportDto reportDto)
        {
            //EODReportDto reportDto = new EODReportDto();
            //reportDto.Date = Convert.ToDateTime("2020-11-23");
            //reportDto.LocationId = 2033;
            var statusResult = _bplManager.GetDailyStatusExport(reportDto);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employee Time Clock");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "First Name";
                worksheet.Cell(currentRow, 2).Value = "Last Name";
                worksheet.Cell(currentRow, 3).Value = "Wash Hours";
                worksheet.Cell(currentRow, 4).Value = "Detail Hours";
                worksheet.Cell(currentRow, 5).Value = "Others";
                if (statusResult.EmployeeTimeClock.TimeClockEmployeeDetails != null)
                {
                    foreach (var employeeTimeClock in statusResult.EmployeeTimeClock.TimeClockEmployeeDetails)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = employeeTimeClock.FirstName;
                        worksheet.Cell(currentRow, 2).Value = employeeTimeClock.LastName;
                        worksheet.Cell(currentRow, 3).Value = employeeTimeClock.WashHours;
                        worksheet.Cell(currentRow, 4).Value = employeeTimeClock.DetailHours;
                        worksheet.Cell(currentRow, 5).Value = employeeTimeClock.OtherHours;
                    }
                }

                var worksheet2 = workbook.Worksheets.Add("Employee Time Clock Details");
                currentRow = 1;
                worksheet2.Cell(currentRow, 1).Value = "TimeClock Id";
                worksheet2.Cell(currentRow, 2).Value = "Employee Id ";
                worksheet2.Cell(currentRow, 3).Value = "Location Id ";
                worksheet2.Cell(currentRow, 4).Value = "Role Id";
                worksheet2.Cell(currentRow, 5).Value = "Location Name";
                worksheet2.Cell(currentRow, 6).Value = "Role Name";
                worksheet2.Cell(currentRow, 7).Value = "Day";
                worksheet2.Cell(currentRow, 8).Value = "EventDate";
                worksheet2.Cell(currentRow, 9).Value = "InTime";
                worksheet2.Cell(currentRow, 10).Value = "OutTime";
                worksheet2.Cell(currentRow, 11).Value = "Total Hours";
                //worksheet2.Cell(currentRow, 11).Value = "EventType";
                worksheet2.Cell(currentRow, 12).Value = "Status";
                if (statusResult.EmployeeTimeClock.TimeClockEmployeeDetails != null)
                {
                    foreach (var employeeTimeClock in statusResult.EmployeeTimeClock.TimeClockDetails)
                    {
                        currentRow++;
                        worksheet2.Cell(currentRow, 1).Value = employeeTimeClock.TimeClockId;
                        worksheet2.Cell(currentRow, 2).Value = employeeTimeClock.EmployeeId;
                        worksheet2.Cell(currentRow, 3).Value = employeeTimeClock.LocationId;
                        worksheet2.Cell(currentRow, 4).Value = employeeTimeClock.RoleId;
                        worksheet2.Cell(currentRow, 5).Value = employeeTimeClock.LocationName;
                        worksheet2.Cell(currentRow, 6).Value = employeeTimeClock.RoleName;
                        worksheet2.Cell(currentRow, 7).Value = employeeTimeClock.Day;
                        worksheet2.Cell(currentRow, 8).Value = employeeTimeClock.EventDate;
                        worksheet2.Cell(currentRow, 9).Value = employeeTimeClock.InTime;
                        worksheet2.Cell(currentRow, 10).Value = employeeTimeClock.OutTime;
                        worksheet2.Cell(currentRow, 11).Value = (employeeTimeClock.OutTime - employeeTimeClock.InTime);// employeeTimeClock.TotalHours;
                        //worksheet2.Cell(currentRow, 11).Value = employeeTimeClock.EventType;
                        worksheet2.Cell(currentRow, 12).Value = employeeTimeClock.Status;
                    }
                }
                var worksheet4 = workbook.Worksheets.Add("Daily Status Info");
                currentRow = 1;
                worksheet4.Cell(currentRow, 1).Value = "TicketNumber";
                worksheet4.Cell(currentRow, 2).Value = "FirstName";
                worksheet4.Cell(currentRow, 3).Value = "Commision";
                if (statusResult.DailyStatusDetailInfoViews.DailyStatusDetailInfo != null)
                {

                    foreach (var detailsInfo in statusResult.DailyStatusDetailInfoViews.DailyStatusDetailInfo)
                    {
                        currentRow++;
                        worksheet4.Cell(currentRow, 1).Value = detailsInfo.TicketNumber;
                        worksheet4.Cell(currentRow, 2).Value = detailsInfo.EmployeeName;
                        worksheet4.Cell(currentRow, 3).Value = detailsInfo.Commission;
                    }
                }

                var worksheet3 = workbook.Worksheets.Add("Wash Hours");
                worksheet3.Cell(1, 1).Value = "Wash Employee Count";
                worksheet3.Cell(2, 1).Value = "Wash Expense";
                if (statusResult.DailyStatusDetailInfoViews.DailyStatusWashInfo != null)
                {

                    foreach (var WashInfo in statusResult.DailyStatusDetailInfoViews.DailyStatusWashInfo)
                    {
                        currentRow++;
                        worksheet3.Cell(currentRow, 1).Value = WashInfo.WashEmployeeCount;
                        worksheet3.Cell(currentRow, 2).Value = WashInfo.WashExpense;
                    }
                }

                var worksheet5 = workbook.Worksheets.Add("Daily Status");
                currentRow = 1;
                worksheet5.Cell(currentRow, 1).Value = "Number";
                worksheet5.Cell(currentRow, 2).Value = "Service Name";
                worksheet5.Cell(currentRow, 3).Value = "Job Type";
                worksheet5.Cell(currentRow, 4).Value = "Job Date";

                foreach (var details in statusResult.DailyStatusReport)
                {
                    currentRow++;
                    worksheet5.Cell(currentRow, 1).Value = details.Number;
                    worksheet5.Cell(currentRow, 2).Value = details.ServiceName;
                    worksheet5.Cell(currentRow, 3).Value = details.JobType;
                    worksheet5.Cell(currentRow, 4).Value = details.JobDate;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "StatusReport.xlsx");
                }
            }
        }
        [HttpPost]
        [Route("DailySalesReport")]
        public Result uspGetDailySalesReport([FromBody] DailySalesReportDto DailySalesReport) => _bplManager.GetDailySalesReport(DailySalesReport);

        [HttpGet]
        [Route("GetIrregularitiesReport")]
        public Result GetIrregularitiesReport(IrregularitiesDto Irregularities) => _bplManager.GetIrregularitiesReport(Irregularities);

        [HttpPost]
        [Route("IrregularitiesExport")]
        public IActionResult GetIrregularitiesExport([FromBody] IrregularitiesDto reportDto)
        {

            var rptResult = _bplManager.GetIrregularitiesExport(reportDto);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Vehicles with no info");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Manager";
                worksheet.Cell(currentRow, 2).Value = "Date";
                worksheet.Cell(currentRow, 3).Value = "Barcode";
                worksheet.Cell(currentRow, 4).Value = "Time";
                worksheet.Cell(currentRow, 5).Value = "Tkt #";
                worksheet.Cell(currentRow, 6).Value = "Color";
                worksheet.Cell(currentRow, 7).Value = "Make";
                worksheet.Cell(currentRow, 8).Value = "Model";
                worksheet.Cell(currentRow, 9).Value = "Extra serv";
                worksheet.Cell(currentRow, 10).Value = "Sale amt";
                worksheet.Cell(currentRow, 11).Value = "Discount";
                worksheet.Cell(currentRow, 12).Value = "paid by";

                if (rptResult.VehiclesInfo != null)
                {
                    foreach (var VehiclesInfo in rptResult.VehiclesInfo)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = VehiclesInfo.Manager;
                        worksheet.Cell(currentRow, 2).Value = VehiclesInfo.JobDate;
                        worksheet.Cell(currentRow, 3).Value = VehiclesInfo.Barcode;
                        worksheet.Cell(currentRow, 4).Value = VehiclesInfo.TimeIn;
                        worksheet.Cell(currentRow, 5).Value = VehiclesInfo.TicketNumber;
                        worksheet.Cell(currentRow, 6).Value = VehiclesInfo.Color;
                        worksheet.Cell(currentRow, 7).Value = VehiclesInfo.Make;
                        worksheet.Cell(currentRow, 8).Value = VehiclesInfo.Model;
                        worksheet.Cell(currentRow, 9).Value = VehiclesInfo.ExtraServices;
                        worksheet.Cell(currentRow, 10).Value = VehiclesInfo.SaleAmt;
                        worksheet.Cell(currentRow, 11).Value = VehiclesInfo.Discount;
                        worksheet.Cell(currentRow, 12).Value = VehiclesInfo.PaidBy;
                    }
                }

                var worksheet2 = workbook.Worksheets.Add("Missing ticket numbers");
                currentRow = 1;
                worksheet2.Cell(currentRow, 1).Value = "Manager";
                worksheet2.Cell(currentRow, 2).Value = "Date";
                worksheet2.Cell(currentRow, 3).Value = "Barcode";
                worksheet2.Cell(currentRow, 4).Value = "Time";
                worksheet2.Cell(currentRow, 5).Value = "Tkt #";
                worksheet2.Cell(currentRow, 6).Value = "Color";
                worksheet2.Cell(currentRow, 7).Value = "Make";
                worksheet2.Cell(currentRow, 8).Value = "Model";
                worksheet2.Cell(currentRow, 9).Value = "Extra serv";
                worksheet2.Cell(currentRow, 10).Value = "Sale amt";
                worksheet2.Cell(currentRow, 11).Value = "Discount";
                worksheet2.Cell(currentRow, 12).Value = "paid by";

                if (rptResult.MissingTicket != null)
                {
                    foreach (var MissingTicket in rptResult.MissingTicket)
                    {
                        currentRow++;
                        worksheet2.Cell(currentRow, 1).Value = MissingTicket.Manager;
                        worksheet2.Cell(currentRow, 2).Value = MissingTicket.JobDate;
                        worksheet2.Cell(currentRow, 3).Value = MissingTicket.Barcode;
                        worksheet2.Cell(currentRow, 4).Value = MissingTicket.TimeIn;
                        worksheet2.Cell(currentRow, 5).Value = MissingTicket.TicketNumber;
                        worksheet2.Cell(currentRow, 6).Value = MissingTicket.Color;
                        worksheet2.Cell(currentRow, 7).Value = MissingTicket.Make;
                        worksheet2.Cell(currentRow, 8).Value = MissingTicket.Model;
                        worksheet2.Cell(currentRow, 9).Value = MissingTicket.ExtraServices;
                        worksheet2.Cell(currentRow, 10).Value = MissingTicket.SaleAmt;
                        worksheet2.Cell(currentRow, 11).Value = MissingTicket.Discount;
                        worksheet2.Cell(currentRow, 12).Value = MissingTicket.PaidBy;
                    }
                }
                var worksheet3 = workbook.Worksheets.Add("Coupons");
                currentRow = 1;
                worksheet3.Cell(currentRow, 1).Value = "Manager";
                worksheet3.Cell(currentRow, 2).Value = "Date";
                worksheet3.Cell(currentRow, 3).Value = "Barcode";
                worksheet3.Cell(currentRow, 4).Value = "Time";
                worksheet3.Cell(currentRow, 5).Value = "Tkt #";
                worksheet3.Cell(currentRow, 6).Value = "Color";
                worksheet3.Cell(currentRow, 7).Value = "Make";
                worksheet3.Cell(currentRow, 8).Value = "Model";
                worksheet3.Cell(currentRow, 9).Value = "Extra serv";
                worksheet3.Cell(currentRow, 10).Value = "Sale amt";
                worksheet3.Cell(currentRow, 11).Value = "Discount";
                worksheet3.Cell(currentRow, 12).Value = "paid by";

                if (rptResult.Coupon != null)
                {
                    foreach (var Coupon in rptResult.Coupon)
                    {
                        currentRow++;
                        worksheet3.Cell(currentRow, 1).Value = Coupon.Manager;
                        worksheet3.Cell(currentRow, 2).Value = Coupon.JobDate;
                        worksheet3.Cell(currentRow, 3).Value = Coupon.Barcode;
                        worksheet3.Cell(currentRow, 4).Value = Coupon.TimeIn;
                        worksheet3.Cell(currentRow, 5).Value = Coupon.TicketNumber;
                        worksheet3.Cell(currentRow, 6).Value = Coupon.Color;
                        worksheet3.Cell(currentRow, 7).Value = Coupon.Make;
                        worksheet3.Cell(currentRow, 8).Value = Coupon.Model;
                        worksheet3.Cell(currentRow, 9).Value = Coupon.ExtraServices;
                        worksheet3.Cell(currentRow, 10).Value = Coupon.SaleAmt;
                        worksheet3.Cell(currentRow, 11).Value = Coupon.Discount;
                        worksheet3.Cell(currentRow, 12).Value = Coupon.PaidBy;
                    }
                }

                var worksheet4 = workbook.Worksheets.Add("Deposit off");
                currentRow = 1;
                worksheet4.Cell(currentRow, 1).Value = "Manager";
                worksheet4.Cell(currentRow, 2).Value = "JobDate";
                worksheet4.Cell(currentRow, 3).Value = "Difference";

                if (rptResult.DepositOff != null)
                {
                    foreach (var deposit in rptResult.DepositOff)
                    {
                        currentRow++;
                        worksheet4.Cell(currentRow, 1).Value = deposit.Manager;
                        worksheet4.Cell(currentRow, 2).Value = deposit.JobDate;
                        worksheet4.Cell(currentRow, 3).Value = deposit.Difference;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "StatusReport.xlsx");
                }
            }
        }
    }
}
