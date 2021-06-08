using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblService")]
    public class Service
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ServiceId { get; set; }

        [Column]
        public string ServiceName { get; set; }

        [Column]
        public int? ServiceType { get; set; }

        [Column]
        public int? LocationId { get; set; }
        [Column]
        public int? ServiceCategory { get; set; }
        [Column]
        public bool? IsCeramic { get; set; }

        [Column]
        public decimal? Cost { get; set; }
        [Column]
        public decimal? Price { get; set; }

        [Column]
        public bool? Commision { get; set; }

        [Column]
        public int? CommisionType { get; set; }

        [Column]
        public string Upcharges { get; set; }

        [Column]
        public int? ParentServiceId { get; set; }

        [Column]
        public decimal? CommissionCost { get; set; }

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

        [Column]
        public string Description { get; set; }

        [Column]
        public int? DiscountServiceType { get; set; }
        [Column]
        public string DiscountType { get; set; }

        [Column]
        public TimeSpan? Hours  { get; set; }


    }
}