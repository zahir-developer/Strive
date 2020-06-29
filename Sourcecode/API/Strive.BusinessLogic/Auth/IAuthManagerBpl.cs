using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Strive.BusinessEntities;

namespace Strive.BusinessLogic
{
    public interface IAuthManagerBpl
    {
        Result Login(Authentication authentication, string secretKey, string tenantConnectionStringTemplate);
    }
}
