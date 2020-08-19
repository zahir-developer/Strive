using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class EmployeeDetails
    {
        public EmployeeDetails()
        {
        }
        public EmployeeLogin EmployeeLogin { get; set; }
        public List<EmployeeRoleApi> EmployeeRoles { get; set; }
        public List<EmployeeLocation> EmployeeLocations { get; set; }
        public List<Drawer> Drawer { get; set; }
    }

    public class EmployeeLogin
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeLocation
    {
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }

    public class Drawer
    {
        public int DrawerId { get; set; }
        public string DrawerName { get; set; }
        public int LocationId { get; set; }
        public object IsActive { get; set; }
        public object IsDeleted { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedDate { get; set; }
        public object UpdatedBy { get; set; }
        public object UpdatedDate { get; set; }
    }
}
