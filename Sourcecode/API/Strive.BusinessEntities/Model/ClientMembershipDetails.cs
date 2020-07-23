namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="ClientMembershipDetails" />.
    /// </summary>
    [OverrideName("tblClientMembershipDetails")]
    public class ClientMembershipDetails
    {
        /// <summary>
        /// Gets or sets the ClientMembershipId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int ClientMembershipId { get; set; }

        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        [Column]
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the MembershipId.
        /// </summary>
        [Column]
        public int MembershipId { get; set; }

        /// <summary>
        /// Gets or sets the StartDate.
        /// </summary>
        [Column]
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the EndDate.
        /// </summary>
        [Column]
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        [Column]
        public bool? Status { get; set; }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        [Column]
        public string Notes { get; set; }

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
