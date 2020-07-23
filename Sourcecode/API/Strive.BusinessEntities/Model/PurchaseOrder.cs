namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="PurchaseOrder" />.
    /// </summary>
    [OverrideName("tblPurchaseOrder")]
    public class PurchaseOrder
    {
        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        [Column]
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the VendorId.
        /// </summary>
        [Column]
        public int? VendorId { get; set; }

        /// <summary>
        /// Gets or sets the IsAutoRequest.
        /// </summary>
        [Column]
        public bool? IsAutoRequest { get; set; }

        /// <summary>
        /// Gets or sets the IsMailSent.
        /// </summary>
        [Column]
        public bool? IsMailSent { get; set; }

        /// <summary>
        /// Gets or sets the OrderedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? OrderedDate { get; set; }

        /// <summary>
        /// Gets or sets the OrderedBy.
        /// </summary>
        [Column]
        public int? OrderedBy { get; set; }

        /// <summary>
        /// Gets or sets the OrderDetails.
        /// </summary>
        [Column]
        public string OrderDetails { get; set; }

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
