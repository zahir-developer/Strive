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
        public List<MembershipViewModel> GetAllMembership()
        {
            return db.Fetch<MembershipViewModel>(SPEnum.USPGETMEMBERSHIPSETUP.ToString(), null);
        }
        public List<JobItem> GetServicesWithPrice()
        {
            return db.Fetch<JobItem>(SPEnum.USPGETSERVICEWITHPRICE.ToString(), null);
        }
        public bool AddMembership(List<MembershipDto> member)
        {
            return dbRepo.Insert(member);
        }

        public bool UpdateMembership(List<MembershipDto> member)
        {
            return dbRepo.Update(member);
        }
        public bool DeleteMembershipById(int membershipid)
        {
            _prm.Add("MembershipId", membershipid);
            db.Save(SPEnum.USPDELETEMEMBERSHIP.ToString(), _prm);
            return true;
        }

        public List<MembershipViewModel> GetMembershipById(int membershipid)
        {
            _prm.Add("@MembershipId", membershipid);
            return db.Fetch<MembershipViewModel>(SPEnum.USPGETMEMBERSHIPSETUP.ToString(), _prm);
        }
    }
}