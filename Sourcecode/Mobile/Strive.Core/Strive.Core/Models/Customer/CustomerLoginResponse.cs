using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer
{
    public class CustomerLoginResponse
    {
        public ClientDetails ClientDetails { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
    public class ClientDetails
    {
        public ClientDetail ClientDetail { get; set; }
        public List<RolePermissionViewModel> RolePermissionViewModel { get; set; }
    }
    public class RolePermissionViewModel
    {
        public int EmployeeID { get; set; }
        public int ClientId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
        public string ViewName { get; set; }
        public object FieldName { get; set; }
    }
    public class ClientDetail
    {
        public int ClientId { get; set; }
        public int AuthId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}
