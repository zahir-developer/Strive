using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class ChecklistNotificationUpdate
    {
        public int CheckListEmployeeId { get; set; }
        public int UserId { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

