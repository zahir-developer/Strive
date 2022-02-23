using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class AllWashesViewModel
    {
        public int JobId { get; set; }
        public string TicketNumber { get; set; }
        public string JobDate { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public string Color { get; set; }

        public DateTimeOffset? TimeIn { get; set; }
        public DateTimeOffset? EstimatedTimeOut { get; set; }
        public string ServiceName { get; set; }

        public string IsPaid { get; set; } 
        public string MembershipName { get; set; }
    }
}
