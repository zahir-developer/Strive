using Strive.BusinessEntities;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class WhiteLabelRal : RalBase
    {
        public WhiteLabelRal(ITenantHelper tenant) : base(tenant) { }
        public bool AddWhiteLabelling(WhiteLabelModel whiteLabel)
        {
            return dbRepo.Insert(whiteLabel);
        }
        public bool UpdateWhiteLabelling(WhiteLabelModel whiteLabel)
        {
            return dbRepo.SavePc(whiteLabel, "WhiteLabelId");
        }
        public WhiteLabelDetailViewModel GetAll()
        {
            return db.FetchMultiResult<WhiteLabelDetailViewModel>(EnumSP.WhiteLabelling.USPGETWHITELABEL.ToString(), _prm);
        }
        public bool SaveTheme(ThemeModel themes)
        {
            return dbRepo.SavePc(themes, "ThemeId");
        }
    }
}
