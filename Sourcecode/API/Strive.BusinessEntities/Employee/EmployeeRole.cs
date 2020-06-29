using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{
    public class EmployeeRole
    {
        public int EmployeeRoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
