using Strive.BusinessEntities.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CollisionViewModel
    {
        public List<LiabilityViewModel> Liability { get; set; }
        public List<LiabilityDetail> LiabilityDetail { get; set; }
    }
}
