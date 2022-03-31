using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PrinterViewModel
    {
        public int PrinterId { get; set; }
        public string PrinterName { get; set; }
        public string IpAddress { get; set; }
        public int LocationId { get; set; }
    }
}
