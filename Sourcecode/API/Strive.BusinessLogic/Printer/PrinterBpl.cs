using Microsoft.Extensions.Caching.Distributed;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Printer
{
    public class PrinterBpl : Strivebase, IPrinterBpl
    {
        public PrinterBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result GetPrinterByLocation(int locationId)
        {
            return ResultWrap(new PrinterRal(_tenant).GetPrinterByLocation, locationId, "PrinterDetail");
        }
    }
}
