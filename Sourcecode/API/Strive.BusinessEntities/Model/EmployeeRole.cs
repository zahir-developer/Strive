namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="EmployeeRole" />.
    /// </summary>
    [OverrideName("tblEmployeeRole")]
    public class EmployeeRole
    {
        /// <summary>
        /// Gets or sets the EmployeeId.
        /// </summary>
        [Column]
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the RoleId.
        /// </summary>
        [Column]
        public int? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the IsDefault.
        /// </summary>
        [Column]
        public bool? IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy.
        /// </summary>
        [Column]
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy.
        /// </summary>
        [Column]
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
