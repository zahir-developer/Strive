using Strive.BusinessEntities;
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
    public class MonthlySalesReportRal : RalBase
    {
        public MonthlySalesReportRal(ITenantHelper tenant) : base(tenant) { }
        public MonthlySalesReportDetailViewModel GetMonthlySalesReport(MonthlySalesReportDto monthlysales)
        {
            _prm.Add("@LocationId", monthlysales.LocationId);
            _prm.Add("@Date", monthlysales.Date);
            var result = db.FetchMultiResult<MonthlySalesReportDetailViewModel>(SPEnum.USPGETMONTHLYSALESREPORT.ToString(), _prm);
            return result;
        }
    }
}
