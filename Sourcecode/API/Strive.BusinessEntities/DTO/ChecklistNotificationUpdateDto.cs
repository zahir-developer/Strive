using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class ChecklistNotificationUpdateDto
    {
        public List<CheckListNotification> CheckListNotification { get; set; }
    }

    public class CheckListNotification
    {
        public int CheckListEmployeeId { get; set; }
        public int UserId { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

