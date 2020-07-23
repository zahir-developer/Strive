namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Service" />.
    /// </summary>
    [OverrideName("tblService")]
    public class Service
    {
        /// <summary>
        /// Gets or sets the ServiceId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the ServiceName.
        /// </summary>
        [Column]
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the ServiceType.
        /// </summary>
        [Column]
        public int? ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the Cost.
        /// </summary>
        [Column]
        public Double? Cost { get; set; }

        /// <summary>
        /// Gets or sets the Commision.
        /// </summary>
        [Column]
        public bool? Commision { get; set; }

        /// <summary>
        /// Gets or sets the CommisionType.
        /// </summary>
        [Column]
        public int? CommisionType { get; set; }

        /// <summary>
        /// Gets or sets the Upcharges.
        /// </summary>
        [Column]
        public Double? Upcharges { get; set; }

        /// <summary>
        /// Gets or sets the ParentServiceId.
        /// </summary>
        [Column]
        public int? ParentServiceId { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the CommissionCost.
        /// </summary>
        [Column]
        public decimal? CommissionCost { get; set; }

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
