using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class WashesViewModel
    {
        public int JobId { get; set; }
        public string Barcode { get; set; }
        public string TicketNumber { get; set; }
        public int LocationId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public int? VehicleId { get; set; }
        public int Make { get; set; }
        public int Model { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMake { get; set; }
        public int Color { get; set; }
        public string VehicleColor { get; set; }
        public int? JobType { get; set; }
        public DateTime JobDate { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset? EstimatedTimeOut { get; set; }
        public DateTimeOffset? ActualTimeOut { get; set; }
        public int? JobStatus { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string ReviewNote { get; set; }
    
        public string PastHistoryNote { get; set; }

        public string IsPaid { get; set; }

    }
}
