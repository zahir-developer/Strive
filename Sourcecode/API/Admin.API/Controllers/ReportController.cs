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
        [Route("GetMonthlyMoneyOwnedReport/{date}")]
        public Result GetMonthlyMoneyOwnedReport(string date) => _bplManager.GetMonthlyMoneyOwnedReport(date);


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

                

                var coins = eodResult.CashRegister.CashRegisterCoins;
                {
                    worksheet2.Cell(1, 2).Value = "";
                    worksheet2.Cell(2, 2).Value = coins.Pennies.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet2.Cell(3, 2).Value = coins.Nickels.ToString("C", CultureInfo.GetCultureInfo("en-US"));;
                    worksheet2.Cell(4, 2).Value = coins.Dimes.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet2.Cell(5, 2).Value = coins.Quarters.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet2.Cell(6, 2).Value = coins.HalfDollars.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                }
                var rolls = eodResult.CashRegister.CashRegisterRolls;
                {
                    worksheet2.Cell(7, 2).Value = "";
                    worksheet2.Cell(8, 2).Value = rolls.Pennies.ToString("C", CultureInfo.GetCultureInfo("en-US"));;
                    worksheet2.Cell(9, 2).Value = rolls.Nickels.ToString("C", CultureInfo.GetCultureInfo("en-US"));;
                    worksheet2.Cell(10, 2).Value = rolls.Dimes.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet2.Cell(11, 2).Value = rolls.Quarters.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                }
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
                
                foreach (var detailsInfo in eodResult.DailyStatusDetailInfoViews)
                {
                    currentRow++;
                    worksheet4.Cell(currentRow, 1).Value = detailsInfo.TicketNumber;
                    worksheet4.Cell(currentRow, 2).Value = detailsInfo.EmployeeName;
                    worksheet4.Cell(currentRow, 3).Value = detailsInfo.Commission;
                                      
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
                var sales = eodResult.Sales.EODSalesDetails;
                {
                    worksheet5.Cell(1, 2).Value = "";
                    worksheet5.Cell(2, 2).Value = "0.00";
                    worksheet5.Cell(3, 2).Value = "0.00";
                    worksheet5.Cell(4, 2).Value = "0.00";
                    worksheet5.Cell(5, 2).Value = sales.Credit.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(6, 2).Value = sales.TotalPaid.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(7, 2).Value = sales.Account.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(8, 2).Value = sales.GiftCard.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(9, 2).Value = sales.TotalPaid.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(10, 2).Value =sales.Total.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(11, 2).Value =sales.TaxAmount.ToString("C", CultureInfo.GetCultureInfo("en-US"));  
                    worksheet5.Cell(12, 2).Value =sales.GrandTotal.ToString("C", CultureInfo.GetCultureInfo("en-US"));
                    worksheet5.Cell(13, 2).Value =sales.Cash.ToString("C", CultureInfo.GetCultureInfo("en-US")); 
                    worksheet5.Cell(14, 2).Value ="0.00"; 
                    worksheet5.Cell(15, 2).Value =sales.Credit.ToString("C", CultureInfo.GetCultureInfo("en-US")); 
                    worksheet5.Cell(16, 2).Value =sales.Account.ToString("C", CultureInfo.GetCultureInfo("en-US")); 
                    worksheet5.Cell(17, 2).Value =sales.GiftCard.ToString("C", CultureInfo.GetCultureInfo("en-US")); 
                    worksheet5.Cell(18, 2).Value =sales.TotalPaid.ToString("C", CultureInfo.GetCultureInfo("en-US")); 
                    worksheet5.Cell(19, 2).Value =sales.CashBack.ToString("C", CultureInfo.GetCultureInfo("en-US"));  
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
                foreach (var employeeTimeClock in statusResult.EmployeeTimeClock.TimeClockEmployeeDetails)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = employeeTimeClock.FirstName;
                    worksheet.Cell(currentRow, 2).Value = employeeTimeClock.LastName;
                    worksheet.Cell(currentRow, 3).Value = employeeTimeClock.WashHours;
                    worksheet.Cell(currentRow, 4).Value = employeeTimeClock.DetailHours;
                    worksheet.Cell(currentRow, 5).Value = employeeTimeClock.OtherHours;
                }
                var worksheet2 = workbook.Worksheets.Add("Employee Time Clock Details");
                currentRow = 1;
                worksheet2.Cell(currentRow, 1).Value = "TimeClock Id";
                worksheet2.Cell(currentRow, 2).Value = "Employee Id ";
                worksheet2.Cell(currentRow, 3).Value = "Location Id ";
                worksheet2.Cell(currentRow, 4).Value = "Role Id";
                worksheet2.Cell(currentRow, 5).Value = "Role Name";
                worksheet2.Cell(currentRow, 6).Value = "Day";
                worksheet2.Cell(currentRow, 7).Value = "EventDate";
                worksheet2.Cell(currentRow, 8).Value = "InTime";
                worksheet2.Cell(currentRow, 9).Value = "OutTime";
                worksheet2.Cell(currentRow, 10).Value = "Total Hours";
                worksheet2.Cell(currentRow, 11).Value = "EventType";
                worksheet2.Cell(currentRow, 12).Value = "Status";
                foreach (var employeeTimeClock in statusResult.EmployeeTimeClock.TimeClockDetails)
                {
                    currentRow++;
                    worksheet2.Cell(currentRow, 1).Value = employeeTimeClock.TimeClockId;
                    worksheet2.Cell(currentRow, 2).Value = employeeTimeClock.EmployeeId;
                    worksheet2.Cell(currentRow, 3).Value = employeeTimeClock.LocationId;
                    worksheet2.Cell(currentRow, 4).Value = employeeTimeClock.RoleId;
                    worksheet2.Cell(currentRow, 5).Value = employeeTimeClock.RoleName;
                    worksheet2.Cell(currentRow, 6).Value = employeeTimeClock.Day;
                    worksheet2.Cell(currentRow, 7).Value = employeeTimeClock.EventDate;
                    worksheet2.Cell(currentRow, 8).Value = employeeTimeClock.InTime;
                    worksheet2.Cell(currentRow, 9).Value = employeeTimeClock.OutTime;
                    worksheet2.Cell(currentRow, 10).Value = employeeTimeClock.TotalHours;
                    worksheet2.Cell(currentRow, 11).Value = employeeTimeClock.EventType;
                    worksheet2.Cell(currentRow, 12).Value = employeeTimeClock.Status;
                }
                var worksheet4 = workbook.Worksheets.Add("Daily Status Info");
                currentRow = 1;
                worksheet4.Cell(currentRow, 1).Value = "TicketNumber";               
                worksheet4.Cell(currentRow, 2).Value = "FirstName";
                worksheet4.Cell(currentRow, 3).Value = "Commision";
                
                foreach (var detailsInfo in statusResult.DailyStatusDetailInfoViews)
                {
                    currentRow++;
                    worksheet4.Cell(currentRow, 1).Value = detailsInfo.TicketNumber;
                    worksheet4.Cell(currentRow, 2).Value = detailsInfo.EmployeeName;
                    worksheet4.Cell(currentRow, 3).Value = detailsInfo.Commission;
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
    }
}
