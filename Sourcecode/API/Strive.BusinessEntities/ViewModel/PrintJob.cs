using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PrintJob
    {
        public int JobId { get; set; }

        public string TicketNumber { get; set; }

        public string Barcode { get; set; }

        public string VehicleMake { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleColor { get; set; }

        public DateTimeOffset? TimeIn { get; set; }

        public DateTimeOffset? TimeOut { get; set; }

        public DateTimeOffset? EstimatedTimeOut { get; set; }

        public DateTime? JobDate { get; set; }

        public string JobType { get; set; }

        public string JobStatus { get; set; }

        public string Notes { get; set; }

    }
}
