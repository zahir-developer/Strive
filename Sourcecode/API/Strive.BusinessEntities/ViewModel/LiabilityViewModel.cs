using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class LiabilityViewModel
    {
        public string LiabilityId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public int? VehicleId { get; set; }
        public int? TypeId { get; set; }
        public string LiabilityType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
