using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class SalesItemViewModel
    {
        public int JobId { get; set; }
        public string TicketNumber { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        public int? JobItemId { get; set; }

    }
}