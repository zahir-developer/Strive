using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.PersonalChat
{
    public class SendChatMessage
    {
        public chatMessage chatMessage { get; set; }
        public chatMessageRecipient chatMessageRecipient { get; set; }
        public string ConnectionId { get; set; }
        public string FullName { get; set; }
        public string GroupId { get; set; }
    }
    public class chatMessage
    {
		public int ChatMessageId { get; set; }
		public string Subject { get; set; }
		public string Messagebody { get; set; }
		public int? ParentChatMessageId { get; set; }
		public string ExpiryDate { get; set; }
		public bool? IsReminder { get; set; }
		public string NextRemindDate { get; set; }
		public int? ReminderFrequencyId { get; set; }
		public int? CreatedBy { get; set; }
		public string CreatedDate { get; set; }

	}
	public class chatMessageRecipient
	{
        public long ChatRecipientId { get; set; }
        public long? ChatMessageId { get; set; }
        public int? SenderId { get; set; }
        public int? RecipientId { get; set; }
        public int? RecipientGroupId { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public bool? IsRead { get; set; }
    }
}
