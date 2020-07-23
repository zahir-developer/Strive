namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="CodeCategory" />.
    /// </summary>
    [OverrideName("tblCodeCategory")]
    public class CodeCategory
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        [Column]
        public string Category { get; set; }

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
