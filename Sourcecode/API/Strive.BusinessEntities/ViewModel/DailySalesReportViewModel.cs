using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DailySalesReportViewModel
    {
        public string TicketNumber { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset TimeOut { get; set; }
        public DateTimeOffset Est { get; set; }
        public int EstTime { get; set; }
        //public int? TimeOutDifference { get; set; }
        public string Deviation { get; set; }
        public int MerchandiseItemsPurchased { get; set; }
        public string Barcode { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
        public decimal? Amount { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public string PaymentType { get; set; }
    }
}
