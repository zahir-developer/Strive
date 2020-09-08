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
        public string ServiceName { get; set; }
        public string ServiceTypeName { get; set; }
    }
}
