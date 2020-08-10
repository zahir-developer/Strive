using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblEmployeeDocument")]
    public class EmployeeDocument
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeDocumentId { get; set; }

        [Column, PrimaryKey]
        public int? EmployeeId { get; set; }

        [Column]
        public string Filename { get; set; }

        [Column]
        public string Filepath { get; set; }

        [Ignore]
        public string Base64 { get; set; }

        [Column]
        public string FileType { get; set; }

        [Column]
        public bool? IsPasswordProtected { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public string Comments { get; set; }

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