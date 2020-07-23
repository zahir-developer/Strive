namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="ClientAddress" />.
    /// </summary>
    [OverrideName("tblClientAddress")]
    public class ClientAddress
    {
        /// <summary>
        /// Gets or sets the AddressId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the RelationshipId.
        /// </summary>
        [Column]
        public int? RelationshipId { get; set; }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        [Column]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        [Column]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the PhoneNumber.
        /// </summary>
        [Column]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the PhoneNumber2.
        /// </summary>
        [Column]
        public string PhoneNumber2 { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        [Column]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        [Column]
        public int? City { get; set; }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        [Column]
        public int? State { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        [Column]
        public int? Country { get; set; }

        /// <summary>
        /// Gets or sets the Zip.
        /// </summary>
        [Column]
        public string Zip { get; set; }

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
