using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class NotificationViewModel
    {
        public string Name { get; set; }
        public int CheckListId { get; set; }
        public int CheckListEmployeeId { get; set; }
        public TimeSpan NotificationTime { get; set; }
        public int RoleId { get; set; }

        public DateTime NotificationDate { get; set; }

        public string Token { get; set; }

    }
}
