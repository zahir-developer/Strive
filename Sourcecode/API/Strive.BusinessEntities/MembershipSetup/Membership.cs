using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.MembershipSetup
{
    public class Membership
    {
        public int MembershipId { get; set; }
        public string MembershipName { get; set; }
        public int LocationId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
