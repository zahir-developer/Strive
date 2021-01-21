using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblDocument")]
    public class Document
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int DocumentId { get; set; }

        [Column, PrimaryKey]
        public int DocumentType { get; set; }

        [Column]
        public int? DocumentSubType { get; set; }

        [Column]
        public string FileName { get; set; }

        [Column]
        public string OriginalFileName { get; set; }

        [Column]
        public string FilePath { get; set; }

        [Ignore]
        public string Base64 { get; set; }

        [Column]
        public string DocumentName { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }

        [Column]
        public int? CreatedBy { get; set; }

        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }

        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }

       

    }
}