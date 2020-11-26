using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.BonusSetup
{
    public class BonusSetupDto
    {
        public Model.Bonus Bonus{get;set;}
        public List<BonusRange> BonusRange { get; set; }
    }
}
