using Strive.BusinessEntities.DTO.MembershipSetup;
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
        Result GetMembershipById(int membershipId);
        Result GetMembershipAvailability(int vehicleId);
        Result AddMembership(MembershipDto member);
        Result UpdateMembership(MembershipDto member);
        Result DeleteMembershipById(int membershipId);
        Result DeleteVehicleMembershipById(int clientMembershipId);
        Result GetMembershipAndServiceByMembershipId(int id);
        Result GetMembershipSearch(MembershipSearchDto search);

        Result GetVehicleMembershipByMembershipId(int membershipId);

        Result GetAllMembershipName(int locationId);
    }


}
