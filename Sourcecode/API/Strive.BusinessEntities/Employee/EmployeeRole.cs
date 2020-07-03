namespace Strive.BusinessEntities.Employee
{
    public class EmployeeRole
    {
        public int EmployeeRoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
