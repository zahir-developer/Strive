namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="EmployeeLiabilityDetail" />.
    /// </summary>
    [OverrideName("tblEmployeeLiabilityDetail")]
    public class EmployeeLiabilityDetail
    {
        /// <summary>
        /// Gets or sets the LiabilityDetailId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int LiabilityDetailId { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityId.
        /// </summary>
        [Column]
        public int? LiabilityId { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityDetailType.
        /// </summary>
        [Column]
        public int LiabilityDetailType { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        [Column]
        public Double? Amount { get; set; }

        /// <summary>
        /// Gets or sets the PaymentType.
        /// </summary>
        [Column]
        public int? PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the DocumentPath.
        /// </summary>
        [Column]
        public string DocumentPath { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        [Column]
        public string Description { get; set; }

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
