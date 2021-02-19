using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblDeals")]
    public class Deal
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int? DealId { get; set; }

        [Column]
        public string DealName { get; set; }

        [Column]
        public int? TimePeriod { get; set; }


        [Column]
        public DateTime? StartDate { get; set; }
        [Column]
        public DateTime? EndDate { get; set; }

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
