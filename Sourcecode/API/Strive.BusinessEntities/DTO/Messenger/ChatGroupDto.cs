using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Messenger
{
    public class ChatGroupDto
    {
        public ChatGroup ChatGroup { get; set; }
        public List<ChatUserGroup> ChatUserGroup { get; set; }
        public string GroupId { get; set; }
    }
}
