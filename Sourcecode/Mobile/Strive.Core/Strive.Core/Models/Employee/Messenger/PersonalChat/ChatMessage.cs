using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.PersonalChat
{
    public class ChatMessage
    {
        public List<ChatMessageDetail> ChatMessageDetail { get; set; }
    }
    public class ChatMessageDetail
    {
        public int SenderId { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public int ReceipientId { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string MessageBody { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
