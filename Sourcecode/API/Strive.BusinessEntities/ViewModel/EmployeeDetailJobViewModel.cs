using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class EmployeeDetailJobViewModel
    {
        public int JobId { get; set; }
        public string Barcode { get; set; }
        public string TicketNumber { get; set; }
        public int LocationId { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public int? JobStatus { get; set; }
        public DateTime JobDate { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset EstimatedTimeOut { get; set; }
        public string DetailService { get; set; }
        public string AdditionalService { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
