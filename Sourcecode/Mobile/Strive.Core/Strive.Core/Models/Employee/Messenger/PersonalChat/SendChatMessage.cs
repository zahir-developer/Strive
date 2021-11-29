using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.PersonalChat
{
    public class  SendChatMessage
    {
        public chatMessage chatMessage { get; set; }
        public chatMessageRecipient chatMessageRecipient { get; set; }
        public List<chatGroupRecipient> chatGroupRecipient { get; set; }
        public string connectionId { get; set; }
        public string fullName { get; set; }
        public string groupId { get; set; }
        public string groupName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }



    }
    public class chatMessage
    {
		public int chatMessageId { get; set; }
		public string subject { get; set; }
		public string messagebody { get; set; }
		public int? parentChatMessageId { get; set; }
		public string expiryDate { get; set; }
		public bool? isReminder { get; set; }
		public string nextRemindDate { get; set; }
		public int? reminderFrequencyId { get; set; }
		public int? createdBy { get; set; }
		public string createdDate { get; set; }

	}
	public class chatMessageRecipient
	{
        public long chatRecipientId { get; set; }
        public long? chatMessageId { get; set; }
        public int? senderId { get; set; }
        public int? recipientId { get; set; }
        public int? recipientGroupId { get; set; }
        public string createdDate { get; set; }
        public bool? isRead { get; set; }
    }
    public class chatGroupRecipient
    {
        public int chatGroupRecipientId { get; set; }
        public int chatGroupId { get; set; }
        public int? recipientId { get; set; }
        public bool isRead { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
    }
}
