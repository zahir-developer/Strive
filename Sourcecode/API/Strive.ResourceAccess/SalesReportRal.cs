﻿using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class SalesReportRal : RalBase
    {
        public SalesReportRal(ITenantHelper tenant) : base(tenant) { }
        public SalesReportDetailViewModel GetMonthlySalesReport(SalesReportDto monthlysales)
        {
            _prm.Add("@LocationId", monthlysales.LocationId);
            _prm.Add("@FromDate", monthlysales.FromDate);
            _prm.Add("@EndDate", monthlysales.EndDate);
            var result = db.FetchMultiResult<SalesReportDetailViewModel>(EnumSP.SalesReport.USPGETMONTHLYSALESREPORT.ToString(), _prm);
            return result;
        }
    }
}
