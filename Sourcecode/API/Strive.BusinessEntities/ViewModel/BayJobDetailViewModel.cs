using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class BayJobDetailViewModel
    {
        public int BayId { get; set; }
        public string BayName { get; set; }
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
        public string OutsideService { get; set; }
        public DateTime JobDate { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int JobItemId { get; set; }
        public decimal Cost { get; set; }

        public string Barcode { get; set; }
    }
}
