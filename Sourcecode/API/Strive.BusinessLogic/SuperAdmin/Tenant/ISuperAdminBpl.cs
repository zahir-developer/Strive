﻿using Strive.BusinessEntities.ViewModel;
using Strive.Common;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
     public interface ISuperAdminBpl
    {
        Result CreateTenant(TenantViewModel tenant);
    }
}
