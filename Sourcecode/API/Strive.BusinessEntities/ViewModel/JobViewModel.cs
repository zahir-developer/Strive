using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class JobViewModel
    {
        public int? JobId { get; set; }
        public string TicketNumber { get; set; }
        public string TimeIn { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string EstimatedTimeOut { get; set; }
        public string ServiceTypeName { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public decimal Upcharge { get; set; }       
        public DateTime JobDate { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }        
        public decimal Cost { get; set; }

        public string Barcode { get; set; }
        public string VehicleId { get; set; }
        public string PaymentDate { get; set; }
        public string JobPaymentId { get; set; }
        public string TipPaymentId { get; set; }
        public string TipAmount { get; set; }
    }
}
