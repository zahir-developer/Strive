using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CollisionViewModel
    {
        public long? LiabilityId { get; set; }
        public long? EmployeeId { get; set; }
        public int? LiabilityType { get; set; }
        public string LiabilityDescription { get; set; }
        public int ProductId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        //public List<LiabilityDetail> LiabilityDetail { get; set; }
    }
}
