using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel.Messenger
{
    public class ChatEmployeeList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsGroup { get; set; }
        public int ChatGroupId { get; set; }
        public string GroupId { get; set; }
        public string CommunicationId { get; set; }
        public string RecentChatMessage { get; set; }
        public int? ChatGroupUserId { get; set; }
        public string CreatedDate { get; set; }
        public bool? IsRead { get; set; }
        public bool Selected { get; set; }
    }
}
