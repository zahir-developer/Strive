using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblCheckListNotification")]
    public class CheckListNotification
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int CheckListNotificationId { get; set; }


        [Column]
        public int? ChecklistId { get; set; }

        [Column]
        public TimeSpan NotificationTime { get; set; }

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
        public DateTime? NotificationDate { get; set; }

    }
}
