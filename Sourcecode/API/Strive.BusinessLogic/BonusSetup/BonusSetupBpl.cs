using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.BonusSetup;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.BonusSetup
{
    public class BonusSetupBpl : Strivebase, IBonusSetupBpl
    {
        public BonusSetupBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result AddBonusSetup(BonusSetupDto bonus)
        {
            return ResultWrap(new BonusSetupRal(_tenant).AddBonusSetup, bonus, "AddBonusSetup");
        }
        public Result UpdateBonusSetup(BonusSetupDto bonus)
        {
            return ResultWrap(new BonusSetupRal(_tenant).UpdateBonusSetup, bonus, "UpdateBonusSetup");
        }
        public Result DeleteBonusSetup(int id)
        {
            return ResultWrap(new BonusSetupRal(_tenant).DeleteBonusSetup, id, "DeleteBonusSetup");
        }
        public Result GetBonus(BonusDto bonusInput)
        {
            return ResultWrap(new BonusSetupRal(_tenant).GetBonus, bonusInput, "BonusDetails");
        }
    }
}
