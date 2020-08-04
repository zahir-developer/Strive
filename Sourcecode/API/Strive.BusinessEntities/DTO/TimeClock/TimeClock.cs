using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace Strive.BusinessEntities.TimeClock
{
    [Table("tblTimeClock")]
    public class TimeClock
    {
        [Key]
        public long id { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int RoleId { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public int EventType { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedFrom { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
        public string Comments { get; set; }
    }
}
