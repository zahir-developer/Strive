using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblChatGroupRecipientId")]
    public class ChatGroupRecipient
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ChatGroupRecipientId { get; set; }

        [Column, PrimaryKey]
        public int ChatGroupId { get; set; }

        [Column, PrimaryKey]
        public int? RecipientId { get; set; }

        [Column]
        public bool? IsRead { get; set; }

        [Column]
        public int? CreatedBy { get; set; }

        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

    }
}