using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PayRollCategoryViewModel
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public string CodeValue { get; set; }
        public int? EmployeeId { get; set; }
        public int? LiabilityType { get; set; }
        public int? LiabilityDetailType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Collision { get; set; }
        public decimal? Uniform { get; set; }
        public decimal? Adjustment { get; set; }
    }
}
