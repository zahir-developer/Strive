using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MembershipAndServiceViewModel
    {
        public MembershipViewModel Membership {get;set;}
        public List<MembershipServiceViewModel> MembershipService { get; set; }
    }
}
