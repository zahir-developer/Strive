using Cocoon.ORM;
using Dapper.Contrib.Extensions;
using System;

namespace Strive.BusinessEntities.Model
{
    [Table("tblLocation")]
    [OverrideName("tblLocation")]
    public class Location
    {
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int LocationId { get; set; }

        [Column]
        public int? LocationType { get; set; }

        [Column]
        public string LocationName { get; set; }

        [Column]
        public string LocationDescription { get; set; }

        [Column]
        public bool? IsFranchise { get; set; }

        [Column]
        public string TaxRate { get; set; }

        [Column]
        public string SiteUrl { get; set; }

        [Column]
        public int? Currency { get; set; }

        [Column]
        public string Facebook { get; set; }

        [Column]
        public string Twitter { get; set; }

        [Column]
        public string Instagram { get; set; }

        [Column]
        public string WifiDetail { get; set; }

        [Column]
        public int? WorkhourThreshold { get; set; }

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