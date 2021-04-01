using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientHistoryViewModel
    {
        public string TicketNumber { get; set; }
        public DateTime Date { get; set; }
        public string ServiceCompleted { get; set; }
        //public string Detailer { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Comm { get; set; }
        public int JobId { get; set; }

        public string JobType { get; set; }
        public string ServiceType { get; set; }
    }
}
