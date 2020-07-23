namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Membership" />.
    /// </summary>
    [OverrideName("tblMembership")]
    public class Membership
    {
        /// <summary>
        /// Gets or sets the MembershipId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int MembershipId { get; set; }

        /// <summary>
        /// Gets or sets the MembershipName.
        /// </summary>
        [Column]
        public string MembershipName { get; set; }

        /// <summary>
        /// Gets or sets the ServiceId.
        /// </summary>
        [Column]
        public int? ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

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
