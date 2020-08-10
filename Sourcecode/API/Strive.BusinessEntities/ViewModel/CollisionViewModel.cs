using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CollisionViewModel
    {
        public string LiabilityId { get; set; }
        public int TypeId { get; set; }
        public string LiabilityType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
