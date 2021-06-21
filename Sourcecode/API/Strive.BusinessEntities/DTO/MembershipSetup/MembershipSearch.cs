using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.MembershipSetup
{
    public class MembershipSearch
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public int LocationId { get; set; }
    }
}
