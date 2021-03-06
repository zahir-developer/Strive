using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class SalesViewModel
    {
        public string TicketNumber { get; set; }
        public decimal Tax { get; set; }
        public decimal Cashback { get; set; }
        public decimal Price { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
        public decimal GiftCard { get; set; }   
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
