using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model 
{
    [OverrideName("tblEmployeeRole")]
    public class EmployeeRole
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeRoleId { get; set; }

        [Column, PrimaryKey]
        public int? EmployeeId { get; set; }

        [Column]
        public int? RoleId { get; set; }

        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public string RoleName { get; set; }

        [Column]
        public bool? IsDefault { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }

        [Column]
        public int? CreatedBy { get; set; }

        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }

        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }

    }
}