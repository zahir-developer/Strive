namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="CodeValue" />.
    /// </summary>
    [OverrideName("tblCodeValue")]
    public class CodeValue
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId.
        /// </summary>
        [Column]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the CodeValue.
        /// </summary>
        [Column]
        [OverrideName("CodeValue")]
        public string codeValue { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        [Column]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the CodeShortValue.
        /// </summary>
        [Column]
        public string CodeShortValue { get; set; }

        /// <summary>
        /// Gets or sets the ParentId.
        /// </summary>
        [Column]
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the SortOrder.
        /// </summary>
        [Column]
        public int? SortOrder { get; set; }

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
