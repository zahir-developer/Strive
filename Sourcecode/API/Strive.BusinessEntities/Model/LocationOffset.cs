using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblLocationOffset")]
    public class LocationOffset
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int LocationOffsetId { get; set; }

        [Column]
        public int LocationId { get; set; }

        [Column]
        public decimal? OffSet1 { get; set; }

        [Column]
        public decimal? OffSetA { get; set; }

         [Column]
        public decimal? OffSetB { get; set; }

        [Column]
        public decimal? OffSetC { get; set; }
            
        [Column]
        public decimal? OffSetD { get; set; }

        [Column]
        public decimal? OffSetE { get; set; }

        [Column]
        public decimal? OffSetF { get; set; }
        [Column]
        public bool? OffSet1On { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }    
    }
}