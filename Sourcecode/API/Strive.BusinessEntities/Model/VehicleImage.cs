using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblVehicleImage")]
    public class VehicleImage
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int VehicleImageId { get; set; }

        [Column]
        public int VehicleId { get; set; }


        [Column]
        public string ImageName { get; set; }

        [Column]
        public string OriginalImageName { get; set; }

        [Column]
        public string ThumbnailFileName { get; set; }

        [Column]
        public string FilePath { get; set; }

        [Column]
        public string Description { get; set; }

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