using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblJobServiceEmployee")]
    public class JobServiceEmployee
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobServiceEmployeeId { get; set; }

        [Column]
        public int? JobItemId { get; set; }

        [Column]
        public int? ServiceId { get; set; }

        [Column]
        public int? EmployeeId { get; set; }

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