using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ChecklistNotificationViewModel
    {
        public int ChecklistNotificationId { get; set; }
        public int CheckListId { get; set; }
        public TimeSpan NotificationTime { get; set; }
    }
}
