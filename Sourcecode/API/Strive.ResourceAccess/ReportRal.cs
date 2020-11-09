﻿using Strive.BusinessEntities;
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
    }
}