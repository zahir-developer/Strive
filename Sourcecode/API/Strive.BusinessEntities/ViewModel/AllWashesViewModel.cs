using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class AllWashesViewModel
    {
        public int JobId { get; set; }
        public string TicketNumber { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleName { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset EstimatedTimeOut { get; set; }
        public string ServiceName { get; set; }
    }
}
