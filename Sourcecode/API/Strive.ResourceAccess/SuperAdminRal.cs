using Dapper;
using Strive.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Strive.Repository;
using System.Linq;
using System.Data;
using Strive.Common;
using Strive.BusinessEntities.ViewModel;

namespace Strive.ResourceAccess
{
    public class SuperAdminRal : RalBase
    {
        public SuperAdminRal(ITenantHelper tenant, bool isAuth = false) : base(tenant, isAuth)
        {
        }

        public bool CreateTenant(TenantViewModel tenant)
        {
            _prm.Add("@TenantName", tenant.TenantName);
            _prm.Add("@TenantEmail", tenant.TenantEmail);
            _prm.Add("@Subscriptionid", tenant.SubscriptionId);
            _prm.Add("@SchemaPasswordHash", tenant.PasswordHash);
            _prm.Add("@ExpiryDate", tenant.ExpiryDate);

            CommandDefinition cmd = new CommandDefinition(SPEnum.USPCREATETENANT.ToString(), _prm, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
