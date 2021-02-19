using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tbldeal")]
    public class DealSetup
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]


        public int DealId { get; set; }

        [Column]
        public string DealName { get; set; }

        [Column]
        public string TimePeriod { get; set; }

        [Column]
        public bool Deals { get; set; }

        [Column]
        public DateTime StartDate { get; set; }
        [Column]
        public DateTime EndDate { get; set; }
    }
}
