using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Location;
using Strive.BusinessEntities.DTO.MembershipSetup;
using Strive.BusinessEntities.MembershipSetup;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class MembershipSetupRal : RalBase
    {
        public MembershipSetupRal(ITenantHelper tenant) : base(tenant) { }
        public List<AllMembershipViewModel> GetAllMembership()
        {
            return db.Fetch<AllMembershipViewModel>(EnumSP.Membership.USPGETALLMEMBERSHIP.ToString(), null);
        }

        public MembershipExistViewModel GetMembershipAvailability(int vehicleId)
        {
            _prm.Add("@VehicleId",vehicleId);
            return db.FetchSingle<MembershipExistViewModel>(EnumSP.Membership.USPGETVEHICLEMEMBERSHIPAVAILABILITY.ToString(), _prm);
        }
        public bool AddMembership(MembershipDto member)
        {
            return dbRepo.InsertPc(member,"MembershipId");
        }

        public bool UpdateMembership(MembershipDto member)
        {
            return dbRepo.UpdatePc(member);
        }
        public bool DeleteMembershipById(int membershipid)
        {
            _prm.Add("MembershipId", membershipid);
            db.Save(EnumSP.Membership.USPDELETEMEMBERSHIP.ToString(), _prm);
            return true;
        }

        public bool DeleteVehicleMembershipById(int ClientMembershipid)
        {
            _prm.Add("ClientMembershipId", ClientMembershipid);
            db.Save(EnumSP.Membership.USPDELETEVEHICLEMEMBERSHIP.ToString(), _prm);
            return true;
        }

        public List<MembershipServiceViewModel> GetMembershipById(int membershipid)
        {
            _prm.Add("@MembershipId", membershipid);
            return db.Fetch<MembershipServiceViewModel>(EnumSP.Membership.USPGETMEMBERSHIPLISTSETUPBYMEMBERSHIPID.ToString(), _prm);
        }
        public MembershipAndServiceViewModel GetMembershipAndServiceByMembershipId(int id)
        {
            _prm.Add("@MembershipId", id);
            return db.FetchMultiResult<MembershipAndServiceViewModel>(EnumSP.Membership.USPGETMEMBERSHIPSERVICEBYMEMBERSHIPID.ToString(), _prm);
        }
        public List<AllMembershipViewModel> GetMembershipSearch(MembershipSearchDto search)
        {
            _prm.Add("@MembershipSearch", search.MembershipSearch);
            var result = db.Fetch<AllMembershipViewModel>(EnumSP.Membership.USPGETALLMEMBERSHIP.ToString(), _prm);
            return result;
        }


        public bool GetVehicleMembershipByMembershipId(int membershipid)
        {
            _prm.Add("@MembershipId", membershipid);
            var result = db.Fetch<VehicleMembershipByMembership>(EnumSP.Membership.USPGETVEHICLEMEMBERSHIPBYMEMBERSHIPID.ToString(), _prm);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<MembershipNameViewModel> GetAllMembershipName()
        {
            return db.Fetch<MembershipNameViewModel>(EnumSP.Membership.USPGETALLMEMBERSHIPNAME.ToString(), null);
        }

    }
}