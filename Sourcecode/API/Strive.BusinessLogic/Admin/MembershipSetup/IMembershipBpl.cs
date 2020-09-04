using Strive.BusinessEntities.MembershipSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.MembershipSetup
{
    public interface IMembershipBpl
    {
        Result GetAllMembership();
        Result GetServicesWithPrice();
        //Result GetServiceWithPrice();
        Result GetMembershipById(int membershipId);
        Result SaveMembershipSetup(List<MembershipView> member);
        Result DeleteMembershipById(int membershipId);

    }
}
