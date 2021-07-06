using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class SalesAccountViewModel
    {
       public string TicketNumber { get; set; }
        public int? ClientId { get; set; }
        public int? AccountType { get; set; }
        public string CodeValue { get; set; }
        public int? MembershipId { get; set; }
        public int? ClientMembershipId { get; set; }
        public int? VehicleId { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsAccount { get; set; }

    }
}
