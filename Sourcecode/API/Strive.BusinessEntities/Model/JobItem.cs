namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="JobItem" />.
    /// </summary>
    [OverrideName("tblJobItem")]
    public class JobItem
    {
        /// <summary>
        /// Gets or sets the JobItemId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobItemId { get; set; }

        /// <summary>
        /// Gets or sets the JobId.
        /// </summary>
        [Column]
        public int? JobId { get; set; }

        /// <summary>
        /// Gets or sets the ServiceId.
        /// </summary>
        [Column]
        public int? ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the Commission.
        /// </summary>
        [Column]
        public decimal? Commission { get; set; }

        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        [Column]
        public decimal? Price { get; set; }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        [Column]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the ReviewNote.
        /// </summary>
        [Column]
        public string ReviewNote { get; set; }

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
