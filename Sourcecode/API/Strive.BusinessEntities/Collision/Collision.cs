using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Collision
{
    public class Collision
    {
        public long? LiabilityId { get; set; }
        public long? EmployeeId { get; set; }
        public int? LiabilityType { get; set; }
        public string LiabilityDescription { get; set; }
        public int ProductId { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        
    }
}
