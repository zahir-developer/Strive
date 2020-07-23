namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Vendor" />.
    /// </summary>
    [OverrideName("tblVendor")]
    public class Vendor
    {
        /// <summary>
        /// Gets or sets the VendorId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the VIN.
        /// </summary>
        [Column]
        public string VIN { get; set; }

        /// <summary>
        /// Gets or sets the VendorName.
        /// </summary>
        [Column]
        public string VendorName { get; set; }

        /// <summary>
        /// Gets or sets the VendorAlias.
        /// </summary>
        [Column]
        public string VendorAlias { get; set; }

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
