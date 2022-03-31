using Strive.BusinessEntities;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class PrinterRal : RalBase
    {
        public PrinterRal(ITenantHelper tenant) : base(tenant) { }

        public PrinterViewModel GetPrinterByLocation(int locationId)
        {
            _prm.Add("@Location", locationId);
            var result = db.FetchSingle<PrinterViewModel>(EnumSP.Printer.USPGETPRINTERBYLOCATION.ToString(), _prm);
            return result;
        }
    }
}
