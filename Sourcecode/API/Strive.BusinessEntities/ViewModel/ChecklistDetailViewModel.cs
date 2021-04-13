using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ChecklistDetailViewModel
    {
        public ChecklistViewModel ChecklistDetail { get; set; }
        public List<ChecklistNotificationViewModel> ChecklistNotificationTime { get; set; }
    }
}
