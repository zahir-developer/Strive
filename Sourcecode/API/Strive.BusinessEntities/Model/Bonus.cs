using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblBonus")]
    public class Bonus
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int BonusId { get; set; }

        [Column]
        public int? LocationId { get; set; }

        [Column]
        public int? BonusStatus { get; set; }

        [Column]
        public int? BonusMonth { get; set; }

        [Column]
        public int? BonusYear { get; set; }

        [Column]
        public int? NoOfBadReviews { get; set; }

        [Column]
        public decimal? BadReviewDeductionAmount { get; set; }

        [Column]
        public int? NoOfCollisions { get; set; }

        [Column]
        public decimal? CollisionDeductionAmount { get; set; }

        [Column]
        public decimal? TotalBonusAmount { get; set; }

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