using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Collision
{
    public class EmployeeLiabilityDetail
    {
        public long? LiabilityDetailId { get; set; }
        public long? LiabilityId { get; set; }
        public int? LiabilityDetailType { get; set; }
        public float Amount { get; set; }
        public int PaymentType { get; set; }
        public string DocumentPath { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
