using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class EmployeeAdjustmentDto
    {
        public int id { get; set;}
        public decimal adjustment { get; set; }
        public int LocationId { get; set; }
    }
}
