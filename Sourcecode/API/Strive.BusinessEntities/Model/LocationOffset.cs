using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblLocationOffset")]
    public class LocationOffset
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int LocationOffSetId { get; set; }

        [Column]
        public int LocationId { get; set; }

        [Column]
        public bool? OffSet1 { get; set; }

        [Column]
        public bool? OffSetA { get; set; }

         [Column]
        public bool? OffSetB { get; set; }

        [Column]
        public bool? OffSetC { get; set; }
            
        [Column]
        public bool? OffSetD { get; set; }

        [Column]
        public bool? OffSetE { get; set; }

        [Column]
        public bool? OffSetF { get; set; }
        [Column]
        public bool? OffSet1On { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }    
    }
}