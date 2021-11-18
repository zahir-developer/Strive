using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using System.Collections.Generic;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeLoginViewModel
    {
        public EmployeeLoginDto EmployeeLogin { get; set; }
        public List<EmployeeRoleDto> EmployeeRoles { get; set; }
        public List<EmployeeLocationDto> EmployeeLocations { get; set; }
        public List<Drawer> Drawer { get; set; }
        public List<RolePermissionViewModel> RolePermissionViewModel { get; set; }
        public TokenExpireViewModel TokenExpireMinutes { get; set; }
    }
}
