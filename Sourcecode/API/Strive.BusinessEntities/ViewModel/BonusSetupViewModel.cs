using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class BonusSetupViewModel
    {
        public Model.Bonus Bonus { get; set; }
        public List<BonusRange> BonusRange { get; set; }
        public LocationWashCountViewModel LocationBasedWashCount { get; set; }
    }
}
