using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.BonusSetup;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class BonusSetupRal : RalBase
    {
        public BonusSetupRal(ITenantHelper tenant) : base(tenant) { }

        public bool AddBonusSetup(BonusSetupDto bonus)
        {
            return dbRepo.InsertPc(bonus, "BonusId");
        }
        public bool UpdateBonusSetup(BonusSetupDto bonus)
        {
            return dbRepo.UpdatePc(bonus);
        }

        public bool DeleteBonusSetup(int id)
        {
            _prm.Add("@BonusId", id);
            db.Save(EnumSP.SystemSetup.USPDELETEBONUSSETUP.ToString(), _prm);
            return true;
        }

        public BonusSetupViewModel GetBonus(BonusInputDto bonusInput)
        {
            _prm.Add("@BonusMonth", bonusInput.BonusMonth);
            _prm.Add("@BonusYear", bonusInput.BonusYear);
            _prm.Add("@LocationId", bonusInput.LocationId);
            var result = db.FetchMultiResult<BonusSetupViewModel>(EnumSP.SystemSetup.USPGETBONUS.ToString(), _prm);
            return result;
        }
    }

}