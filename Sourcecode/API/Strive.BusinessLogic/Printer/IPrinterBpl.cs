using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Printer
{
    public interface IPrinterBpl
    {
        Result GetPrinterByLocation(int locationId);
    }
}
