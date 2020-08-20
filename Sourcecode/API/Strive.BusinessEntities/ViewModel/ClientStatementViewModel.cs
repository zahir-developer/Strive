using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientStatementViewModel
    {
        public string TicketNumber { get; set; }
        public DateTime Date { get; set; }
        public string ServiceCompleted { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
