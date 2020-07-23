namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Location" />.
    /// </summary>
    [OverrideName("tblLocation")]
    public class Location
    {
        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the LocationType.
        /// </summary>
        [Column]
        public int? LocationType { get; set; }

        /// <summary>
        /// Gets or sets the LocationName.
        /// </summary>
        [Column]
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the LocationDescription.
        /// </summary>
        [Column]
        public string LocationDescription { get; set; }

        /// <summary>
        /// Gets or sets the IsFranchise.
        /// </summary>
        [Column]
        public bool? IsFranchise { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the TaxRate.
        /// </summary>
        [Column]
        public string TaxRate { get; set; }

        /// <summary>
        /// Gets or sets the SiteUrl.
        /// </summary>
        [Column]
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the Currency.
        /// </summary>
        [Column]
        public int? Currency { get; set; }

        /// <summary>
        /// Gets or sets the Facebook.
        /// </summary>
        [Column]
        public string Facebook { get; set; }

        /// <summary>
        /// Gets or sets the Twitter.
        /// </summary>
        [Column]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the Instagram.
        /// </summary>
        [Column]
        public string Instagram { get; set; }

        /// <summary>
        /// Gets or sets the WifiDetail.
        /// </summary>
        [Column]
        public string WifiDetail { get; set; }

        /// <summary>
        /// Gets or sets the WorkhourThreshold.
        /// </summary>
        [Column]
        public int? WorkhourThreshold { get; set; }

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
