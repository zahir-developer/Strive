using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblVehicleIssueImage")]
    public class VehicleIssueImage
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int VehicleIssueImageId { get; set; }

        [Column]
        public int VehicleIssueId { get; set; }

        [Column]
        public string DocumentType { get; set; }

        [Column]
        public string ImageName { get; set; }

        [Column]
        public string OriginalImageName { get; set; }

        [Column]
        public string ThumbnailFileName { get; set; }

        [Column]
        public string FilePath { get; set; }

        [Ignore]
        public string Base64 { get; set; }

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