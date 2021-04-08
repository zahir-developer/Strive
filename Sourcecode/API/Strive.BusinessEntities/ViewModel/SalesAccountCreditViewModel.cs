using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class SalesAccountCreditViewModel
    {
       public string TicketNumber { get; set; }
        public int? ClientId { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsCreditAccount { get; set; }

    }
}
