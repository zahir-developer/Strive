using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class JobProductItemViewModel
    {
        public int JobProductItemId { get; set; }
        public int JobId { get; set; }
        public string TicketNumber { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductType { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ServiceType { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Cost { get; set; }
    }
}
