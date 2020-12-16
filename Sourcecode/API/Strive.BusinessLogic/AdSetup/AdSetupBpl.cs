using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.AdSetup;
using Strive.BusinessLogic.Document;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.AdSetup
{
    public class AdSetupBpl : Strivebase, IAdSetupBpl
    {
        public AdSetupBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        public Result AddAdSetup(AdSetupDto adSetup)
        {
            return ResultWrap(new AdSetupRal(_tenant).AddAdSetup, adSetup, "AddNewAdSetup");
        }

        public Result UpdateAdSetup(AdSetupDto adSetup)
        {
            return ResultWrap(new AdSetupRal(_tenant).UpdateAdSetup, adSetup, "UpdateAdSetup");
        }
        public Result GetAllAdSetup()
        {
            return ResultWrap(new AdSetupRal(_tenant).GetAllAdSetup, "GetAllAdSetup");
        }

        public Result GetAdSetupById(int id)
        {
            return ResultWrap(new AdSetupRal(_tenant).GetAdSetupById, id, "GetAdSetupByIdd");
        }

        public Result DeleteAdSetup(int id)
        {
            return ResultWrap(new AdSetupRal(_tenant).DeleteAdSetup, id, "AdSetupDelete");
        }
    }
}
