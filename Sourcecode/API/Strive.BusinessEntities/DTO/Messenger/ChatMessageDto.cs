using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Messenger
{
    public class ChatMessageDto
    {
        public ChatMessage ChatMessage { get; set; }

        public ChatMessageRecipient ChatMessageRecipient { get; set; }

        public List<ChatGroupRecipient> ChatGroupRecipient { get; set; }

        public string ConnectionId { get; set; }

        public string FullName { get; set; }

        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
