using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Employee
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string SSNo { get; set; }
        public int MaritalStatus { get; set; }

        public bool IsCitizen { get; set; }
        public string AlienNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int ImmigrationStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public List<EmployeeDetail> EmployeeDetail { get; set; }
        public List<EmployeeAddress> EmployeeAddress { get; set; }
        public List<EmployeeRole> EmployeeRole { get; set; }
        public List<EmployeeRoles> EmployeeRoles { get; set; }
    }
}
