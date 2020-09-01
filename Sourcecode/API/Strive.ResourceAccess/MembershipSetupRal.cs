using Dapper;
using Strive.BusinessEntities;
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
            db.Save(SPEnum.USPDELETEMEMBERSHIP.ToString(), _prm);
            return true;
        }

        public List<MembershipServiceViewModel> GetMembershipById(int membershipid)
        {
            _prm.Add("@MembershipId", membershipid);
            return db.Fetch<MembershipServiceViewModel>(SPEnum.USPGETMEMBERSHIPLISTSETUPBYMEMBERSHIPID.ToString(), _prm);
        }
        public MembershipAndServiceViewModel GetMembershipAndServiceByMembershipId(int id)
        {
            _prm.Add("@MembershipId", id);
            return db.FetchMultiResult<MembershipAndServiceViewModel>(SPEnum.USPGETMEMBERSHIPSERVICEBYMEMBERSHIPID.ToString(), _prm);
        }
    }
}