using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Employee
{
    public class EmployeeRoles
    {
        public long EmployeeRolesId { get; set; }
        public long EmployeeId { get; set; }
        //public string RoleName { get; set; }
        public int RoleId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        

    }
}
