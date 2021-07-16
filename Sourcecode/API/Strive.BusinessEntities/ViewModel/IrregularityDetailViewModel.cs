using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
  public  class IrregularityDetailViewModel
    {
        public string Manager { get; set; }
        public DateTime? JobDate { get; set; }
        public string Barcode { get; set; }
        public string TimeIn { get; set; }
        public string TicketNumber { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal ExtraServices { get; set; }
        public decimal SaleAmt { get; set; }
        public decimal Discount { get; set; }
        public string PaidBy { get; set; }

    }
}
