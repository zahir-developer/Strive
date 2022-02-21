using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ChecklistNotificationViewModel
    {
        public string Name { get; set; }
        public int CheckListId { get; set; }
        public int ChecklistNotificationId { get; set; }
        public TimeSpan NotificationTime { get; set; }
        public bool? IsCompleted { get; set; }
        
    }
}
