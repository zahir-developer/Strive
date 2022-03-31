using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PrintJobItem
    {
        public int JobItemId { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        public string ServiceType { get; set; }

    }
}
