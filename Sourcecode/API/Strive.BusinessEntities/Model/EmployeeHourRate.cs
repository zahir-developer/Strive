using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblEmployeeHourRate")]
    public class EmployeeHourRate
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeHourRateId { get; set; }

        [Column]
        public int EmployeeId { get; set; }

       [Column]
        public int RoleId { get; set; }

        [Column]
        public int LocationId { get; set; }

        [Column]
        public int HourlyRate{ get; set; }
       
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
        public bool? Tips { get; set; }

    }
}