namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="EmployeeLiability" />.
    /// </summary>
    [OverrideName("tblEmployeeLiability")]
    public class EmployeeLiability
    {
        /// <summary>
        /// Gets or sets the LiabilityId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int LiabilityId { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeId.
        /// </summary>
        [Column]
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityType.
        /// </summary>
        [Column]
        public int LiabilityType { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityDescription.
        /// </summary>
        [Column]
        public string LiabilityDescription { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        [Column]
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        [Column]
        public int? Status { get; set; }

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
