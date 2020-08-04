namespace Strive.BusinessEntities.Employee
{
    public class EmployeeRole
    {
        public long EmployeeRoleId { get; set; }
        public long EmployeeId { get; set; }
        public int RoleId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        
    }
}
