using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class EmployeeDetails
    {
        public EmployeeDetails()
        {
        }
        public int EmployeeId { get; set; }
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
        public object EmployeeAddress { get; set; }
        public List<EmployeeRoleApi> EmployeeRole { get; set; }
        public List<EmployeeRoleApi> EmployeeRoles { get; set; }


    }
}
