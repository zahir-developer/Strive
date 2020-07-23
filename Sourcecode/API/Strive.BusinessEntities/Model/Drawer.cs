namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Drawer" />.
    /// </summary>
    [OverrideName("tblDrawer")]
    public class Drawer
    {
        /// <summary>
        /// Gets or sets the DrawerId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int DrawerId { get; set; }

        /// <summary>
        /// Gets or sets the DrawerName.
        /// </summary>
        [Column]
        public string DrawerName { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

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
