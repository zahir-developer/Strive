using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.CustomerSummaryReport;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class CustomerSummaryReportRal : RalBase
    {
        public CustomerSummaryReportRal(ITenantHelper tenant) : base(tenant) { }
        public List<CustomerSummaryViewModel> GetCustomerSummaryReport(CustomerSummaryReportDto customersummary)
        {
            _prm.Add("@LocationId", customersummary.LocationId);
            _prm.Add("@Date", customersummary.Date);
            var result = db.Fetch<CustomerSummaryViewModel>(EnumSP.SalesReport.USPGETCUSTOMERSUMMARYREPORT.ToString(), _prm);
            return result;
        }
    }
}