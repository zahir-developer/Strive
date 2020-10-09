using Strive.BusinessEntities.Model;
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
        public bool AddWhiteLabelling(WhiteLabel whiteLabel)
        {
            return dbRepo.SavePc(whiteLabel, "WhiteLabelId");
        }
    }
}
