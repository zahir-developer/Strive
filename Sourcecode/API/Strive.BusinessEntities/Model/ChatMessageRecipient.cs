using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblChatMessageRecipient")]
    public class ChatMessageRecipient
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public long ChatRecipientId { get; set; }

        [Column, PrimaryKey]
        public long? ChatMessageId { get; set; }

        [Column, PrimaryKey]
        public int? SenderId { get; set; }

        [Column, PrimaryKey]
        public int? RecipientId { get; set; }

        [Column, PrimaryKey]
        public int? RecipientGroupId { get; set; }
        
        [Column]
        public bool? IsRead { get; set; }

    }
}