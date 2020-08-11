using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.MembershipSetup
{
    public class MembershipDto
    {
        public Model.Membership Membership { get; set; }
        public ClientMembershipDetails ClientMembershipDetails { get; set; }
    }
}
