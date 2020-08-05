using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public int? Collisions { get; set; }
        public int? Documents { get; set; }
        public int? Schedules { get; set; }
        public bool Status { get; set; }
    }
}