using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.AdSetup;
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

        public Result AddDealSetup(Deals dealSetup)
        {
            return ResultWrap(new DealSetupRal(_tenant).AddDealSetup, dealSetup, "Status");
        }
        

            public Result GetAllDeals()
        {
            return ResultWrap(new DealSetupRal(_tenant).GetAllDeals, "GetAllDeals");
        }
     public Result UpdateToggledeal(bool status)
        {
            return ResultWrap(new DealSetupRal(_tenant).UpdateToggledeal, status, "Status");
        }

        public Result AddClientDeal(ClientDealDto addClientDeal)
        {
            return ResultWrap(new DealSetupRal(_tenant).AddClientDeal, addClientDeal, "Status");
        }

        public Result GetClientDeal(ClientDealDto addClientDeal)
        {
            return ResultWrap(new DealSetupRal(_tenant).GetClientDeal, addClientDeal, "ClientDeal");
        }
        
    }
}