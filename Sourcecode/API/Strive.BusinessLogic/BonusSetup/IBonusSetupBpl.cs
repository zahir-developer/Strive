using Strive.BusinessEntities.DTO.BonusSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.BonusSetup
{
    public interface IBonusSetupBpl
    {
        Result AddBonusSetup(BonusSetupDto bonus);
        Result UpdateBonusSetup(BonusSetupDto bonus);
        Result DeleteBonusSetup(int bonusId);
        Result GetBonus(BonusInputDto bonusInput);
    }
}
