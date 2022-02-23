using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class ChecklistNotificationDto
    {
        public int EmployeeId { get; set; }
        public int Role { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
