using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.MonthlySalesReport
{
    public class MonthlySalesReportBpl : Strivebase, IMonthlySalesReportBpl
    {
        public MonthlySalesReportBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetMonthlySalesReport(MonthlySalesReportDto monthlysales)
        {
            return ResultWrap(new MonthlySalesReportRal(_tenant).GetMonthlySalesReport, monthlysales, "GetMonthlySalesReport");
        }
    }
}
