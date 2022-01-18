using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class PrintJobDto
    {
        public string Title { get; set; }
        public string TicketNumber { get; set; }
        public string InTime { get; set; }
        public string TimeOut { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Barcode { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleColor { get; set; }
        public string Notes { get; set; }
    }
}
