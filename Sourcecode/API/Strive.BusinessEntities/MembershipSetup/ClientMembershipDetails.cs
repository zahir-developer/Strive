using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.MembershipSetup
{
    public class ClientMembership
    {
        public int ClientMembershipId { get; set; }
        public int ClientId { get; set; }
        public int LocationId { get; set; }
        public int MembershipId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ServiceId { get; set; }
    }
}
