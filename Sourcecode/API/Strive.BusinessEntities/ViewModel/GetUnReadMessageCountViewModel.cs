using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class GetUnReadMessageCountViewModel
    {
        public string SenderName { get; set; }
        public bool TotalMesUnread { get; set; }
        public int EmployeeId { get; set; }
    }
}
