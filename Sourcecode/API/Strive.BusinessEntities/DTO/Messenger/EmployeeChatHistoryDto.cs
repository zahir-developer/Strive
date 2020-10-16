using Strive.BusinessEntities.ViewModel.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Messenger
{
    public class EmployeeChatHistoryDto
    {
        public List<ChatEmployeeList> ChatEmployeeList { get; set; }

        public List<GroupDto> GroupList { get; set; }
    }
}
