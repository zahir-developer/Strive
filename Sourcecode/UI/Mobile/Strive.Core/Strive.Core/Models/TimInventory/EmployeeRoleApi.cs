using System;
namespace Strive.Core.Models.TimInventory
{
    public class EmployeeRoleApi
    {
        public EmployeeRoleApi()
        {
        }

        public int EmployeeRoleId { get; set; }
        public int EmployeeId { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
