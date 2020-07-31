using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.MembershipSetup;
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
    public class MembershipSetupRal
    {
        private readonly Db _db;

        public MembershipSetupRal(ITenantHelper tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }
        public List<MembershipView> GetAllMembership()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<MembershipView> lstResource = new List<MembershipView>();
            var res = _db.FetchRelation1<MembershipView, ClientMembership>(SPEnum.USPGETMEMBERSHIPSETUP.ToString(), dynParams);
            return res;
        }
        public List<JobItem> GetServicesWithPrice()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<JobItem> lstResource = new List<JobItem>();
            var res = _db.Fetch<JobItem>(SPEnum.USPGETSERVICEWITHPRICE.ToString(), dynParams);
            return res;
        }
        public bool SaveMembershipSetup(List<MembershipView> member)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Membership> lstMembership = new List<Membership>();
            var newMember = member.FirstOrDefault();
            lstMembership.Add(new Membership
            {
                MembershipId = newMember.MembershipId,
                MembershipName = newMember.MembershipName,
                LocationId = newMember.LocationId,
                IsActive = newMember.IsActive,
                DateCreated = newMember.DateCreated,
            });
            dynParams.Add("@tvpMembership", lstMembership.ToDataTable().AsTableValuedParameter("tvpMembership"));
            dynParams.Add("@tvpClientMembershipDetails", member.FirstOrDefault().ClientMembership.ToDataTable().AsTableValuedParameter("tvpClientMembershipDetails"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEMEMBERSHIPSETUP.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            _db.Save(cmd);

            return true;
        }
        public bool DeleteMembershipById(int membershipid)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@MembershipId", membershipid.toInt());
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETEMEMBERSHIP.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            _db.Save(cmd);
            return true;
        }

        public List<MembershipView> GetMembershipById(int membershipid)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@MembershipId", membershipid.toInt());
            var res = _db.FetchRelation1<MembershipView, ClientMembership>(SPEnum.USPGETMEMBERSHIPBYID.ToString(), dynParams);
            return res;
        }
    }
}
