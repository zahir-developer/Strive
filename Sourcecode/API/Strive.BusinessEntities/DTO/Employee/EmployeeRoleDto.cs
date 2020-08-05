namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeRoleDto
    {
        public long EmployeeRoleId { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsDefault { get; set; }

    }
}