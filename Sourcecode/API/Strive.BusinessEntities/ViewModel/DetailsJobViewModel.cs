using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DetailsJobViewModel
    {
        public int JobId { get; set; }
        public int BayId { get; set; }
        public string Barcode { get; set; }
        public string TicketNumber { get; set; }
        public int LocationId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public int? VehicleId { get; set; }
        public int Make { get; set; }
        public int Model { get; set; }
        public int Color { get; set; }
        public int? JobType { get; set; }
        public DateTime JobDate { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset DueTime { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }

    }
}
