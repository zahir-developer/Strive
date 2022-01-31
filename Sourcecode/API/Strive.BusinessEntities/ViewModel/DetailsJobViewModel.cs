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
        public int? Make { get; set; }
        public int? Model { get; set; }
        public int? Color { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public int? JobType { get; set; }
        public int? JobStatus { get; set; }
        public DateTime JobDate { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset EstimatedTimeOut { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Notes { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string ColorName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string IsPaid { get; set; }
        public string JobDetailId { get; set; }
    }
}
