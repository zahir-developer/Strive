using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public bool? Collisions { get; set; }
        public bool? Documents { get; set; }
        public bool? Schedules { get; set; }
        public bool Status { get; set; }
    }
}
