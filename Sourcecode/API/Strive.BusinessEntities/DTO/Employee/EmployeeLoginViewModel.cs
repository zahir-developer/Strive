using System.Collections.Generic;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeLoginViewModel
    {
        public EmployeeLoginDto EmployeeLogin { get; set; }
        public List<EmployeeRoleDto> EmployeeRoles { get; set; }
        public List<EmployeeLocationDto> EmployeeLocations { get; set; }
    }
}
