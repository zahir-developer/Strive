namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Product" />.
    /// </summary>
    [OverrideName("tblProduct")]
    public class Product
    {
        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        [Column]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the ProductCode.
        /// </summary>
        [Column]
        public string ProductCode { get; set; }

        /// <summary>
        /// Gets or sets the ProductDescription.
        /// </summary>
        [Column]
        public string ProductDescription { get; set; }

        /// <summary>
        /// Gets or sets the ProductType.
        /// </summary>
        [Column]
        public int? ProductType { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the VendorId.
        /// </summary>
        [Column]
        public int? VendorId { get; set; }

        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        [Column]
        public int? Size { get; set; }

        /// <summary>
        /// Gets or sets the SizeDescription.
        /// </summary>
        [Column]
        public string SizeDescription { get; set; }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        [Column]
        public Double? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the QuantityDescription.
        /// </summary>
        [Column]
        public string QuantityDescription { get; set; }

        /// <summary>
        /// Gets or sets the Cost.
        /// </summary>
        [Column]
        public Double? Cost { get; set; }

        /// <summary>
        /// Gets or sets the IsTaxable.
        /// </summary>
        [Column]
        public bool? IsTaxable { get; set; }

        /// <summary>
        /// Gets or sets the TaxAmount.
        /// </summary>
        [Column]
        public Double? TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the ThresholdLimit.
        /// </summary>
        [Column]
        public int? ThresholdLimit { get; set; }

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
