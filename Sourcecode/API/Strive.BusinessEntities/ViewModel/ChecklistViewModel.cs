using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ChecklistViewModel
    {
        public int ChecklistId { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public TimeSpan NotificationTime { get; set; }
    }
}
