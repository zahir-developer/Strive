using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeLoginDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int AuthId { get; set; }
        public int? ClientId { get; set; }
        public int? ClientAuthId { get; set; }
        public bool IsActive { get; set; }
    }
}
