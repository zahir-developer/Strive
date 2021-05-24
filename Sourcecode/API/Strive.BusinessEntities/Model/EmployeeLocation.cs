using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblEmployeeLocation")]
    public class EmployeeLocation
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeLocationId { get; set; }

        [Column, PrimaryKey]
        public int EmployeeId { get; set; }

        [Column]
        public int LocationId { get; set; }

        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public string LocationName { get; set; }

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

        [Column]
        public decimal? HourlyWashRate { get; set; }




    }
}