using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ChecklistDetailViewModel
    {
        public int ChecklistNotificationId { get; set; }
        public int ChecklistId { get; set; }
        public int RoleId { get; set; }
        public int RoleName { get; set; }
        public DateTime NotificationTime { get; set; }
    }
}
