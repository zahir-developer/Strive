using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.DealSetup
{
    public class DealSetupBpl : Strivebase, IdealSetupBpl
    {
        public DealSetupBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result AddDealSetup(DealSetupDto dealSetup)
        {
            return ResultWrap(new DealSetupRal(_tenant).AddDealSetup, dealSetup, "dealSetup");
        }
        

            public Result GetAllDeals()
        {
            return ResultWrap(new DealSetupRal(_tenant).GetAllDeals, "GetAllDeals");
        }
     \
    }
}