using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblBonusRange")]
    public class BonusRange
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int BonusRangeId { get; set; }

        [Column]
        public int? BonusId { get; set; }

        [Column]
        public int? Min { get; set; }

        [Column]
        public int? Max { get; set; }

        [Column]
        public decimal? BonusAmount { get; set; }

        [Column]
        public decimal? Total { get; set; }

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